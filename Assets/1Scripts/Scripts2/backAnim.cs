using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backAnim : MonoBehaviour
{
    [SerializeField] Animator creditAnim;


    void Start()
    {
        Animator creditAnim = GetComponent<Animator>();
    }

    public void BackToTheMainMenu()
    {
        Invoke("BackToTheMainMenuInvoke", 0.5f);
        creditAnim.SetTrigger("Close");
    }
    private void BackToTheMainMenuInvoke()
    {
        SceneManager.LoadScene("MAIN MENU");
    }
}
