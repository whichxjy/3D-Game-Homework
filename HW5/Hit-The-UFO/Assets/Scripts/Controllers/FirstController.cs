using UnityEngine;
using System.Collections.Generic;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
    private GameGUI gui;
    private Judger judger;
    private List<UFOController> ufoControllers = new List<UFOController>();

    public void Awake() {
        Director director = Director.GetInstance();
        director.fps = 60;
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        gui = gameObject.AddComponent<GameGUI>() as GameGUI;
        judger = new Judger();
    }

    public void LoadResources() {
        // empty
    }

    private void Update() {
        List<UFOController> missUFOControllers = new List<UFOController>();
        foreach (UFOController ufoController in ufoControllers) {
            if (ufoController.transform.position.y < -20) {
                missUFOControllers.Add(ufoController);
            }
        }
        foreach (UFOController ufoController in missUFOControllers) {
            ufoControllers.Remove(ufoController);
            judger.SubScore(1);
            UFOFactory.GetInstance().RecycleUFO(ufoController);
        }
    }

    public void GenerateUFOs() {
        judger.NextTrial();
        ufoControllers.AddRange(Ruler.GenerateUFOs(judger.round));
    }

    public void HitUFO(UFOController ufoController) {
        judger.AddScore(ufoController.info.score);
        ufoControllers.Remove(ufoController);
        UFOFactory.GetInstance().RecycleUFO(ufoController);
    }

    public void SetGUI(GameResult result, int round, int trial, int score) {
        gui.result = result;
        gui.round = round;
        gui.trial = trial;
        gui.score = score;
    }

    public void Restart() {
        gui.Reset();
        judger = new Judger();
        ufoControllers = new List<UFOController>();
    }
}