using UnityEngine;

public class CCActionManager : IActionManager {
    private class CCAction : MonoBehaviour {
        private Vector3 speed { get; set; }
        private static Vector3 gravity = new Vector3(0f, -9.80665f, 0f);

        private void Start () {
            ResetSpeed();
        }

        private void Update() {
            if (gameObject.activeSelf == false) {
                Destroy(this);
            }
            else {
                speed += gravity * Time.deltaTime;
                Vector3 translation = speed * Time.deltaTime;
                transform.Translate(translation);
            }
        }

        public void ResetSpeed() {
            UFOInfo info = gameObject.GetComponent<UFOController>().info;
            speed = new Vector3(info.speed, info.speed, info.speed);
        }
    }

    public void SetAction(UFOController ufoController) {
        CCAction action = ufoController.obj.GetComponent<CCAction>();
        if (action == null) {
            ufoController.obj.AddComponent<CCAction>();
        }
        else {
            action.ResetSpeed();
        }
    }
}
