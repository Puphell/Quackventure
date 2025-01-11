using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerReal : MonoBehaviour
{
    public GameObject[] initialActiveObjects;
    public GameObject[] firstStepObjects;
    public GameObject[] secondStepObjects;
    public GameObject[] thirdStepObjects;
    public GameObject[] postJumpStepObjects; // Jump sonrasý aktif olacak objeler

    public Button rightButton;
    public Button leftButton;
    public Button jumpButton;

    private void Start()
    {
        InitializeTutorial();
    }

    private void InitializeTutorial()
    {
        SetInitialActiveObjects();
        StartCoroutine(PrepareStep(firstStepObjects, rightButton, OnRightButtonClicked));
        rightButton.onClick.AddListener(OnRightButtonClicked);
        leftButton.onClick.AddListener(OnLeftButtonClicked);
        jumpButton.onClick.AddListener(OnJumpButtonClicked);
    }

    private void SetInitialActiveObjects()
    {
        foreach (GameObject obj in initialActiveObjects)
        {
            obj.SetActive(true);
        }
    }

    private void OnRightButtonClicked()
    {
        rightButton.onClick.RemoveListener(OnRightButtonClicked);
        StartCoroutine(HandleStep(firstStepObjects, secondStepObjects, leftButton, OnLeftButtonClicked));
    }

    private void OnLeftButtonClicked()
    {
        leftButton.onClick.RemoveListener(OnLeftButtonClicked);
        StartCoroutine(HandleStep(secondStepObjects, thirdStepObjects, jumpButton, OnJumpButtonClicked));
    }

    private void OnJumpButtonClicked()
    {
        jumpButton.onClick.RemoveListener(OnJumpButtonClicked);
        StartCoroutine(HandleStep(thirdStepObjects, postJumpStepObjects, null, null));
        // Jump sonrasý objeleri kontrol eden Coroutine'i baþlat
        StartCoroutine(CheckPlayerCoinCollision());
    }

    private IEnumerator PrepareStep(GameObject[] objectsToActivate, Button nextButton, UnityEngine.Events.UnityAction nextAction)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
        yield return null;

        if (nextButton != null && nextAction != null)
        {
            nextButton.onClick.AddListener(nextAction);
        }
    }

    private IEnumerator HandleStep(GameObject[] currentStepObjects, GameObject[] nextStepObjects, Button nextButton, UnityEngine.Events.UnityAction nextAction)
    {
        foreach (GameObject obj in currentStepObjects)
        {
            obj.SetActive(false);
        }
        if (nextStepObjects != null)
        {
            foreach (GameObject obj in nextStepObjects)
            {
                obj.SetActive(true);
            }
        }
        yield return null;

        if (nextButton != null && nextAction != null)
        {
            nextButton.onClick.AddListener(nextAction);
        }
    }

    private IEnumerator CheckPlayerCoinCollision()
    {
        while (true)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

            foreach (GameObject coin in coins)
            {
                if (player != null && coin != null && player.GetComponent<Collider2D>().IsTouching(coin.GetComponent<Collider2D>()))
                {
                    foreach (GameObject obj in postJumpStepObjects)
                    {
                        obj.SetActive(false);
                    }
                    ResetTutorial();
                    yield break; // Coroutine'i bitir
                }
            }
            yield return null; // Her frame kontrol et
        }
    }

    private void ResetTutorial()
    {
        // Deactivate all tutorial objects
        DeactivateAllObjects(firstStepObjects);
        DeactivateAllObjects(secondStepObjects);
        DeactivateAllObjects(thirdStepObjects);
        DeactivateAllObjects(postJumpStepObjects);

        // Remove all button listeners
        rightButton.onClick.RemoveAllListeners();
        leftButton.onClick.RemoveAllListeners();
        jumpButton.onClick.RemoveAllListeners();

        // Restart the tutorial
        InitializeTutorial();
    }

    private void DeactivateAllObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public void TransitionLevels()
    {
        SceneManager.LoadScene("LEVELS");
    }
}
