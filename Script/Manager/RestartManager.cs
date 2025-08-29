
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class RestartManager : MonoBehaviour
{
    public static RestartManager Instance;

    [Header("UI Elemanları")]
    public GameObject restartPanel;
    public TextMeshProUGUI scoreText;

   

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        Time.timeScale = 1f;
    }

    
    public void ShowRestartScreen(int completedQuests)
    {
        Time.timeScale = 0f;

        QuestTimer timer = FindAnyObjectByType<QuestTimer>();
        if (timer != null)
        {
            timer.StopTimer();
        }


        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.SetGameOver(true);
        }
        restartPanel.SetActive(true);
         if (scoreText != null)
        {
            // Mevcut oyuncunun adını al
            string username = UsernameManager.Instance.CurrentUsername;
            
            // Tamamlanan görev sayısını al
            string summary = $"Player: {username}\nCompleted Quests: {completedQuests}";
            
            // Skor metnini güncelle
            scoreText.text = summary;
        }
      

       
    }
        
      
    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void CopyScoreToClipboard()
    {
        GUIUtility.systemCopyBuffer = scoreText.text;
        
    }

   
}