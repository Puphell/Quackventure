using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject[] persistOnRestartObjects;

    void Start()
    {
        persistOnRestartObjects = GameObject.FindGameObjectsWithTag("PersistOnRestart");
        HideObjects();
    }

    public void RestartObjects()
    {
        ShowObjects();
    }

    void HideObjects()
    {
        foreach (GameObject obj in persistOnRestartObjects)
        {
            obj.SetActive(false);
        }
    }

    void ShowObjects()
    {
        foreach (GameObject obj in persistOnRestartObjects)
        {
            obj.SetActive(true);
        }
    }
}
