using UnityEngine;
using UnityEngine.UI;

public class DiamondManager : MonoBehaviour
{
    public static DiamondManager Instance;
    public int diamondCount = 0;
    public Text diamondText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddDiamond(int amount)
    {
        diamondCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("Elmas: " + diamondCount);
    }
}



