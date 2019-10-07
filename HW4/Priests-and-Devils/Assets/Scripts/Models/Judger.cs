using UnityEngine;

public enum GameResult {
    Continuing,
    Win,
    Lose
}

public class Judger {
    public GameResult result = GameResult.Continuing;
    private Boat boat;
    private Shore leftShore;
    private Shore rightShore;

    public Judger(Boat boat, Shore leftShore, Shore rightShore) {
        this.boat = boat;
        this.leftShore = leftShore;
        this.rightShore = rightShore;
    }

    public void CheckResult() {
        // the number of priests and devil in left shore
        int leftPriestNum = 0;
        int leftDevilNum = 0;
        foreach (Role role in leftShore.roles) {
            if (role == null) {
                continue;
            }
            else if (role.type == RoleType.Priest) {
                leftPriestNum += 1;
            }
            else {
                leftDevilNum += 1;
            }
        }
        // the number of priests and devil in right shore
        int rightPriestNum = 0;
        int rightDevilNum = 0;
        foreach (Role role in rightShore.roles) {
            if (role == null) {
                continue;
            }
            else if (role.type == RoleType.Priest) {
                rightPriestNum += 1;
            }
            else {
                rightDevilNum += 1;
            }
        }
        
        // check if the player wins
        if (rightPriestNum + rightDevilNum == 6) {
            result = GameResult.Win;
        }
        
        // count the roles on the boat
        int boatPriestNum = 0;
        int boatDevilNum = 0;
        foreach (Role role in boat.roles) {
            if (role == null) {
                continue;
            }
            else if (role.type == RoleType.Priest) {
                boatPriestNum += 1;
            }
            else {
                boatDevilNum += 1;
            }
        }

        // get the sum of the roles
        if (boat.location == Location.Left) {
            leftPriestNum += boatPriestNum;
            leftDevilNum += boatDevilNum;
        }
        else {
            rightPriestNum += boatPriestNum;
            rightDevilNum += boatDevilNum;
        }

        // check if the player loses
        if ((leftPriestNum > 0 && leftDevilNum > leftPriestNum)
            || (rightPriestNum > 0 && rightDevilNum > rightPriestNum)) {
            result = GameResult.Lose;
        }

        (Director.GetInstance().currentSceneController as FirstController).SetGameResult(result);
    }
}
