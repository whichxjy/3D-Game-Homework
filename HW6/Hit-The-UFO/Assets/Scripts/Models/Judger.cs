using UnityEngine;

public enum GameResult {
    Continuing,
    Win,
    Lose
}

public class Judger {
    public GameResult result = GameResult.Continuing;
    public int round = 1;
    public int trial = 1;
    public int score = 0;
    private readonly int maxRound = 10;
    private readonly int maxTrial = 10;

    public void AddScore(int score) {
        this.score += score;
        FirstController sceneController = Director.GetInstance().currentSceneController as FirstController;
        sceneController.SetGUI(this.result, this.round, this.trial, this.score);
    }

    public void SubScore(int score) {
        this.score -= score;
        if (this.score < 0) {
            result = GameResult.Lose;
        }
        FirstController sceneController = Director.GetInstance().currentSceneController as FirstController;
        sceneController.SetGUI(result, this.round, this.trial, this.score);
    }

    public void NextRound() {
        round += 1;
        if (round > maxRound) {
            result = GameResult.Win;
        }
        FirstController sceneController = Director.GetInstance().currentSceneController as FirstController;
        sceneController.SetGUI(result, this.round, this.trial, this.score);
    }

    public void NextTrial() {
        trial += 1;
        if (trial > maxTrial) {
            trial = 1;
            NextRound();
        }
    }
}
