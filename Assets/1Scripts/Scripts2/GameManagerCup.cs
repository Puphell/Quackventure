using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Interstitial; // UI elemanlarýný kullanmak için gerekli

namespace Interstitial
{
    public class GameManager : MonoBehaviour
    {
        public GameObject coinPrizeTextObject;

        public CupGameShowAd cupGameShowAd;

        public GameObject LosePanel;
        public Text statusText; // Status text'i referans olarak ekleyin

        public int swapCount = 4; // Baþlangýçta cup'larýn deðiþme sayýsý
        bool clicked = false; // Týklamanýn yapýldýðýný takip etmek için bir deðiþken
        public List<GameObject> cups;
        public GameObject ball;
        public float showBallDuration = 2f; // Topu gösterme süresi
        public float mixDuration = 5f; // Bardaklarý karýþtýrma süresi
        public float cupMoveDistance = 1f; // Bardaklarýn baþlangýçta yukarý hareket etme mesafesi
        public float swapSpeed = 1f; // Bardaklarýn yer deðiþtirirken hýzý
        public float ballInitialYPosition = 1.62f; // Ball gameobject'inin baþlangýç yüksekliði

        private bool gameEnded = false;
        private bool mixingCups = false;
        private Transform initialCup; // Ball'ýn baþlangýçta bulunduðu cup

        void Start()
        {
            if (ball == null)
            {
                Debug.LogError("Ball is not assigned in the inspector!");
                return;
            }

            if (cups == null || cups.Count == 0)
            {
                Debug.LogError("Cups are not assigned in the inspector!");
                return;
            }

            if (statusText == null)
            {
                Debug.LogError("StatusText is not assigned in the inspector!");
                return;
            }

            GetComponent<CupGameShowAd>().LoadInterstitialAd();

            PlaceBall();
            StartCoroutine(StartGameRoutine());
        }

        void PlaceBall()
        {
            int randomIndex = Random.Range(0, cups.Count);
            GameObject selectedCup = cups[randomIndex];
            initialCup = selectedCup.transform;

            if (selectedCup == null)
            {
                Debug.LogError("Selected cup is null. Check your cups list.");
                return;
            }
            ball.transform.SetParent(selectedCup.transform);
            ball.transform.localPosition = Vector3.zero;

            // Ball'ýn yüksekliðini ayarla
            ball.transform.localPosition = new Vector3(ball.transform.localPosition.x, ballInitialYPosition, ball.transform.localPosition.z);
        }

        IEnumerator StartGameRoutine()
        {
            // Bardaklarý yukarý hareket ettir
            yield return StartCoroutine(MoveCups(Vector3.down * cupMoveDistance, 1f, false));
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            yield return StartCoroutine(MoveCups(Vector3.up * cupMoveDistance, 1f, false));

            // Belirli bir süre topu göster
            yield return new WaitForSeconds(showBallDuration);

            // Bardaklarý karýþtýr
            yield return StartCoroutine(MixCups());

            // Oyuncunun dokunuþunu bekle
            yield return StartCoroutine(WaitForPlayerInput());
        }

        IEnumerator WaitForPlayerInput()
        {
            // Oyuncunun týklama yapabileceði zamaný belirtiyoruz
            statusText.text = "Ready";

            while (!gameEnded && !clicked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Cup"))
                    {
                        clicked = true; // Týklama yapýldý
                        CheckCup(hit.collider.gameObject);
                    }
                }
                yield return null;
            }
        }

