using UnityEngine;

public class Main : MonoBehaviour {
    // player1 => "x", player2 => "o"
    private int[,] space = new int[3, 3]; // 0 => nothig, 1 => player1, 2 => player2
    private int playTurn; // 1 => player1, 2 => player2
    private int moveCount;
    private int winner; // 0 => undecided, 1 => player1, 2 => player2, 3 => winwin

    void Start() {
        reset();
    }

    void OnGUI() {
        // Show reset button
        if (GUI.Button(new Rect(650, 60, 90, 50), "Reset")) {
            reset();
        }
        // Show the grid
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (space[i, j] == 1) {
                    GUI.Button(new Rect(i * 100 + 555, j * 100 + 300, 100, 100), "x");
                }
                else if (space[i, j] == 2) {
                    GUI.Button(new Rect(i * 100 + 555, j * 100 + 300, 100, 100), "o");
                }
                else if (GUI.Button(new Rect(i * 100 + 555, j * 100 + 300, 100, 100), "") && winner == 0) {
                    // a empty space is clicked
                    space[i, j] = playTurn;
                    moveCount += 1;
                    winner = checkWinner();
                    // switch player
                    if (winner == 0) {
                        playTurn = (playTurn == 1) ? 2 : 1;
                    }
                }
            }
        }
        if (winner == 0) {
            GUI.Label(new Rect(670, 150, 100, 50), "In progress");
        }
        else if (winner == 1) {
            GUI.Label(new Rect(670, 150, 100, 50), "Player 1 (x) wins");
        }
        else if (winner == 2) {
            GUI.Label(new Rect(670, 150, 100, 50), "Player 2 (o) wins");
        }
        else {
            GUI.Label(new Rect(670, 150, 100, 50), "Win Win");
        }
    }

    void reset() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                space[i, j] = 0;
            }
        }
        playTurn = 1;
        moveCount = 0;
        winner = 0;
    }

    int checkWinner() {
        // check if any of the row is crossed with the same player's move
        for (int i = 0; i < 3; i++) {
            if (space[i, 0] == space[i, 1] && space[i, 1] == space[i, 2]) {
                return space[i, 0];
            }
        }

        // check if any of the column is crossed with the same player's move
        for (int i = 0; i < 3; i++) {
            if (space[0, i] == space[1, i] && space[1, i] == space[2, i]) {
                return space[0, i];
            }
        }

        // check if any of the diagonal is crossed with the same player's move
        if (space[0, 0] == space[1, 1] && space[1, 1] == space[2, 2]) {
            return space[0, 0];
        }
        if (space[0, 2] == space[1, 1] && space[1, 1] == space[2, 0]) {
            return space[0, 2];
        }

        if (moveCount == 9) {
            // winwin
            return 3;
        }
        else {
            // undecided
            return 0;
        }
    }
}