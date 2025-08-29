
using TMPro;
using UnityEngine;

public class QuestTimer : MonoBehaviour
{
    public float timeLimit = 10f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    private bool isRunning = true;
    public StateControl playerStateControl;

    void Start()
    {
        currentTime = timeLimit;
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0)
        {
            isRunning = false;
            EndQuestPhase();
        }
    }

    void EndQuestPhase()
    {
        
        StartCoroutine(EndSequence());
    }

    System.Collections.IEnumerator EndSequence()
    {
        if (playerStateControl != null)
        {
            playerStateControl.SetState(PlayerState.Die);
        }

        yield return new WaitForSeconds(2f);

        int completedCount = QuestManager.Instance.GetCompletedQuestCount();
        
        
        string username = UsernameManager.Instance.CurrentUsername;
      

        RestartManager.Instance.ShowRestartScreen(completedCount);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}

