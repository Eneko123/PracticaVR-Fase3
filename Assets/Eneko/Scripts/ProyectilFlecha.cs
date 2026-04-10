using UnityEngine;

public class ProyectilFlecha : MonoBehaviour
{
    [Header("Configuración")]
    public float maxLifeTime = 5f;

    [Header("Colliders por cara (Is Trigger)")]
    [SerializeField] private Collider leftFace, rightFace, upFace, downFace;

    [Header("UI de Flechas")]
    [SerializeField] private GameObject leftArrowUI, rightArrowUI, upArrowUI, downArrowUI;

    private Transform endPoint;
    private Counter counter;
    private Vector3 direction;
    private float speed, lifeTime;

    private enum Direction { Left, Right, Up, Down }
    private Direction expectedDirection;

    private void Awake()
    {
        endPoint = EndPoint.Instance.transform;
        counter = FindAnyObjectByType<Counter>();
        HideAll();
    }

    public void Launch(float destinationOffsetRange, float projectileSpeed)
    {
        speed = projectileSpeed;
        lifeTime = maxLifeTime;

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 target = new Vector3(endPoint.position.x + offset, endPoint.position.y, endPoint.position.z);
        direction = (target - transform.position).normalized;

        InitializeDirection();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Sable")) return;

        bool correct = false;
        if (other == leftFace) correct = (expectedDirection == Direction.Left);
        else if (other == rightFace) correct = (expectedDirection == Direction.Right);
        else if (other == upFace) correct = (expectedDirection == Direction.Up);
        else if (other == downFace) correct = (expectedDirection == Direction.Down);

        if (counter != null) counter.counter += 1;
        Deactivate();
    }

    private void InitializeDirection()
    {
        int rand = Random.Range(0, 4);
        HideAll();

        switch (rand)
        {
            case 0: expectedDirection = Direction.Left; leftFace.enabled = true; leftArrowUI?.SetActive(true); break;
            case 1: expectedDirection = Direction.Right; rightFace.enabled = true; rightArrowUI?.SetActive(true); break;
            case 2: expectedDirection = Direction.Up; upFace.enabled = true; upArrowUI?.SetActive(true); break;
            case 3: expectedDirection = Direction.Down; downFace.enabled = true; downArrowUI?.SetActive(true); break;
        }
    }

    private void HideAll()
    {
        if (leftFace != null) leftFace.enabled = false;
        if (rightFace != null) rightFace.enabled = false;
        if (upFace != null) upFace.enabled = false;
        if (downFace != null) downFace.enabled = false;
        if (leftArrowUI != null) leftArrowUI.SetActive(false);
        if (rightArrowUI != null) rightArrowUI.SetActive(false);
        if (upArrowUI != null) upArrowUI.SetActive(false);
        if (downArrowUI != null) downArrowUI.SetActive(false);
    }

    private void Deactivate() { HideAll(); gameObject.SetActive(false); }
}