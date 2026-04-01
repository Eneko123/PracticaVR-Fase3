using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Transform end;
    private float speed;
    private Vector3 direction;
    private Counter counter;
    public float maxLifeTime = 5f;
    private float lifeTime;

    // Nuevas variables para el sistema de bombas
    private bool isBomb;
    private Renderer proyectilRenderer;
    public Color normalColor = Color.blue;
    public Color bombColor = Color.red;

    // Nuevos coliders para sistema de flechas
    public Collider leftFace;
    public Collider rightFace;
    public Collider upFace;
    public Collider downFace;
    private bool firstHit = false;
    private bool secondHit = false;

    private void Awake()
    {
        end = EndPoint.Instance.transform;
        counter = FindAnyObjectByType<Counter>();
        proyectilRenderer = GetComponent<Renderer>();
    }

    public void Launch(float destinationOffsetRange, bool bomb, float projectileSpeed)
    {
        speed = projectileSpeed;
        lifeTime = maxLifeTime;
        isBomb = bomb;

        if (proyectilRenderer != null)
        {
            proyectilRenderer.material.color = isBomb ? bombColor : normalColor;
        }

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);

        Vector3 targetPos = new Vector3(end.position.x + offset, end.position.y, end.position.z);

        direction = (targetPos - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        LifeTime();
    }

    void LifeTime()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sable"))
        {
            if (isBomb)
            {
                // Las bombas restan 1 punto
                counter.counter--;
            }
            else
            {
                // Proyectil normal suma 1 punto
                counter.counter++;
                firstHit = true;
                SableAtackDirection1(ArrowDirectionFirst(), ArrowDirectionSecond());
                SableAtackDirection2(ArrowDirectionFirst(), ArrowDirectionSecond());
            }
        }
    }

    public bool IsBomb()
    {
        return isBomb;
    }

    Collider ArrowDirectionFirst()
    {
        int faceHit = Random.Range(0,4);

        switch (faceHit)
        {
            case 0:
                leftFace.enabled = true;
                rightFace.enabled = false;
                upFace.enabled = false;
                downFace.enabled = false;
                return leftFace;
            case 1:
                leftFace.enabled = false;
                rightFace.enabled = true;
                upFace.enabled = false;
                downFace.enabled = false;
                return rightFace;
            case 2:
                leftFace.enabled = false;
                rightFace.enabled = false;
                upFace.enabled = true;
                downFace.enabled = false;
                return upFace;
            case 3:
                leftFace.enabled = false;
                rightFace.enabled = false;
                upFace.enabled = false;
                downFace.enabled = true;
                return downFace;
            default:
                // Valor de retorno por defecto para evitar CS0161
                return null;
        }
    }

    Collider ArrowDirectionSecond()
    {
        if (ArrowDirectionFirst() == leftFace)
        {
            return rightFace;
        }
        else if (ArrowDirectionFirst() == rightFace)
        {
            return leftFace;
        }
        else if (ArrowDirectionFirst() == upFace)
        {
            return downFace;
        }
        else if (ArrowDirectionFirst() == downFace)
        {
            return upFace;
        }
        else
        {
            // Valor de retorno por defecto para evitar CS0161
            return null;
        }
    }

    void SableAtackDirection1(Collider Face1, Collider Face2)
    {
        if (firstHit && !secondHit)
        {
            Face1.enabled = true;
            Face2.enabled = false;
            secondHit = true;
        }
    }
    void SableAtackDirection2(Collider Face1, Collider Face2)
    {
        if (firstHit && secondHit)
        {
            Face1.enabled = false;
            Face2.enabled = false;
            gameObject.SetActive(false);
            firstHit = false;
            secondHit = false;
        }
    }
}