using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BacktoThe
{


    public class backToMenu : MonoBehaviour
    {
        public void BackToTheMenu()
        {
            SceneManager.LoadScene("MAIN MENU");
        }
    }
}
