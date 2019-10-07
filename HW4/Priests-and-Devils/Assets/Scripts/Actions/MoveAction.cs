using UnityEngine;

public class MoveAction : Action {
    // target position
    public Vector3 target;
    // the speed of object
    public float speed;

    public static MoveAction GetAction(GameObject gameObject, IActionCallback callback, Vector3 target, float speed) {
        MoveAction action = ScriptableObject.CreateInstance<MoveAction>();
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = callback;
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Start() {
        // empty
    }

    public override void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target) {
            destroy = true;
            callback.ActionDone(this);
        }
    }
}
