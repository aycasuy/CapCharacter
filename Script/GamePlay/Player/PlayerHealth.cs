
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    private StateControl _stateControl;

    void Start()
    {
        _stateControl = GetComponent<StateControl>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            
            TriggerRestart();
        }
    }

    void TriggerRestart()
    {
        int completedCount = QuestManager.Instance.GetCompletedQuestCount();
        
        
        
       

        RestartManager.Instance.ShowRestartScreen(completedCount);
    }

    void Die()
    {
        
        StartCoroutine(DieSequence());
    }

    System.Collections.IEnumerator DieSequence()
    {
        if (_stateControl != null)
        {
            _stateControl.SetState(PlayerState.Die);
        }

        yield return new WaitForSeconds(4f);

         int completedCount = QuestManager.Instance.GetCompletedQuestCount();


         RestartManager.Instance.ShowRestartScreen(completedCount);
      
    }
}