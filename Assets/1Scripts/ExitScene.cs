using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float delayBeforeExit = 1f;

    bool transitionStarted = false;

    void Update()
    {
        // Ekrana týklanýrsa veya Space tuþuna basýlýrsa ve geçiþ baþlamadýysa
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !transitionStarted)
        {
            // Geçiþ baþladý olarak iþaretle
            transitionStarted = true;

            // "End" triggerini tetikle
            transitionAnimator.SetTrigger("End");

            // Belirli bir süre bekleyerek oyunu çýk
            Invoke("ExitGame", delayBeforeExit);
        }
    }

    void ExitGame()
    {
        // Oyundan çýk
        Application.Quit();
    }
}
