using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new31 : MonoBehaviour
{
    public GameObject TutorialTextCoinObj;
    public GameObject TransitionButtonObj;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TutorialTextCoinObj.SetActive(false);
            TransitionButtonObj.SetActive(true);
        }

    }
}
