using UnityEngine;

public class GameControl : MonoBehaviour
{
    void Start()
    {
        // PlayerPrefs'ten seçilen indeksi al
        int selectedIndex = PlayerPrefs.GetInt("SelectedElementIndex", 0);
        // Seçilen indekse sahip olaný enable yap, diðerlerini disable yap
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(selectedIndex).gameObject.SetActive(true);
    }
}
