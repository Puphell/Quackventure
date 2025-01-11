using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // SceneManager eklenmeli

public class DeathController : MonoBehaviour
{
    private bool gameIsOver = false;
    private Vector3 startPosition; // Başlangıç pozisyonu
    public GameObject gameOverCanvas;
    public Button restartButton;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Başlangıç pozisyonunu kaydet
        restartButton.gameObject.SetActive(false);

        // Yeniden başlatma butonuna tıklanınca RestartGame fonksiyonunu çağır
        restartButton.onClick.AddListener(RestartGame);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike") || other.CompareTag("Enemy"))
        {
            ShowGameOver();
        }
    }

    void Update()
    {
        if (gameIsOver)
        {
            return; // Oyun durduysa karakterin hareketini güncelleme
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Karakterin dönüşünü kontrol et
        if (horizontalInput > 0)
        {
            // Sağa gidiyorsa orijinal konumunu koru
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0)
        {
            // Sola gidiyorsa x ekseninde dön
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Oyunu başlatma kontrolü
        if (Input.GetButtonDown("Fire1") && !gameIsOver)
        {
            StartGame();
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        gameIsOver = true;
        gameOverCanvas.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        // Time.timeScale'ı 1'e geri getir, oyun devam etsin
        Time.timeScale = 1;
        // Restart buttonunu kapat
        restartButton.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Bu noktada sahne yeniden yüklendi, Player nesnesini etkinleştirmek ve başlangıç pozisyonuna yerleştirmek gerekiyor.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = startPosition; // Başlangıç pozisyonunu ayarla
        }
        else
        {
            Debug.LogError("Player nesnesi bulunamadı veya etkin değil.");
        }
    }



    void StartGame()
    {
        // Oyunu başlatma kodları
    }
}
