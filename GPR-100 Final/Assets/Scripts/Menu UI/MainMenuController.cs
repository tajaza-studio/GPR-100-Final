using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Transform playButton;
    [SerializeField] Transform settingsButton;
    [SerializeField] Transform creditsButton;
    [SerializeField] Transform quitButton;
    [SerializeField] AudioSource playSound;
    bool isTransitioning = false;

    int optionHovered = 0;
    public void Start() {
        transform.position = playButton.position;
    }

    public IEnumerator OnPlaySelect(string Level)
    {
        playSound.Play(0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Level);
    }

    public void Update() {
        switch (optionHovered) {
            case 0:
                transform.position = playButton.position;
                if (Input.GetButtonDown("Select") && ! isTransitioning) {
                    isTransitioning = true;
                    StartCoroutine(OnPlaySelect("Tutorial"));  
                }
                break;
            case 1:
                transform.position = settingsButton.position;
                if (Input.GetButtonDown("Select") && !isTransitioning) {
                    isTransitioning = true;
                    Debug.Log("no settings yet");
                    StartCoroutine(OnPlaySelect("Settings"));
                }
                break;
            case 2:
                transform.position = creditsButton.position;
                if (Input.GetButtonDown("Select") && !isTransitioning) {
                    isTransitioning = true;
                    StartCoroutine(OnPlaySelect("Credits"));
                }
                break;
            case 3:
                transform.position = quitButton.position;
                if (Input.GetButtonDown("Select") && !isTransitioning) {
                    isTransitioning = true;
                    Application.Quit();
                    Debug.Log("Quit");
                }
                break;
        }

        if (Input.GetButtonDown("Up") && ! isTransitioning) {
            optionHovered = (optionHovered + 3) % 4;
        }
        if (Input.GetButtonDown("Down") && ! isTransitioning) {
            optionHovered = (optionHovered + 1) % 4;
        }
    }
}
