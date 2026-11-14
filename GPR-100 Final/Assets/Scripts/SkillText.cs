using TMPro;
using UnityEngine;

public class SkillText : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] BattleSystem battleSystem;

    [SerializeField] TMP_Text text;

    private void Update() {
        switch (battleSystem.state) {
            case BattleSystem.State.PRIEST_TURN:
                switch (uiController.skillHovered) {
                    case 0:
                        text.text = "Olive heals one ally for a medium amount.";
                        break;
                    case 1:
                        text.text = "Olive heals all allies for a small amount.";
                        break;
                    case 2:
                        text.text = "Olive bonks one enemy with her staff for small damage.";
                        break;
                }
                break;
            case BattleSystem.State.MC_TURN:
                switch (uiController.skillHovered) {
                    case 0:
                        text.text = "MC buffs one ally's next skill.";
                        break;
                    case 1:
                        text.text = "MC debuffs one enemy's next attack.";
                        break;
                    case 2:
                        text.text = "MC slashes one enemy with their sword for medium damage";
                        break;
                }
                break;
            case BattleSystem.State.FIGHTER_TURN:
                switch (uiController.skillHovered) {
                    case 0:
                        text.text = "Dracovich chops one enemy with his axe for big damage.";
                        break;
                    case 1:
                        text.text = "Dracovich cleaves all enemies with his axe for medium damage.";
                        break;
                    case 2:
                        text.text = "Dracovich takes a deep breath and heals himself.";
                        break;
                }
                break;
            case BattleSystem.State.BUSY:
                text.text = "";
                break;
        }
    }
}
