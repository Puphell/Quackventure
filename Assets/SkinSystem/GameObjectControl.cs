using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameObjectControl : MonoBehaviour
{
    public int[] elementPrices;
    public GameObject[] prefabObjects;
    public List<Button> buyButtonList;
    private int currentIndex = 0;
    public TotalCoinTextUpdater coinText;
    public Text totalCoinText;
    public Button selectButton;
    public Button goTestButton;
    private bool[] buyButtonClickedArray;
    public AudioClip[] buttonClickSounds;
    public AudioClip selectClickSound;
    public Animator SkinsAnimator;
    private int selectedElementIndex = -1; // Variable to store the selected element index

    void Start()
    {
        Animator SkinsAnimator = GetComponent<Animator>();
        EnableCurrentObject();
        EnableCurrentButton();
        selectButton.interactable = false;

        buyButtonClickedArray = new bool[buyButtonList.Count];
        buyButtonClickedArray[0] = true;
        buyButtonList[0].gameObject.SetActive(false);

        for (int i = 0; i < buyButtonList.Count; i++)
        {
            if (PlayerPrefs.HasKey("BuyButtonClicked_" + i))
            {
                buyButtonClickedArray[i] = PlayerPrefs.GetInt("BuyButtonClicked_" + i) == 1;
                if (buyButtonClickedArray[i])
                {
                    selectButton.interactable = true;
                    buyButtonList[i].gameObject.SetActive(false);
                }
            }
        }

        if (buyButtonClickedArray[0])
        {
            buyButtonList[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("DisabledBuyButtonIndex"))
        {
            int disabledButtonIndex = PlayerPrefs.GetInt("DisabledBuyButtonIndex");
            buyButtonList[disabledButtonIndex].gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SelectedElementIndex"))
        {
            selectedElementIndex = PlayerPrefs.GetInt("SelectedElementIndex");
        }

        if (!buyButtonClickedArray[currentIndex])
        {
            selectButton.interactable = false;
        }

        UpdateCoinText();
    }

    void Update()
    {
        buyButtonClickedArray[0] = true;
        buyButtonList[0].gameObject.SetActive(false);

        if (buyButtonClickedArray[0])
        {
            buyButtonList[0].gameObject.SetActive(false);
        }

        if (!buyButtonClickedArray[currentIndex])
        {
            selectButton.interactable = false;
        }

        if (buyButtonClickedArray[currentIndex])
        {
            selectButton.interactable = true;
            buyButtonList[currentIndex].gameObject.SetActive(false);
        }

        // Set select button interactable state based on selected element index
        if (currentIndex == selectedElementIndex)
        {
            selectButton.interactable = false;
        }
        else if (buyButtonClickedArray[currentIndex])
        {
            selectButton.interactable = true;
        }
    }

    public void Next()
    {
        currentIndex++;
        if (currentIndex >= prefabObjects.Length)
        {
            currentIndex = 0;
        }
        EnableCurrentObject();
        EnableCurrentButton();
    }

    public void Previous()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = prefabObjects.Length - 1;
        }
        EnableCurrentObject();
        EnableCurrentButton();
    }

    public void Select()
    {
        PlaySelectSound();
        PlayerPrefs.SetInt("SelectedElementIndex", currentIndex);
        selectedElementIndex = currentIndex; // Update the selected element index
        selectButton.interactable = false; // Set select button interactable state to false
    }

    private void PlaySelectSound()
    {
        AudioSource.PlayClipAtPoint(selectClickSound, Camera.main.transform.position);
    }

    public void Buy()
    {
        int selectedElementPrice = GetElementPrice(currentIndex);
        int currentCoinAmount = GetCoinAmount();
        if (currentCoinAmount >= selectedElementPrice)
        {
            selectButton.interactable = true;
            goTestButton.interactable = true;

            DecreaseCoinAmount(selectedElementPrice);
            UpdateCoinText();

            buyButtonClickedArray[currentIndex] = true;
            buyButtonList[currentIndex].gameObject.SetActive(false);

            PlayerPrefs.SetInt("BuyButtonClicked_" + currentIndex, 1);
            PlayerPrefs.SetInt("DisabledBuyButtonIndex", currentIndex);
            PlayerPrefs.SetInt("BuyButtonClickedArray_" + currentIndex, 1);
            PlayButtonClickSound(currentIndex);
        }
        else
        {
            Debug.Log("Yetersiz coin miktarý!");
        }
    }

    private void PlayButtonClickSound(int index)
    {
        if (index >= 0 && index < buttonClickSounds.Length)
        {
            AudioSource.PlayClipAtPoint(buttonClickSounds[index], Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Ses dosyasý dizisi indeksi geçersiz!");
        }
    }

    public void GoTest()
    {
        SkinsAnimator.SetTrigger("Close");
        Invoke("GoTestMainMenu", 1f);
    }

    private void GoTestMainMenu()
    {
        SceneManager.LoadScene("MAIN MENU");
    }

    private void EnableCurrentButton()
    {
        foreach (Button button in buyButtonList)
        {
            button.gameObject.SetActive(false);
        }
        buyButtonList[currentIndex].gameObject.SetActive(true);
    }

    private void EnableCurrentObject()
    {
        foreach (GameObject obj in prefabObjects)
        {
            obj.SetActive(false);
        }
        prefabObjects[currentIndex].SetActive(true);
    }

    private int GetElementPrice(int index)
    {
        if (index >= 0 && index < elementPrices.Length)
        {
            return elementPrices[index];
        }
        else
        {
            Debug.LogWarning("Geçersiz element index'i!");
            return 0;
        }
    }

    private int GetCoinAmount()
    {
        return PlayerPrefs.GetInt("TotalCollectedCoins", 0);
    }

    private void DecreaseCoinAmount(int amount)
    {
        int currentCoinAmount = GetCoinAmount();
        currentCoinAmount -= amount;
        PlayerPrefs.SetInt("TotalCollectedCoins", currentCoinAmount);
    }

    private void UpdateCoinText()
    {
        if (totalCoinText != null)
        {
            int totalCoins = GetCoinAmount();
            totalCoinText.text = " " + totalCoins.ToString();
        }
    }
}
