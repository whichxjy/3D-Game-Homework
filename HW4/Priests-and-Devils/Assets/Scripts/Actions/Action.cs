using UnityEngine;

public class Action : ScriptableObject {
    public bool enable = true;
    public bool destroy = false;
    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public IActionCallback callback { get; set; }

    protected Action() {}

    public virtual void Start() {
        // it's not implemented!
        throw new System.NotImplementedException();
    }

    public virtual void Update() {
        // it's not implemented!
        throw new System.NotImplementedException();
    }
}
