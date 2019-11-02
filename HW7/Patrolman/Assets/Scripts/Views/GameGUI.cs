using UnityEngine;

public class GameGUI : MonoBehaviour {
    private IUserAction action;
    private GameResult result;
    private int score;

    private void Start() {
        Restart();
    }

    private void OnGUI() {
        GUIStyle messageStyle = new GUIStyle();
        messageStyle.fontSize = 40;
        messageStyle.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(10, Screen.height / 2 - 300, 200, 100), "Score: " + score, messageStyle);

        if (result != GameResult.Continuing) {
            GUIStyle buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 30;
            buttonStyle.fontStyle = FontStyle.Bold;

            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 100, 20), "Fail", messageStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle)) {
                action.Restart();
            }
        }
    }

    private void Update() {
        if (result != GameResult.Continuing) {
            return;
        }
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        PlayerEventManager.GetInstance().Move(verticalAxis, horizontalAxis);
    }

    public void SetState(Judger judger) {
        result = judger.result;
        score = judger.score;
    }

    public void Restart() {
        action = Director.GetInstance().currentSceneController as IUserAction;
        result = GameResult.Continuing;
        score = 0;
    }
}