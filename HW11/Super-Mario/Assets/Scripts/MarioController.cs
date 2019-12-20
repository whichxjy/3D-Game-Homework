using UnityEngine;

public class MarioController : MonoBehaviour {
    private enum MarioState {
        Stop,
        Forward,
        Backward,
        Left,
        Right
    };
    private MarioState currentState;

    private readonly float moveSpeed = 0.05f;
    private readonly float turnSpeed = 80f;

    private Animation idleAnimation;
    private Animation runAnimation;

    private void Start() {
        idleAnimation = GetComponent<Animation>();
        runAnimation = GetComponent<Animation>();
        Stop();
    }

    private void Update() {
        if (currentState == MarioState.Stop) {
            return;
        }
        runAnimation.Play("run");
        switch (currentState) {
            case MarioState.Forward:
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                break;
            case MarioState.Backward:
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                break;
            case MarioState.Left:
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                break;
            case MarioState.Right:
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                break;
        }
    }

    public void MoveForward() {
        currentState = MarioState.Forward;
    }

    public void MoveBackward() {
        currentState = MarioState.Backward;
    }

    public void RotateLeft() {
        currentState = MarioState.Left;
    }

    public void RotateRight() {
        currentState = MarioState.Right;
    }

    public void Stop() {
        idleAnimation.Play("idle");
        currentState = MarioState.Stop;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Meshroom")) {
            Destroy(collision.collider.gameObject);
        }
    }
}
