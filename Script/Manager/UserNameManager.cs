using UnityEngine;
using TMPro;

public class UsernameManager : MonoBehaviour
{
    public static UsernameManager Instance;

    public GameObject usernamePanel;
    public TMP_InputField usernameInput;
    public GameObject player;

    public string CurrentUsername { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        string inputName = usernameInput.text.Trim();

        if (!string.IsNullOrEmpty(inputName))
        {
            CurrentUsername = inputName;
           
            usernamePanel.SetActive(false);

            // Oyuncunun hareketini aç
        player.GetComponent<PlayerController>().isInputLocked = false;
        }
    }

    void Start()
    {
       // Her oyun başında usernamePanel aktif olsun
       usernamePanel.SetActive(true);
    }
}
