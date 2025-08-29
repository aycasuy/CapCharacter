using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    public TextMeshProUGUI questText;

    public void SetText(string text, bool completed)
    {
        if (completed)
        {
            questText.text = "<s>" + text + "</s>"; // Üzeri çizili
            questText.color = Color.gray;
        }
        else
        {
            questText.text = text;
            questText.color = Color.white;
        }
    }
}
