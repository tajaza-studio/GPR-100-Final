using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Transform playButton;
    [SerializeField] Transform settingsButton;
    [SerializeField] Transform creditsButton;
    [SerializeField] Transform quitButton;

    int optionHovered = 0;
    public void Start() {
        transform.position = playButton.position;
    }
    public void Update() {
        switch (optionHovered) {
            case 0:
                transform.position = playButton.position;
                if (Input.GetButtonDown("Select")) {
                    SceneManager.LoadScene("Tutorial");
                }
                break;
            case 1:
                transform.position = settingsButton.position;
                if (Input.GetButtonDown("Select")) {
                    Debug.Log("no settings yet");
                }
                break;
            case 2:
                transform.position = creditsButton.position;
                if (Input.GetButtonDown("Select")) {
                    SceneManager.LoadScene("Credits");
                }
                break;
            case 3:
                transform.position = quitButton.position;
                if (Input.GetButtonDown("Select")) {
                    Application.Quit();
                    Debug.Log("Quit");
                }
                break;
        }

        if (Input.GetButtonDown("Up")) {
            optionHovered = (optionHovered + 3) % 4;
        }
        if (Input.GetButtonDown("Down")) {
            optionHovered = (optionHovered + 1) % 4;
        }
    }
}
