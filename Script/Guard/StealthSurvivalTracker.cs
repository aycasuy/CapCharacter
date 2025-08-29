using UnityEngine;

public class StealthSurvivalTracker : MonoBehaviour
{
    public float requiredTime = 15f;
    private float timer = 0f;
    private bool isSpotted = false;
    private bool questCompleted = false;

    void Update()
    {
        if (questCompleted) return;

        if (isSpotted)
        {
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= requiredTime)
            {
                QuestManager.Instance.AddProgress("Guard’a Yakalanmadan 15 Saniye Dolaş");
                questCompleted = true;
            }
        }
    }

    public void SetSpotted(bool value)
    {
        isSpotted = value;
    }
}
