using UnityEngine;

public class jumpGameCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform followTarget; // Kameranýn takip edeceði nesne (örneðin, bir hedef nokta)
    public GameObject[] prefabObjects; // Prefab objelerin listesi

    private Transform selectedElementTransform; // Seçilen elementin transformu
    private bool isZoomingOut = false; // Kameranýn uzaklaþtýrma iþlemi devam ediyor mu?

    void Start()
    {
        // PlayerPrefs'ten seçilen elementin index'ini al
        int selectedIndex = PlayerPrefs.GetInt("SelectedElementIndex", 0);

        // Seçilen elementin transformunu al
        if (selectedIndex >= 0 && selectedIndex < prefabObjects.Length)
        {
            selectedElementTransform = prefabObjects[selectedIndex].transform;
        }
        else
        {
            Debug.LogWarning("Seçilen elementin index'i geçerli deðil veya prefabObjects dizisinde yok.");
        }
    }

    void Update()
    {
        if (selectedElementTransform != null)
        {
            // Sadece pozitif y eksenindeki hareketleri izle
            if (selectedElementTransform.position.y > transform.position.y)
            {
                // Hedef pozisyonu seçilen elementin pozisyonu ve belirlenen yükseklik ofseti olarak ayarla
                Vector3 targetPosition = new Vector3(selectedElementTransform.position.x, selectedElementTransform.position.y + yOffset, -10f);

                // Kamera pozisyonunu yumuþak bir þekilde güncelle
                transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.unscaledDeltaTime);
            }

            // Uzaklaþtýrma iþlemi devam ediyorsa, kameranýn boyutunu küçült
            if (isZoomingOut)
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5f, FollowSpeed * Time.unscaledDeltaTime);
            }
        }
        else if (followTarget != null)
        {
            // Takip edilecek hedef varsa, sadece pozitif y eksenindeki hareketleri izle
            if (followTarget.position.y > transform.position.y)
            {
                // Hedefin pozisyonunu al ve ona doðru hareket et
                Vector3 targetPosition = new Vector3(followTarget.position.x, followTarget.position.y + yOffset, -10f);
                transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.unscaledDeltaTime);
            }
        }
        else
        {
            // Hem seçilen element hem de takip edilecek hedef yoksa, kamera konumunu sabit tut
            return;
        }
    }

    public void ZoomOut()
    {
        isZoomingOut = true;
    }
}
