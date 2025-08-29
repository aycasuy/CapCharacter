using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    

  
    public void GoToMainMenu()
    {
        
        SceneManager.LoadScene("Main Menu");
    }
     public void OnExitButtonPressed()
    {
        Application.Quit();
        
    }
}
