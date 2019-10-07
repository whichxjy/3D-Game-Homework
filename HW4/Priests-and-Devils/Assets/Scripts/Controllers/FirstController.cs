using UnityEngine;
using System.Collections.Generic;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
    private GameGUI gui;
    private Judger judger;
    private UserActionManager userActionManager;
    public ShoreController leftShoreController;
    public ShoreController rightShoreController;
    public BoatController boatController;
    public RoleController[] roleControllers = new RoleController[6];

    public void Awake() {
        Director director = Director.GetInstance();
        director.fps = 60;
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        gui = gameObject.AddComponent<GameGUI>() as GameGUI;
        judger = new Judger(boatController.model, leftShoreController.model, rightShoreController.model);
        userActionManager = gameObject.AddComponent<UserActionManager>() as UserActionManager;
    }

    public void LoadResources() {
        // load left shore
        GameObject leftShoreObj = Loader.Instantiate("Prefabs/Land", Shore.leftShorePosition);
        leftShoreObj.name = "LeftShore";
        leftShoreController = leftShoreObj.AddComponent<ShoreController>() as ShoreController;
        leftShoreController.model.location = Location.Left;
        // load right shore
        GameObject rightShoreObj = Loader.Instantiate("Prefabs/Land", Shore.rightShorePosition);
        rightShoreObj.name = "RightShore";
        rightShoreController = rightShoreObj.AddComponent<ShoreController>() as ShoreController;
        rightShoreController.model.location = Location.Right;
        // load the boat
        GameObject boatObj = Loader.Instantiate("Prefabs/Boat", Boat.leftBoatPosition);
        boatObj.name = "Boat";
        boatController = boatObj.AddComponent<BoatController>() as BoatController;
        boatController.model.location = Location.Left;
        // load priests
        for (int i = 0; i < 3; i++) {
            GameObject priestObj = Loader.Instantiate("Prefabs/Priest", leftShoreController.model.GetEmptyPosition());
            priestObj.name = "Priest" + i;
            roleControllers[i] = priestObj.AddComponent<RoleController>() as RoleController;
            roleControllers[i].model.type = RoleType.Priest;
            roleControllers[i].model.index = i;
            roleControllers[i].ArriveShore(leftShoreController);
            leftShoreController.RoleArrived(roleControllers[i], leftShoreController.model.GetEmptyPositionIndex());
        }
        // load devils
        for (int i = 0; i < 3; i++) {
            GameObject devilObj = Loader.Instantiate("Prefabs/Devil", leftShoreController.model.GetEmptyPosition());
            devilObj.name = "Devil" + i;
            roleControllers[i + 3] = devilObj.AddComponent<RoleController>() as RoleController;
            roleControllers[i + 3].model.type = RoleType.Devil;
            roleControllers[i + 3].model.index = i;
            roleControllers[i + 3].ArriveShore(leftShoreController);
            leftShoreController.RoleArrived(roleControllers[i + 3], leftShoreController.model.GetEmptyPositionIndex());
        }
    }

    public void MoveBoat() {
        if (boatController.model.IsEmpty()) {
            return;
        }
        boatController.Move();
        userActionManager.MoveBoat(boatController);
        judger.CheckResult();
    }

    public void MoveRole(RoleController roleController) {
        if (roleController.model.isOnBoat) {
            // if the role is on the boat, then jump to the shore
            ShoreController targetShoreController;
            if (boatController.model.location == Location.Left) {
                targetShoreController = leftShoreController;
            }
            else {
                targetShoreController = rightShoreController;
            }
            boatController.RoleLeave(roleController);
            roleController.ArriveShore(targetShoreController);
            int emptyPositionIndex = targetShoreController.model.GetEmptyPositionIndex();
            targetShoreController.RoleArrived(roleController, emptyPositionIndex);
            userActionManager.MoveRole(roleController, targetShoreController.model.GetPosition(emptyPositionIndex));
        }
        else {
            // if the role is in the shore, then jump to the boat
            if ((roleController.model.location != boatController.model.location)
                || (boatController.model.IsFull())) {
                return;
            }
            ShoreController currentShoreController;
            if (roleController.model.location == Location.Left) {
                currentShoreController = leftShoreController;
            }
            else {
                currentShoreController = rightShoreController;
            }
            currentShoreController.RoleLeave(roleController);
            roleController.ArriveBoat(boatController);
            int emptyPositionIndex = boatController.model.GetEmptyPositionIndex();
            boatController.RoleArrived(roleController, emptyPositionIndex);
            userActionManager.MoveRole(roleController, boatController.model.GetPosition(emptyPositionIndex));
        }
        judger.CheckResult();
    }

    public void Restart() {
        gui.Reset();
        leftShoreController.Reset();
        rightShoreController.Reset();
        boatController.Reset();
        foreach (RoleController roleController in roleControllers) {
            roleController.Reset();
            roleController.ArriveShore(leftShoreController);
            int emptyPositionIndex = leftShoreController.model.GetEmptyPositionIndex();
            leftShoreController.RoleArrived(roleController, emptyPositionIndex);
            userActionManager.MoveRole(roleController, leftShoreController.model.GetPosition(emptyPositionIndex));
        }
        judger = new Judger(boatController.model, leftShoreController.model, rightShoreController.model);
    }

    public void SetGameResult(GameResult result) {
        gui.result = result;
    }
}
