using UnityEngine;
using UnityEngine.UI;

public class ReedemCode : MonoBehaviour
{
    public GameObject reedemCodePanel;
    public InputField codeInputField;
    public GameObject codeInputField0;
    public Text debugLogText;
    public Image parentImage; // Image objesinin referansý
    public Animator ReedemCodeAnimator;
    public Animator InputFieldAnimator;

    public static string[] validCodes = { "QUACKVENTURE1K", "QUACK", "RUNQUACKRUN" };

    private bool[] codeUsed = new bool[validCodes.Length]; // Kodlarýn kullanýlýp kullanýlmadýðýný takip etmek için

    private void Start()
    {
        Animator InputFieldAnimator = GetComponent<Animator>();
        Animator ReedemCodeAnimator = GetComponent<Animator>();
        // Baþlangýçta reedemCodePanel'i gizle
        reedemCodePanel.SetActive(false);
        codeInputField0.SetActive(false);
        // PlayerPrefs'ten kodlarýn kullaným durumunu yükle
        for (int i = 0; i < validCodes.Length; i++)
        {
            codeUsed[i] = PlayerPrefs.GetInt("CodeUsed_" + i, 0) == 1;
        }
    }


    public void OpenReedemCodePanel()
    {
        InputFieldAnimator.SetTrigger("Open");
        ReedemCodeAnimator.SetTrigger("Open");
        // Reedem Code panelini aç
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
        // Kullanýcý tarafýndan girilen kodu al
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

        // Geçerli kod mu kontrol et
        if (isValidCode)
        {
            // Ödül alýnmadýysa devam et
            if (!codeUsed[codeIndex])
            {
                // Ödül ver
                GiveReward(codeIndex);

                // Debug log ve Text gameobjesini güncelle
                Debug.Log("You Won the Prize!");
                debugLogText.text = "You Won the Prize!";

                // Kullanýlan kodu iþaretle
                codeUsed[codeIndex] = true;

                // Kullanýlan kodun PlayerPrefs'e kaydedilmesi
                PlayerPrefs.SetInt("CodeUsed_" + codeIndex, 1);
                PlayerPrefs.Save(); // PlayerPrefs'e kaydet

                // Parent Image'ýn SetActive durumunu true yap
                if (parentImage != null)
                {
                    parentImage.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // Yanlýþ kod hatasý
            Debug.Log("Incorrect code or has been used before");
            debugLogText.text = "Incorrect code or has been used before";

            // Parent Image'ýn SetActive durumunu false yap
            if (parentImage != null)
            {
                parentImage.gameObject.SetActive(true);
            }
        }
    }

    private void GiveReward(int codeIndex)
    {
        // Ödülü vermek için buraya ödül verme kodlarýný ekleyebilirsiniz
        // Örneðin, her kod için farklý ödül miktarlarý verebilirsiniz
        int rewardAmount = 1000; // Ödül miktarý
        // TotalCoinTextUpdater script'ine eriþim saðlayarak ödül miktarýný güncelleyebilirsiniz
        TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
        if (coinUpdater != null)
        {
            coinUpdater.GiveCoinReward(rewardAmount);
        }
    }
}
