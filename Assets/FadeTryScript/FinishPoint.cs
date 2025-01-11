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
            // SceneController'dan sonraki seviyeye geç
            SceneManager.LoadScene("Season1");

            // Eðer objectToDisable null deðilse, disable hale getir
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // Player temas ettiði için hasPlayerTouched'i true yap
            hasPlayerTouched = true;
        }
    }
}
