using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator MainAnimator;

     void Start()
     {
        Animator MainAnimator = GetComponent<Animator>();     
     }
    public void MAINMENUBUTTONClicked()
    {
        MainAnimator.SetTrigger("Close");
        Invoke("MainMenuButtonAnim", 0.5f);
    }
    private void MainMenuButtonAnim()
    {
        SceneManager.LoadScene("MAIN MENU");
    }

    public void SKINBUTTONClicked()
    {
        SceneManager.LoadScene("SKIN");
    }

    public void LEVEL1Clicked()
    {
        SceneManager.LoadScene("SAHNE 1");
    }   
    public void LEVEL2Clicked()
    {
        SceneManager.LoadScene("SAHNE 2");
    }

    public void LEVEL3Clicked()
    {
        SceneManager.LoadScene("SAHNE 3");
    }

    public void LEVEL4Clicked()
    {
        SceneManager.LoadScene("SAHNE 4");
    }

    public void LEVEL5Clicked()
    {
        SceneManager.LoadScene("SAHNE 5");
    }

    public void LEVEL6Clicked()
    {
        SceneManager.LoadScene("SAHNE 6");
    }

    public void LEVEL7Clicked()
    {
        SceneManager.LoadScene("SAHNE 7");
    }

    public void LEVEL8Clicked()
    {
        SceneManager.LoadScene("SAHNE 8");
    }

    public void LEVEL9Clicked()
    {
        SceneManager.LoadScene("SAHNE 9");
    }

    public void LEVEL10Clicked()
    {
        SceneManager.LoadScene("SAHNE 10");
    }
}