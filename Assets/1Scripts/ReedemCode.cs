using UnityEngine;
using UnityEngine.UI;

public class ReedemCode : MonoBehaviour
{
    public GameObject reedemCodePanel;
    public InputField codeInputField;
    public GameObject codeInputField0;
    public Text debugLogText;
    public Image parentImage; // Image objesinin referans�
    public Animator ReedemCodeAnimator;
    public Animator InputFieldAnimator;

    public static string[] validCodes = { "QUACKVENTURE1K", "QUACK", "RUNQUACKRUN" };

    private bool[] codeUsed = new bool[validCodes.Length]; // Kodlar�n kullan�l�p kullan�lmad���n� takip etmek i�in

    private void Start()
    {
        Animator InputFieldAnimator = GetComponent<Animator>();
        Animator ReedemCodeAnimator = GetComponent<Animator>();
        // Ba�lang��ta reedemCodePanel'i gizle
        reedemCodePanel.SetActive(false);
        codeInputField0.SetActive(false);
        // PlayerPrefs'ten kodlar�n kullan�m durumunu y�kle
        for (int i = 0; i < validCodes.Length; i++)
        {
            codeUsed[i] = PlayerPrefs.GetInt("CodeUsed_" + i, 0) == 1;
        }
    }


    public void OpenReedemCodePanel()
    {
        InputFieldAnimator.SetTrigger("Open");
        ReedemCodeAnimator.SetTrigger("Open");
        // Reedem Code panelini a�
        reedemCodePanel.SetActive(true);
        codeInputField0.SetActive(true);
    }

    public void CloseReedemCodePanel()
    {
        InputFieldAnimator.SetTrigger("Close");
        ReedemCodeAnimator.SetTrigger("Close");
        Invoke("ClosePanelAnim", 0.5f);
    }
    private void ClosePanelAnim()
    {
        // Reedem Code panelini kapat
        reedemCodePanel.SetActive(false);
        codeInputField0.SetActive(false);
    }

    public void Reedem()
    {
        // Kullan�c� taraf�ndan girilen kodu al
        string enteredCode = codeInputField.text;

        bool isValidCode = false;
        int codeIndex = -1;
        for (int i = 0; i < validCodes.Length; i++)
        {
            if (enteredCode == validCodes[i] && !codeUsed[i])
            {
                isValidCode = true;
                codeIndex = i;
                break;
            }
        }

        // Ge�erli kod mu kontrol et
        if (isValidCode)
        {
            // �d�l al�nmad�ysa devam et
            if (!codeUsed[codeIndex])
            {
                // �d�l ver
                GiveReward(codeIndex);

                // Debug log ve Text gameobjesini g�ncelle
                Debug.Log("You Won the Prize!");
                debugLogText.text = "You Won the Prize!";

                // Kullan�lan kodu i�aretle
                codeUsed[codeIndex] = true;

                // Kullan�lan kodun PlayerPrefs'e kaydedilmesi
                PlayerPrefs.SetInt("CodeUsed_" + codeIndex, 1);
                PlayerPrefs.Save(); // PlayerPrefs'e kaydet

                // Parent Image'�n SetActive durumunu true yap
                if (parentImage != null)
                {
                    parentImage.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // Yanl�� kod hatas�
            Debug.Log("Incorrect code or has been used before");
            debugLogText.text = "Incorrect code or has been used before";

            // Parent Image'�n SetActive durumunu false yap
            if (parentImage != null)
            {
                parentImage.gameObject.SetActive(true);
            }
        }
    }

    private void GiveReward(int codeIndex)
    {
        // �d�l� vermek i�in buraya �d�l verme kodlar�n� ekleyebilirsiniz
        // �rne�in, her kod i�in farkl� �d�l miktarlar� verebilirsiniz
        int rewardAmount = 1000; // �d�l miktar�
        // TotalCoinTextUpdater script'ine eri�im sa�layarak �d�l miktar�n� g�ncelleyebilirsiniz
        TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
        if (coinUpdater != null)
        {
            coinUpdater.GiveCoinReward(rewardAmount);
        }
    }
}
