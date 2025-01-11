using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIndex : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("SelectedElementIndex"))
        {
            PlayerPrefs.SetInt("SelectedElementIndex", 0);
            PlayerPrefs.Save();
        }
    }

}
