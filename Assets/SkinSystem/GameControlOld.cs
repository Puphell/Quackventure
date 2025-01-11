using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlOld : MonoBehaviour
{
    public GameObject[] characters;
    public Transform playerStartPosition;
    public string menuScene = "Character Selection Menu";
    private string selectedCharacterDataName = "SelectedCharacter";
    private int selectedCharacter;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
        playerObject = Instantiate(characters[selectedCharacter], playerStartPosition.position, characters[selectedCharacter].transform.rotation);

        // JumpButton'un nerede olduğunu bulmak için Canvas altındaki tüm butonları kontrol edelim
        Button[] buttons = playerObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.gameObject.name == "JumpButton") // JumpButton'un ismi bu şekilde varsayıldı
            {
                // JumpButton'a tıklandığında Move.Jump fonksiyonunu çağıran bir listener ekle
                button.onClick.AddListener(playerObject.GetComponent<Move>().Jump);
                break; // JumpButton bulundu, döngüyü sonlandır
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