        IEnumerator MoveCups(Vector3 direction, float duration, bool liftBall)
        {
            float elapsedTime = 0f;
            Vector3 ballStartPos = ball.transform.position;
            while (elapsedTime < duration)
            {
                foreach (var cup in cups)
                {
                    if (cup == null)
                    {
                        Debug.LogError("Cup is null. Check your cups list.");
                        continue;
                    }
                    cup.transform.Translate(direction * (Time.deltaTime / duration));
                }

                if (!liftBall)
                {
                    // Ball'ýn y konumunu sabit tut
                    ball.transform.position = new Vector3(ball.transform.position.x, ballInitialYPosition, ball.transform.position.z);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator MixCups()
        {
            statusText.text = "Cups mixing"; // Bardaklar karýþtýrýlýyor mesajý

            mixingCups = true;
            float elapsedTime = 0f;
            int currentSwapCount = swapCount;

            while (elapsedTime < mixDuration)
            {
                for (int i = 0; i < currentSwapCount; i++)
                {
                    // Ýki bardak seç ve yerlerini deðiþtir
                    int cupIndex1 = Random.Range(0, cups.Count);
                    int cupIndex2 = Random.Range(0, cups.Count);

                    while (cupIndex2 == cupIndex1)
                    {
                        cupIndex2 = Random.Range(0, cups.Count);
                    }

                    GameObject cup1 = cups[cupIndex1];
                    GameObject cup2 = cups[cupIndex2];

                    if (cup1 != null && cup2 != null)
                    {
                        yield return StartCoroutine(SwapCups(cup1, cup2));
                    }
                }

                elapsedTime += swapSpeed * currentSwapCount;
                currentSwapCount++;
            }
            mixingCups = false;

            // Bardaklar karýþtýrýldýktan sonra oyuncunun dokunuþunu bekle
            yield return StartCoroutine(WaitForPlayerInput());
        }

        IEnumerator SwapCups(GameObject cup1, GameObject cup2)
        {
            Vector3 cup1StartPos = cup1.transform.position;
            Vector3 cup2StartPos = cup2.transform.position;

            float progress = 0f;

            while (progress <= 1f)
            {
                cup1.transform.position = Vector3.Lerp(cup1StartPos, cup2StartPos, progress);
                cup2.transform.position = Vector3.Lerp(cup2StartPos, cup1StartPos, progress);

                progress += Time.deltaTime * swapSpeed;
                yield return null;
            }

            // Yer deðiþtirme sonrasýnda kesin konumlara ayarlama
            cup1.transform.position = cup2StartPos;
            cup2.transform.position = cup1StartPos;
        }

        void CheckCup(GameObject selectedCup)
        {
            if (mixingCups || gameEnded)
            {
                return; // Karýþtýrma sýrasýnda veya oyun sona erdiðinde dokunmalarý kabul etme
            }

            if (selectedCup.transform.Find("Ball"))
            {
                Debug.Log("You found the ball! You win!");
                StartCoroutine(HandleCupSelectionWin(selectedCup));
            }
            else
            {
                Debug.Log("No ball under this cup. Try again!");
                StartCoroutine(HandleCupSelection(selectedCup));
            }
        }

        IEnumerator HandleCupSelectionWin(GameObject selectedCup)
        {
            coinPrizeTextObject.SetActive(true);
            Invoke("closeCoinPrizeTextObject", 1f);

            gameEnded = true; // Oyun bitti
                              // Bardaklarý yukarý hareket ettir
            yield return StartCoroutine(MoveCups(Vector3.down * cupMoveDistance, 1f, false));
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            yield return StartCoroutine(MoveCups(Vector3.up * cupMoveDistance, 1f, false));

            // MixCups fonksiyonunu tekrar çaðýr
            yield return StartCoroutine(MixCups());

            swapSpeed += 0.5f;
            swapCount += 1; // swapCount'u artýr

            // Týklama iznini geri getir
            clicked = false;
            gameEnded = false;

            int rewardAmount = 10; // Ödül miktarý
                                   // TotalCoinTextUpdater script'ine eriþim saðlayarak ödül miktarýný güncelleyebilirsiniz
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
            }

            yield return new WaitForSeconds(1f); // Tüm bardaklarýn yukarý çýkmasýný bekle

            // Oyuncunun dokunuþunu bekle
            yield return StartCoroutine(WaitForPlayerInput());
        }

        private void closeCoinPrizeTextObject()
        {
            coinPrizeTextObject.SetActive(false);
        }

        IEnumerator HandleCupSelection(GameObject selectedCup)
        {
            gameEnded = true; // Oyun bitti
                              // Bardaklarý yukarý hareket ettir
            yield return StartCoroutine(MoveCups(Vector3.down * cupMoveDistance, 1f, false));
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            yield return StartCoroutine(MoveCups(Vector3.up * cupMoveDistance, 1f, false));

            // Bu kontrol artýk CheckCup'ta yapýlýyor, burada tekrar yapýlmasýna gerek yok

            yield return new WaitForSeconds(1f); // Tüm bardaklarýn yukarý çýkmasýný bekle

            LosePanel.SetActive(true);

            GetComponent<CupGameShowAd>().ShowGameOverPanel();
        }

        public void RestartPanel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MiniGameCub");
        }

        public void BackMiniGames()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MiniGameLevelScene");
        }

        public void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MAIN MENU");
        }
    }
}