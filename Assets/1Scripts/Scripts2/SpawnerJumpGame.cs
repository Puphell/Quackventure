using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerJumpGame : MonoBehaviour
{
    public GameObject platformPrefab;
    [SerializeField] int platformSayisi = 200;
    [SerializeField] float minDistanceX = 1.0f; // Minimum distance between platforms on X-axis
    [SerializeField] float minDistanceY = 1.0f; // Minimum distance between platforms on Y-axis

    void Start()
    {
        LevelUret();
    }

    void LevelUret()
    {
        Vector2 platformVektor = new Vector2();
        List<Vector2> usedPositions = new List<Vector2>(); // Keep track of used positions

        for (int i = 0; i < platformSayisi; i++)
        {
            platformVektor.x = Random.Range(-15.4f, 15.2f);
            platformVektor.y = Random.Range(-3738.4f, 1052.1f);

            // Check if the new position is too close to existing ones
            bool validPosition = true;
            foreach (Vector2 pos in usedPositions)
            {
                if (Vector2.Distance(platformVektor, pos) < minDistanceX || Mathf.Abs(platformVektor.y - pos.y) < minDistanceY)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                GameObject tempPlatform = Instantiate(platformPrefab);
                tempPlatform.transform.position = platformVektor;
                usedPositions.Add(platformVektor); // Add the new position to used positions
            }
        }
    }
}
