using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float delayBeforeExit = 1f;

    bool transitionStarted = false;

    void Update()
    {
        // Ekrana t�klan�rsa veya Space tu�una bas�l�rsa ve ge�i� ba�lamad�ysa
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !transitionStarted)
        {
            // Ge�i� ba�lad� olarak i�aretle
            transitionStarted = true;

            // "End" triggerini tetikle
            transitionAnimator.SetTrigger("End");

            // Belirli bir s�re bekleyerek oyunu ��k
            Invoke("ExitGame", delayBeforeExit);
        }
    }

    void ExitGame()
    {
        // Oyundan ��k
        Application.Quit();
    }
}
