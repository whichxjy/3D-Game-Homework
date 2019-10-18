using UnityEngine;

public class GameGUI : MonoBehaviour {
    private IUserAction action;
    public GameResult result { get; set; }
    public int round = 1;
    public int trial = 1;
    public int score = 0;

    private void Start() {
        Reset();
    }

    private void OnGUI() {
        GUIStyle messageStyle = new GUIStyle();
        messageStyle.fontSize = 40;
        messageStyle.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(10, Screen.height / 2 - 160, 200, 100), "Round: " + round, messageStyle);
        GUI.Label(new Rect(10, Screen.height / 2 - 100, 200, 100), "Trial: " + trial, messageStyle);
        GUI.Label(new Rect(10, Screen.height / 2 - 40, 200, 100), "Score: " + score, messageStyle);

        if (result != GameResult.Continuing) {
            string message;
            if (result == GameResult.Win) {
                message = "Win";
            }
            else {
                message = "Fail";
            }
            GUIStyle buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 30;
            buttonStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 100, 20), message, messageStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle)) {
                action.Restart();
            }
        }
    }

    private void Update() {
        if (result != GameResult.Continuing) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f)) {
                UFOController ufoController = hit.collider.GetComponent<UFOController>();
                if (ufoController != null) {
                    action.HitUFO(ufoController);
                }
            }
        }

        if (Input.GetKeyDown("space")) {
            (Director.GetInstance().currentSceneController as FirstController).GenerateUFOs();
        }
    }

    public void Reset() {
        action = Director.GetInstance().currentSceneController as IUserAction;
        result = GameResult.Continuing;
        round = 1;
        trial = 1;
        score = 0;
    }
}
