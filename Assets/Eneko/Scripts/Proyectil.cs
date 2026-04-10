using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public enum Type { Arrow, Normal, Bomb }
    public enum Direction { Left, Right, Up, Down }

    [Header("Configuración")]
    public float maxLifeTime = 5f;
    public Color normalColor = Color.blue;
    public Color bombColor = Color.red;

    [Header("Colisionadores")]
    public Collider mainCollider;
    public Collider leftFace, rightFace, upFace, downFace;

    [Header("UI Flechas")]
    public GameObject leftArrowUI, rightArrowUI, upArrowUI, downArrowUI;

    private Transform endPoint;
    private Counter counter;
    private Renderer proyectilRenderer;

    private Vector3 direction;
    private float speed, lifeTime;
    private Type currentType;
    private Direction expectedDirection;

    // Variables para el sistema de flechas
    private Direction activeDirection;
    private bool waitingForOpposite = false;

    private void Awake()
    {
        endPoint = EndPoint.Instance.transform;
        counter = FindAnyObjectByType<Counter>();
        proyectilRenderer = GetComponent<Renderer>();

        HideAllArrowUI();
        DisableAllColliders();
    }

    public void Launch(float destinationOffsetRange, Type type, float projectileSpeed)
    {
        speed = projectileSpeed;
        lifeTime = maxLifeTime;
        currentType = type;
        waitingForOpposite = false;

        if (proyectilRenderer != null)
            proyectilRenderer.material.color = (type == Type.Bomb) ? bombColor : normalColor;

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 target = new Vector3(endPoint.position.x + offset, endPoint.position.y, endPoint.position.z);
        direction = (target - transform.position).normalized;

        if (type == Type.Arrow) SetupArrow();
        else SetupHit(mainCollider);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (currentType == Type.Arrow)
            transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        else
            transform.Rotate(Vector3.up, 30f * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Sable")) return;

        // Determinar que collider fue tocado mediante raycasting o comparacion de bounds
        Collider hitCollider = DetermineHitCollider(other);

        if (hitCollider == null)
        {
            return;
        }

        if (hitCollider == mainCollider)
        {
            if (currentType == Type.Bomb)
            {
                counter.counter -= 1;
            }
            else
            {
                counter.counter += 1;
            }
            Deactivate();
        }
        else if (currentType == Type.Arrow)
        {
            Direction touchedDirection = GetDirectionFromCollider(hitCollider);

            if (!waitingForOpposite)
            {
                activeDirection = touchedDirection;
                waitingForOpposite = true;
            }
            else
            {
                bool isOpposite = IsOppositeDirection(activeDirection, touchedDirection);

                if (isOpposite && touchedDirection == expectedDirection)
                {
                    counter.counter += 1;
                    Deactivate();
                }
                //else if (!isOpposite)
                //{
                //    Debug.Log($"✗ No es opuesta. Tocó {activeDirection} → {touchedDirection} (no son opuestas)");
                //    // NO desactivar, permitir seguir intentando
                //}
                //else if (touchedDirection != expectedDirection)
                //{
                //    Deactivate();
                //}
            }
        }
    }

    // Metodo para determinar que collider fue golpeado
    private Collider DetermineHitCollider(Collider sableCollider)
    {
        // Obtener el punto de contacto aproximado
        Vector3 sableCenter = sableCollider.bounds.center;

        // Verificar cada collider y ver cuál esta mas cerca del punto de contacto
        float minDistance = float.MaxValue;
        Collider closestCollider = null;

        Collider[] collidersToCheck = { mainCollider, leftFace, rightFace, upFace, downFace };

        foreach (Collider col in collidersToCheck)
        {
            if (col != null && col.enabled)
            {
                Vector3 closestPoint = col.ClosestPoint(sableCenter);
                float distance = Vector3.Distance(closestPoint, sableCenter);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCollider = col;
                }
            }
        }

        return closestCollider;
    }

    private Direction GetDirectionFromCollider(Collider col)
    {
        if (col == leftFace) return Direction.Left;
        if (col == rightFace) return Direction.Right;
        if (col == upFace) return Direction.Up;
        if (col == downFace) return Direction.Down;
        return Direction.Left; // default
    }

    private bool IsOppositeDirection(Direction dir1, Direction dir2)
    {
        if (dir1 == Direction.Left && dir2 == Direction.Right) return true;
        if (dir1 == Direction.Right && dir2 == Direction.Left) return true;
        if (dir1 == Direction.Up && dir2 == Direction.Down) return true;
        if (dir1 == Direction.Down && dir2 == Direction.Up) return true;
        return false;
    }

    //private Direction GetOppositeDirection(Direction dir)
    //{
    //    switch (dir)
    //    {
    //        case Direction.Left: return Direction.Right;
    //        case Direction.Right: return Direction.Left;
    //        case Direction.Up: return Direction.Down;
    //        case Direction.Down: return Direction.Up;
    //        default: return Direction.Left;
    //    }
    //}

    private void SetupArrow()
    {
        int rand = Random.Range(0, 4);
        DisableAllColliders();
        HideAllArrowUI();

        // Habilitar todos los colliders de cara para detectar cualquier toque
        if (leftFace != null) leftFace.enabled = true;
        if (rightFace != null) rightFace.enabled = true;
        if (upFace != null) upFace.enabled = true;
        if (downFace != null) downFace.enabled = true;

        switch (rand)
        {
            case 0: expectedDirection = Direction.Left; leftArrowUI?.SetActive(true); break;
            case 1: expectedDirection = Direction.Right; rightArrowUI?.SetActive(true); break;
            case 2: expectedDirection = Direction.Up; upArrowUI?.SetActive(true); break;
            case 3: expectedDirection = Direction.Down; downArrowUI?.SetActive(true); break;
        }
    }

    private void SetupHit(Collider activeCol)
    {
        DisableAllColliders();
        HideAllArrowUI();
        if (activeCol != null) activeCol.enabled = true;
    }

    private void DisableAllColliders()
    {
        if (mainCollider != null) mainCollider.enabled = false;
        if (leftFace != null) leftFace.enabled = false;
        if (rightFace != null) rightFace.enabled = false;
        if (upFace != null) upFace.enabled = false;
        if (downFace != null) downFace.enabled = false;
    }

    private void HideAllArrowUI()
    {
        if (leftArrowUI != null) leftArrowUI.SetActive(false);
        if (rightArrowUI != null) rightArrowUI.SetActive(false);
        if (upArrowUI != null) upArrowUI.SetActive(false);
        if (downArrowUI != null) downArrowUI.SetActive(false);
    }

    private void Deactivate()
    {
        // Reproducir sonido correspondiente
        if (SoundEffectsManager.Instance != null)
        {
            SoundEffectsManager.Instance.PlayProjectileSound(currentType);
        }

        HideAllArrowUI();
        DisableAllColliders();
        waitingForOpposite = false;
        gameObject.SetActive(false);
    }
}
