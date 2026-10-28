//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] BattleSystem battleSystem;

    private SpriteRenderer spriteRenderer;

    public int skillHovered = 0;

    SelectionState selectionState;

    private enum SelectionState {
        SKILL,
        ENEMY,
        ALL_ENEMY,
        ALLY,
        ALL_ALLY,
        PRIEST,
        MC,
        FIGHTER
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        selectionState = SelectionState.SKILL;
    }

    void Update()
    {
        switch (skillHovered) {
            case (0):
                transform.position = new Vector3(3f, -1f, 0f);
                break;
            case (1):
                transform.position = new Vector3(3f, -2.5f, 0f);
                break;
            case (2):
                transform.position = new Vector3(3f, -4f, 0f);
                break;
        }
        if (battleSystem.state == BattleSystem.State.BUSY) {
            skillHovered = 0;
            spriteRenderer.enabled = false;
            return;
        }
        spriteRenderer.enabled = true;

        switch (selectionState) {
            case (SelectionState.SKILL):
                if (Input.GetKeyDown(KeyCode.W)) {
                    skillHovered = (skillHovered + 2) % 3;
                }
                if (Input.GetKeyDown(KeyCode.S)) {
                    skillHovered = (skillHovered + 1) % 3;
                }
                break;
            case (SelectionState.ENEMY):
                break;
            case (SelectionState.ALL_ENEMY):
                break;
            case (SelectionState.ALLY):
                break;
            case (SelectionState.ALL_ALLY):
                break;
            case (SelectionState.PRIEST):
                break;
            case (SelectionState.MC):
                break;
            case (SelectionState.FIGHTER):
                break;
        }
    }
}
