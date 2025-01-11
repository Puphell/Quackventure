using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateAfterDelay : MonoBehaviour
{
    public GameObject targetObject;  // Aktif hale getirilecek obje
    public GameObject disableObject;
    public Text timerText;           // Kalan süreyi gösterecek Text objesi
    public float delay = 15f;        // Gecikme süresi (saniye)

    private float remainingTime;     // Kalan süre

    private void Start()
    {
        remainingTime = delay;       // Kalan süreyi baþlangýçta ayarla
        targetObject.SetActive(false);  // Hedef objeyi baþlangýçta devre dýþý býrak
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  // Kalan süreyi güncelle
            timerText.text = Mathf.Ceil(remainingTime).ToString();  // Kalan süreyi text'e yaz
        }
        else
        {
            remainingTime = 0;  // Kalan süre sýfýrýn altýna düþmesin
            timerText.text = "0";  // Kalan süre sýfýr olduðunda "0" yaz
        }
    }

    private IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        targetObject.SetActive(true);  // Hedef objeyi aktif hale getir
        disableObject.SetActive(false);
    }
}
