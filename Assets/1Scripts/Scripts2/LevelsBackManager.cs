using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsBackManager : MonoBehaviour
{
    public void FinishOutro()
    {
        SceneManager.LoadScene("MAIN MENU");
    }
}
