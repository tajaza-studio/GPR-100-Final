
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToFirstFight()
    {
        //SceneManager.LoadScene("Fight1");
        SceneManager.LoadScene("TestingScene"); // this can be deleted
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
