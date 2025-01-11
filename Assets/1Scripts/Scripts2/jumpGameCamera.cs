using UnityEngine;

public class jumpGameCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform followTarget; // Kameran�n takip edece�i nesne (�rne�in, bir hedef nokta)
    public GameObject[] prefabObjects; // Prefab objelerin listesi

    private Transform selectedElementTransform; // Se�ilen elementin transformu
    private bool isZoomingOut = false; // Kameran�n uzakla�t�rma i�lemi devam ediyor mu?

    void Start()
    {
        // PlayerPrefs'ten se�ilen elementin index'ini al
        int selectedIndex = PlayerPrefs.GetInt("SelectedElementIndex", 0);

        // Se�ilen elementin transformunu al
        if (selectedIndex >= 0 && selectedIndex < prefabObjects.Length)
        {
            selectedElementTransform = prefabObjects[selectedIndex].transform;
        }
        else
        {
            Debug.LogWarning("Se�ilen elementin index'i ge�erli de�il veya prefabObjects dizisinde yok.");
        }
    }

    void Update()
    {
        if (selectedElementTransform != null)
        {
            // Sadece pozitif y eksenindeki hareketleri izle
            if (selectedElementTransform.position.y > transform.position.y)
            {
                // Hedef pozisyonu se�ilen elementin pozisyonu ve belirlenen y�kseklik ofseti olarak ayarla
                Vector3 targetPosition = new Vector3(selectedElementTransform.position.x, selectedElementTransform.position.y + yOffset, -10f);

                // Kamera pozisyonunu yumu�ak bir �ekilde g�ncelle
                transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.unscaledDeltaTime);
            }

            // Uzakla�t�rma i�lemi devam ediyorsa, kameran�n boyutunu k���lt
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
                // Hedefin pozisyonunu al ve ona do�ru hareket et
                Vector3 targetPosition = new Vector3(followTarget.position.x, followTarget.position.y + yOffset, -10f);
                transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.unscaledDeltaTime);
            }
        }
        else
        {
            // Hem se�ilen element hem de takip edilecek hedef yoksa, kamera konumunu sabit tut
            return;
        }
    }

    public void ZoomOut()
    {
        isZoomingOut = true;
    }
}
