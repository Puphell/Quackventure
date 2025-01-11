using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateAfterDelay : MonoBehaviour
{
    public GameObject targetObject;  // Aktif hale getirilecek obje
    public GameObject disableObject;
    public Text timerText;           // Kalan s�reyi g�sterecek Text objesi
    public float delay = 15f;        // Gecikme s�resi (saniye)

    private float remainingTime;     // Kalan s�re

    private void Start()
    {
        remainingTime = delay;       // Kalan s�reyi ba�lang��ta ayarla
        targetObject.SetActive(false);  // Hedef objeyi ba�lang��ta devre d��� b�rak
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  // Kalan s�reyi g�ncelle
            timerText.text = Mathf.Ceil(remainingTime).ToString();  // Kalan s�reyi text'e yaz
        }
        else
        {
            remainingTime = 0;  // Kalan s�re s�f�r�n alt�na d��mesin
            timerText.text = "0";  // Kalan s�re s�f�r oldu�unda "0" yaz
        }
    }

    private IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        targetObject.SetActive(true);  // Hedef objeyi aktif hale getir
        disableObject.SetActive(false);
    }
}
