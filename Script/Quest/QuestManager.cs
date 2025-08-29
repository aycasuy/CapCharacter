using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Görev Tanımı")]
    public List<Quest> quests;

    [Header("UI")]
    public GameObject questItemPrefab;    
    public Transform questListParent;     

    [Header("Karakter Animasyonu")]
    public Animator characterAnimator;    
    public float jumpAnimationDuration = 1f; // Zıplama animasyonu süresi
    public GameObject demoEndPanel; 


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        quests.Add(new Quest
        {
            questName = "Tüm Elmasları Topla",
            currentAmount = 0,
            targetAmount = 13,
            isCompleted = false,
            requiresStealth = false
        });

        // Yeni görevleri manuel olarak ekle
        quests.Add(new Quest
        {
            questName = "2 Tane Kilitli Elmas Aç",
            currentAmount = 0,
            targetAmount = 2,
            isCompleted = false,
            requiresStealth = false
        });
        
        foreach (Quest quest in quests)
        {
            GameObject go = Instantiate(questItemPrefab, questListParent);
            QuestItem qi = go.GetComponent<QuestItem>();
            qi.SetText($"{quest.questName}: {quest.currentAmount}/{quest.targetAmount}", quest.isCompleted);
            quest.uiItem = qi;
        }
    }

    public void AddProgress(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName && !q.isCompleted);
        if (quest != null)
        {
            quest.currentAmount++;
            if (quest.currentAmount >= quest.targetAmount)
            {
                quest.isCompleted = true;
                GiveReward(quest);
                

                // Tüm görevler tamamlandı mı kontrol
                if (AllQuestsCompleted())
                {
                    TriggerHappyJump();
                }
            }
            
            if (quest.uiItem != null)
                quest.uiItem.SetText($"{quest.questName}: {quest.currentAmount}/{quest.targetAmount}", quest.isCompleted);
        }
    }

    void GiveReward(Quest quest)
    {
        DiamondManager.Instance.AddDiamond(quest.requiresStealth ? 5 : 2);
    }

    //  fonksiyon
    bool AllQuestsCompleted()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.isCompleted) return false;
        }
        return true;
    }
    public int GetCompletedQuestCount()
    {
        return quests.FindAll(q => q.isCompleted).Count;
    }
     void ShowDemoEndPanel()
  {
    if (demoEndPanel != null)
    {
        demoEndPanel.SetActive(true);
    }
}

    
    void TriggerHappyJump()
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("HappyJump"); 
            Invoke(nameof(StopJumpAnimation), jumpAnimationDuration); // Belirli süre sonra durdur
            Invoke(nameof(ShowDemoEndPanel), jumpAnimationDuration + 0.0f); // Paneli göster
        }
    }

    void StopJumpAnimation()
    {
        characterAnimator.SetTrigger("Idle"); // Varsayılan animasyona dön
    }
    
  


    
}




   