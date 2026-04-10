using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefab Único")]
    public GameObject projectilePrefab; // Asigna aqui el prefab que quieras en cada nivel

    [Header("Configuración de Nivel")]
    public bool levelArrowMode = true;

    [Range(0f, 1f)] public float bombProbability = 0.3f;

    [Header("Spawn")]
    public float spawnDistance = 50f;
    public float destinationOffsetRange = 2f;
    public float minSpeed = 5f, maxSpeed = 10f;
    public float minSpawnTime = 2f, maxSpawnTime = 3f;

    private List<GameObject> pool = new List<GameObject>();
    private int poolSize = 10;
    private float cooldown = 0f;
    private float nextSpawnTime;

    void Start()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Spawner: Asigna un prefab en el Inspector.");
            return;
        }
        InitializePool(poolSize);
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= nextSpawnTime)
        {
            Shoot();
            cooldown = 0f;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void InitializePool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject p = Instantiate(projectilePrefab);
            p.SetActive(false);
            pool.Add(p);
        }
    }

    void Shoot()
    {
        // Decidir tipo segun la configuracion del nivel
        Proyectil.Type typeToSpawn;
        if (levelArrowMode)
        {
            typeToSpawn = Proyectil.Type.Arrow;
        }
        else
        {
            typeToSpawn = (Random.value < bombProbability) ? Proyectil.Type.Bomb : Proyectil.Type.Normal;
        }

        float speed = Random.Range(minSpeed, maxSpeed);
        Vector3 origin = OriginPoint();

        foreach (GameObject p in pool)
        {
            if (!p.activeSelf)
            {
                p.transform.position = origin;
                p.SetActive(true);
                p.GetComponent<Proyectil>().Launch(destinationOffsetRange, typeToSpawn, speed);
                return;
            }
        }

        // Si el pool esta lleno, aniadir uno mas
        InitializePool(1);
        Shoot();
    }

    Vector3 OriginPoint()
    {
        Transform cam = Camera.main.transform;
        Vector3 pos = cam.position + cam.forward * spawnDistance;
        pos.y = 1.5f;
        return pos;
    }
}