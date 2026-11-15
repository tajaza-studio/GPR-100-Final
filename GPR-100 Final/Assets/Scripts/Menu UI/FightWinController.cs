using UnityEngine;
using UnityEngine.SceneManagement;

public class FightWinController : MonoBehaviour
{
    public void Update() {
        if (Input.GetButtonDown("Select")) {
            GameInfo.loadFight();
        }
    }
}
