using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COIN : MonoBehaviour
{
    public int value;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            COINCOUNTERForSahne1.instance.IncreaseCoins(value); // Sahne 1 için toplanan coin miktarýný artýr
        }
    }
}
