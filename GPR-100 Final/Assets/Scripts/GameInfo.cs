using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameInfo
{
    public static int fightNum = 0;
    public static void loadFight() {
        switch (fightNum) {
            case 0:
                fightNum++;
                SceneManager.LoadScene("Fight1");
                break;
            case 1:
                fightNum++;
                SceneManager.LoadScene("Fight2");
                break;
            case 2:
                fightNum++;
                SceneManager.LoadScene("Fight3");
                break;
            case 3:
                fightNum++;
                SceneManager.LoadScene("Fight4");
                break;
            case 4:
                fightNum = 0;
                SceneManager.LoadScene("Win");
                break;
        }
    }

    public static void loseFight() {
        fightNum = 0;
        SceneManager.LoadScene("Lose");
    }
}
