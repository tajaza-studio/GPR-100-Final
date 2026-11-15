using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public void Update() {
        if (Input.GetButtonDown("Select")) {
            SceneManager.LoadScene("TestingScene");
            //SceneManager.LoadScene("Fight1");
        }
    }
}
