using UnityEngine;
using UnityEngine.UI;

public class SliderMiniGame : MonoBehaviour
{
    public Slider slider;
    public RectTransform targetZone;
    public Button unlockButton;

    private Diamond targetDiamond;
    public float successRange = 10f;

    void Start()
    {
        unlockButton.onClick.AddListener(TryUnlock);
        unlockButton.gameObject.SetActive(false);
    }

    void Update()
    {
        float handleX = slider.handleRect.position.x;
        float targetX = targetZone.position.x;

        if (Mathf.Abs(handleX - targetX) < successRange)
        {
            unlockButton.gameObject.SetActive(true);
        }
        else
        {
            unlockButton.gameObject.SetActive(false);
        }
    }

    public void SetTargetDiamond(Diamond diamond)
    {
        targetDiamond = diamond;
        slider.value = 0.0f; //baştan başlasın
    }

    private void TryUnlock()
    {
        if (targetDiamond != null)
        {
            targetDiamond.UnlockDiamond();
            MiniGamePanelController.Instance.HideMiniGame();
        }
    }
}
