public enum GameResult {
    Continuing,
    Lose
}

public class Judger {
    public GameResult result;
    public int score;

    public Judger() {
        Restart();
    }

    public void AddScore() {
        score += 1;
    }

    public void GameOver() {
        result = GameResult.Lose;
    }

    public void Restart() {
        score = 0;
        result = GameResult.Continuing;
    }
}