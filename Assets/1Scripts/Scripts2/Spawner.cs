using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Oluþturulacak GameObject
    public Transform spawnPoint; // Oluþturulacak GameObject'in spawn noktasý
    public float baseSpawnRate = 2f; // Baþlangýç spawn hýzý (saniye cinsinden)
    public float spawnRateIncreaseInterval = 30f; // Spawn hýzý artýþý için geçen süre (saniye cinsinden)
    public float gravityScaleIncreaseInterval = 30f; // Gravity Scale artýþý için geçen süre (saniye cinsinden)
    private float nextSpawnTime = 0f; // Bir sonraki spawn zamaný
    private float nextSpawnRateIncreaseTime = 0f; // Bir sonraki spawn hýzý artýþ zamaný
    private float nextGravityScaleIncreaseTime = 0f; // Bir sonraki gravity scale artýþ zamaný
    public float currentSpawnRate; // Mevcut spawn hýzý

    private void Start()
    {
        currentSpawnRate = baseSpawnRate;
    }

    private void Update()
    {
        // Eðer spawn zamaný geldiyse yeni bir GameObject oluþtur
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + 1f / currentSpawnRate;
        }

        // Eðer spawn hýzý artýþ zamaný geldiyse, spawn hýzýný artýr
        if (Time.time >= nextSpawnRateIncreaseTime)
        {
            IncreaseSpawnRate();
            nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
        }

        // Eðer gravity scale artýþ zamaný geldiyse, artýr
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
            rb.gravityScale += 1f; // Her spawn iþlemi sonrasýnda gravityScale'i 1 artýr
        }
    }

    // Spawn hýzýný artýrýr
    private void IncreaseSpawnRate()
    {
        currentSpawnRate += 1f; // Spawn hýzýný 1 artýr
    }

    // Gravity Scale deðerini artýrýr
    private void IncreaseGravityScale()
    {
        // Burada herhangi bir iþlem yapmamýza gerek yok, çünkü spawn iþlemi sýrasýnda gravityScale zaten arttýrýlýyor.
    }
}
