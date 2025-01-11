using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Olu�turulacak GameObject
    public Transform spawnPoint; // Olu�turulacak GameObject'in spawn noktas�
    public float baseSpawnRate = 2f; // Ba�lang�� spawn h�z� (saniye cinsinden)
    public float spawnRateIncreaseInterval = 30f; // Spawn h�z� art��� i�in ge�en s�re (saniye cinsinden)
    public float gravityScaleIncreaseInterval = 30f; // Gravity Scale art��� i�in ge�en s�re (saniye cinsinden)
    private float nextSpawnTime = 0f; // Bir sonraki spawn zaman�
    private float nextSpawnRateIncreaseTime = 0f; // Bir sonraki spawn h�z� art�� zaman�
    private float nextGravityScaleIncreaseTime = 0f; // Bir sonraki gravity scale art�� zaman�
    public float currentSpawnRate; // Mevcut spawn h�z�

    private void Start()
    {
        currentSpawnRate = baseSpawnRate;
    }

    private void Update()
    {
        // E�er spawn zaman� geldiyse yeni bir GameObject olu�tur
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + 1f / currentSpawnRate;
        }

        // E�er spawn h�z� art�� zaman� geldiyse, spawn h�z�n� art�r
        if (Time.time >= nextSpawnRateIncreaseTime)
        {
            IncreaseSpawnRate();
            nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
        }

        // E�er gravity scale art�� zaman� geldiyse, art�r
        if (Time.time >= nextGravityScaleIncreaseTime)
        {
            IncreaseGravityScale();
            nextGravityScaleIncreaseTime = Time.time + gravityScaleIncreaseInterval;
        }
    }

    void SpawnObject()
    {
        GameObject newObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale += 1f; // Her spawn i�lemi sonras�nda gravityScale'i 1 art�r
        }
    }

    // Spawn h�z�n� art�r�r
    private void IncreaseSpawnRate()
    {
        currentSpawnRate += 1f; // Spawn h�z�n� 1 art�r
    }

    // Gravity Scale de�erini art�r�r
    private void IncreaseGravityScale()
    {
        // Burada herhangi bir i�lem yapmam�za gerek yok, ��nk� spawn i�lemi s�ras�nda gravityScale zaten artt�r�l�yor.
    }
}
