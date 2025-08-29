using UnityEngine;
public class Diamond : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSound;

    public bool isLocked = false;
    public GameObject lockedEffect;

    private void Start()
    {
        if (isLocked && lockedEffect != null)
        {
            lockedEffect.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; // Sadece oyuncuya temas etmesi gerekiyor

        if (isLocked)
        {
            // Mini oyunu başlat
            MiniGamePanelController.Instance.ShowMiniGame(this);
            return;
        }

        Collect();
    }

    public void UnlockDiamond()
    {
        isLocked = false;
        if (lockedEffect != null)
        {
            lockedEffect.SetActive(false);
        }


        QuestManager.Instance?.AddProgress("2 Tane Kilitli Elmas Aç");
        Collect();
        

    }

    private void Collect()
    {
        DiamondManager.Instance.AddDiamond(value);

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddProgress("3 Elmas Topla");
            QuestManager.Instance.AddProgress("Tüm Elmasları Topla");
        }

        Destroy(gameObject);
    }
}




   


