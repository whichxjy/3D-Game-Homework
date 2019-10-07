using UnityEngine;

public class RoleController : MonoBehaviour {
    public Role model = new Role();

    public void ArriveShore(ShoreController shoreController) {
        model.location = shoreController.model.location;
        model.isOnBoat = false;
        transform.parent = null;
    }

    public void ArriveBoat(BoatController boatController) {
        model.location = boatController.model.location;
        model.isOnBoat = true;
        transform.parent = boatController.transform;
    }

    public void Reset() {
        ShoreController shoreController = (Director.GetInstance().currentSceneController as FirstController).leftShoreController;
        ArriveShore(shoreController);
        //transform.position = shoreController.model.GetEmptyPosition();
    }
}
