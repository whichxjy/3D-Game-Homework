using UnityEngine;

public class PhysicActionManager : IActionManager {
    public void SetAction(UFOController ufoController) {
        // current speed
        UFOInfo info = ufoController.info;
        Vector3 speed = new Vector3(info.speed, info.speed, info.speed);

        // set rigidbody
        Rigidbody currentRigidbody = ufoController.obj.GetComponent<Rigidbody>();
        if (currentRigidbody == null) {
            Rigidbody rigidbody = ufoController.obj.AddComponent<Rigidbody>();
            rigidbody.velocity = speed;
            rigidbody.AddForce(ufoController.info.speed * Vector3.one, ForceMode.Impulse);
            rigidbody.useGravity = true;
        }
        else {
            currentRigidbody.velocity = speed;
        }
    }
}
