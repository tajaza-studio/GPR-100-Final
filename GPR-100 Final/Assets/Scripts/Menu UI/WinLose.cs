using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public void Update() {
        if (Input.GetButtonDown("Select")) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
