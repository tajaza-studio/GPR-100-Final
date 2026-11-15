using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public void Update() {
        if (Input.GetButtonDown("Select")) {
            GameInfo.loadFight();
            //SceneManager.LoadScene("Fight1");
        }
    }
}
