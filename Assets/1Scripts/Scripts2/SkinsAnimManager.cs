using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinsAnimManager : MonoBehaviour
{
    [SerializeField] Animator SkinsAnimator;
    public void PlayOnAnim()
    {
        SkinsAnimator.SetTrigger("Close");
    }

    public void OptionsAnim()
    {
        SkinsAnimator.SetTrigger("Close");
    }

    public void CreditsAnim()
    {
        SkinsAnimator.SetTrigger("Close");
    }

    public void GoSkins()
    {
        SkinsAnimator.SetTrigger("Close");
        Invoke("SkinsAnim", 0.5f);
    }
    private void SkinsAnim()
    {
        SceneManager.LoadSceneAsync("SKINS");
    }
}
