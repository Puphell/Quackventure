using UnityEngine;

public class GameControl : MonoBehaviour
{
    void Start()
    {
        // PlayerPrefs'ten se�ilen indeksi al
        int selectedIndex = PlayerPrefs.GetInt("SelectedElementIndex", 0);
        // Se�ilen indekse sahip olan� enable yap, di�erlerini disable yap
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(selectedIndex).gameObject.SetActive(true);
    }
}
