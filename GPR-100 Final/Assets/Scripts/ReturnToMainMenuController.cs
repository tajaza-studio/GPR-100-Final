using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToMainMenuController : MonoBehaviour
{
    public void Update() {
        if (Input.GetButtonDown("Select")) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
