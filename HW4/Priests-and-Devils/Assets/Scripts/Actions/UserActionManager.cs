using UnityEngine;
using System.Collections.Generic;

public class UserActionManager : ActionManager {

    // move boat to the other side
    public void MoveBoat(BoatController boatController) {
        MoveAction moveAction = MoveAction.GetAction(boatController.gameObject, this, boatController.model.GetPosition(), boatController.model.speed);
        AddAction(moveAction);
    }

    // move the role to the target
    public void MoveRole(RoleController roleController, Vector3 target) {
        Vector3 currentPosition = roleController.transform.position;
        Vector3 tempPosition = currentPosition;
        if (target.y > currentPosition.y) {
            tempPosition.y = target.y;
        }
        else {
            tempPosition.x = target.x;
        }
        // move to tempPosition first
        MoveAction moveAction1 = MoveAction.GetAction(roleController.gameObject, this, tempPosition, roleController.model.speed);
        MoveAction moveAction2 = MoveAction.GetAction(roleController.gameObject, this, target, roleController.model.speed);
        SequenceAction seqAction = SequenceAction.GetSeqAction(this, new List<Action>{ moveAction1, moveAction2 });
        AddAction(seqAction);
    }
}
