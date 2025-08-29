using UnityEngine;

public class MiniGamePanelController : MonoBehaviour
{
    public static MiniGamePanelController Instance;
    public GameObject panel;
    public SliderMiniGame sliderMiniGame;

    private void Awake()
    {
        Instance = this; //Kolay erişim için static 
        panel.SetActive(false);
    }

    public void ShowMiniGame(Diamond diamond)
    {
        sliderMiniGame.SetTargetDiamond(diamond);
        panel.SetActive(true);
    }

    public void HideMiniGame()
    {
        
        panel.SetActive(false);
    }
}
