using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishPoint : MonoBehaviour
{
    public GameObject objectToDisable; // Disable edilecek GameObject
    private bool hasPlayerTouched = false; // Player temas etti mi?

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayerTouched)
        {
            // SceneController'dan sonraki seviyeye ge�
            SceneManager.LoadScene("Season1");

            // E�er objectToDisable null de�ilse, disable hale getir
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // Player temas etti�i i�in hasPlayerTouched'i true yap
            hasPlayerTouched = true;
        }
    }
}
