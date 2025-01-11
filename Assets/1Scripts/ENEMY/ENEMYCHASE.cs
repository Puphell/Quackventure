using System.Collections.Generic;
using UnityEngine;

public class AITrackingPlayer : MonoBehaviour
{
    public float moveSpeed = 13f; // Hareket hızı
    private Transform player; // Oyuncunun Transform bileşeni
    public List<GameObject> playerGameObjects; // Player GameObjects listesi
    public ShopManager shopManager;

    void Start()
    {
        // Başlangıçta ShopManager nesnesine erişim sağla
        shopManager = GameObject.FindObjectOfType<ShopManager>();

        // Oyuncuyu bulmaya çalış
        FindPlayer();

        // Eğer oyuncu bulunamazsa uyarı ver
        if (player == null)
        {
            Debug.LogWarning("Oyuncu bulunamadı! Lütfen 'Player' etiketine sahip bir nesne eklediğinizden emin olun veya Player GameObjects listesine manuel olarak oyuncu ekleyin.");
        }
    }

    void Update()
    {
        // Oyuncuyu bulmaya çalış
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            // Hedefe doğru yönel
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Hedefe doğru ilerle
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void FindPlayer()
    {
        // Oyuncuyu bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else if (playerGameObjects.Count > 0)
        {
            // Eğer 'Player' etiketine sahip nesne bulunamazsa, listeden ilk nesneyi kullan
            player = playerGameObjects[0].transform;
        }
    }
}
