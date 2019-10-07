using UnityEngine;

public class ShoreController : MonoBehaviour {
    public Shore model = new Shore();

    public void RoleArrived(RoleController roleController, int targetPositionIndex) {
        model.roles[targetPositionIndex] = roleController.model;
    }

    public void RoleLeave(RoleController roleController) {
        // remove the target role from the shore
        Role targetRole = roleController.model;
        for (int i = 0; i < model.roles.Length; i++) {
            Role role = model.roles[i];
            if (role != null && role.type == targetRole.type && role.index == targetRole.index) {
                model.roles[i] = null;
            }
        }
    }

    public void Reset() {
        model.roles = new Role[6];
    }
}
