using UnityEngine;

public class ProyectilBomba : MonoBehaviour
{
    [Header("Configuración")]
    public float maxLifeTime = 5f;
    public Color bombColor = Color.red;

    private Transform endPoint;
    private Counter counter;
    private Renderer projRenderer;
    private Vector3 direction;
    private float speed, lifeTime;
    private bool hasHit;

    private void Awake()
    {
        endPoint = EndPoint.Instance.transform;
        counter = FindAnyObjectByType<Counter>();
        projRenderer = GetComponent<Renderer>();
    }

    public void Launch(float destinationOffsetRange, float projectileSpeed)
    {
        speed = projectileSpeed;
        lifeTime = maxLifeTime;
        hasHit = false;

        if (projRenderer != null) projRenderer.material.color = bombColor;

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 target = new Vector3(endPoint.position.x + offset, endPoint.position.y, endPoint.position.z);
        direction = (target - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, 45f * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit || !other.CompareTag("Sable")) return;
        hasHit = true;

        if (counter != null) counter.counter--;
        Deactivate();
    }

    private void Deactivate() => gameObject.SetActive(false);
}