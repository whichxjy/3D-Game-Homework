using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public float speed = 7.0f;
    public float rotationSpeed = 100.0f;

    public void Move(float verticalAxis, float horizontalAxis) {
        float translation = verticalAxis * speed;
        float rotation = horizontalAxis * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
