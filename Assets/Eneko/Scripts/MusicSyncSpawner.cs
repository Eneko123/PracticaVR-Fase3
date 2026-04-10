using System.Collections.Generic;
using UnityEngine;

public class MusicSyncSpawner : MonoBehaviour
{
    [Header("Projectile Pool")]
    public GameObject projectil;
    public List<GameObject> pool = new List<GameObject>();
    private int poolSize = 20;

    [Header("Spawn Settings")]
    public float spawnDistance = 50;
    public float destinationOffsetRange = 2;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    [Header("Music Sync")]
    public AudioSource musicSource;
    public bool useMusicSync = true;
    public float bpm = 128f; // BPM de tu cancion
    public int beatsPerSpawn = 2; // Cada cuantos beats spawner

    [Header("Difficulty Patterns")]
    public DifficultyPattern[] patterns;

    private float beatDuration;
    private float nextBeatTime;
    private int beatCounter = 0;
    private bool isPlaying = false;
    private int currentPatternIndex = 0;

    [System.Serializable]
    public class DifficultyPattern
    {
        public string name;
        public float startTime;
        public float endTime;
        public int beatsPerSpawn;
        public float bombProbability;
        public float minSpeed;
        public float maxSpeed;
    }

    void Start()
    {
        AddProyectil(poolSize);

        if (useMusicSync && musicSource != null)
        {
            beatDuration = 60f / bpm;
            nextBeatTime = beatDuration;

            // Ordenar patrones por tiempo
            if (patterns.Length > 0)
            {
                System.Array.Sort(patterns, (a, b) => a.startTime.CompareTo(b.startTime));
            }

            // Iniciar musica
            musicSource.Play();
            isPlaying = true;
        }
    }

    void Update()
    {
        if (useMusicSync && isPlaying && musicSource != null && musicSource.isPlaying)
        {
            UpdateMusicSync();
        }
        else if (!useMusicSync)
        {
            // Modo original sin musica
            UpdateRandomSpawn();
        }
    }

    void UpdateMusicSync()
    {
        float currentTime = musicSource.time;

        // Actualizar patron actual
        UpdateCurrentPattern(currentTime);

        // Detectar beat
        if (currentTime >= nextBeatTime)
        {
            beatCounter++;

            // Obtener configuracion del patron actual
            int spawnInterval = beatsPerSpawn;
            float bombProb = 0.3f;
            float speedMin = minSpeed;
            float speedMax = maxSpeed;

            if (currentPatternIndex < patterns.Length)
            {
                DifficultyPattern pattern = patterns[currentPatternIndex];
                spawnInterval = pattern.beatsPerSpawn;
                bombProb = pattern.bombProbability;
                speedMin = pattern.minSpeed;
                speedMax = pattern.maxSpeed;
            }

            // Spawner segun el intervalo
            if (beatCounter >= spawnInterval)
            {
                bool isBomb = Random.value < bombProb;
                float speed = Random.Range(speedMin, speedMax);
                ShootProyectil(OriginPoint(), isBomb, speed);
                beatCounter = 0;
            }

            nextBeatTime += beatDuration;
        }
    }

    void UpdateCurrentPattern(float currentTime)
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            if (currentTime >= patterns[i].startTime && currentTime < patterns[i].endTime)
            {
                currentPatternIndex = i;
                return;
            }
        }
        currentPatternIndex = patterns.Length;
    }

    // Modo sin musica 
    private float cooldown = 0;
    private float nextSpawnTime = 2f;

    void UpdateRandomSpawn()
    {
        cooldown += Time.deltaTime;

        if (cooldown >= nextSpawnTime)
        {
            bool isBomb = Random.value < 0.3f;
            float speed = Random.Range(minSpeed, maxSpeed);
            ShootProyectil(OriginPoint(), isBomb, speed);

            cooldown = 0f;
            nextSpawnTime = Random.Range(2f, 3f);
        }
    }

    void AddProyectil(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject p = Instantiate(projectil);
            p.SetActive(false);
            pool.Add(p);
        }
    }

    void ShootProyectil(Vector3 origin, bool isBomb, float speed)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].transform.position = origin;
                pool[i].SetActive(true);
                //pool[i].GetComponent<Proyectil>().Launch(destinationOffsetRange, isBomb, speed);
                return;
            }
        }

        AddProyectil(1);
        ShootProyectil(origin, isBomb, speed);
    }

    Vector3 OriginPoint()
    {
        Transform cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * spawnDistance;
        spawnPos.y = 1.5f;
        return spawnPos;
    }

    void OnDisable()
    {
        isPlaying = false;
    }
}