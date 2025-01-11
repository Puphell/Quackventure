using UnityEngine;
using UnityEngine.SceneManagement;

public class TabelaEtkilesim3 : MonoBehaviour
{
    public GameObject targetObject; // Hedef GameObject'i tanımla

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player Tabela ile etkileşime girerse
        if (other.CompareTag("Player"))
        {
            // Hedef GameObject'i aktif hale getir
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }

            // ChangeScene fonksiyonunu 1 saniye sonra çağır
            Invoke("ChangeScene", 1f);
        }
    }

    void ChangeScene()
    {
        // Sahneyi değiştir (3. sahne adınızı belirtin)
        SceneManager.LoadScene("Sahne 3");
    }
}
