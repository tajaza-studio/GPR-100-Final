using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToMainMenuController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
