using UnityEngine;

public class BoatController : MonoBehaviour {
    public Boat model = new Boat();

    public void Move() {
        if (model.location == Location.Left) {
            model.location = Location.Right;
        }
        else {
            model.location = Location.Left;
        }
    }

    public void RoleLeave(RoleController roleController) {
        // remove the target role from the boat
        Role targetRole = roleController.model;
        for (int i = 0; i < model.roles.Length; i++) {
            Role role = model.roles[i];
            if (role != null && role.type == targetRole.type && role.index == targetRole.index) {
                model.roles[i] = null;
            }
        }
    }

    public void RoleArrived(RoleController roleController, int targetPositionIndex) {
        model.roles[targetPositionIndex] = roleController.model;
    }

    public void Reset() {
        if (model.location == Location.Right) {
            Move();
            transform.position = Boat.leftBoatPosition;
        }
        model.roles = new Role[2];
    }
}
