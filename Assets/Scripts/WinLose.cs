using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
