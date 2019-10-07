using UnityEngine;

public class GameGUI : MonoBehaviour {
    private IUserAction action;
    public GameResult result { get; set; }

    private void Start() {
        Reset();
    }

    private void OnGUI() {
        if (result != GameResult.Continuing) {
            string message;
            if (result == GameResult.Win) {
                message = "Win";
            }
            else {
                message = "Fail";
            }
            GUIStyle messageStyle = new GUIStyle();
            messageStyle.fontSize = 50;
            messageStyle.fontStyle = FontStyle.Bold;
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
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f)) {
                Debug.Log(hit.collider.name);
                RoleController roleController = hit.collider.GetComponent<RoleController>();
                if (roleController != null) {
                    // if a role is clicked, then move it
                    action.MoveRole(roleController);
                }
                else if (hit.transform.name == "Boat") {
                    // if a boat is clicked, then move it
                    action.MoveBoat();
                }
            }
        }
    }

    public void Reset() {
        action = Director.GetInstance().currentSceneController as IUserAction;
        result = GameResult.Continuing;
    }
}
