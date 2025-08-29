public enum QuestType { Collect, Stealth }

[System.Serializable]
public class Quest
{
    public string questName;
    public QuestType questType;
    public int targetAmount;
    public int currentAmount;
    public bool isCompleted;
    public bool requiresStealth;

    [System.NonSerialized] public QuestItem uiItem;
}
