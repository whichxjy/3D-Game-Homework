using UnityEngine;

public class Revolution : MonoBehaviour {
    public Transform sun;
    private float speed;
    private Vector3 axis;

    void Start() {
        speed = Random.Range(10f, 30f);
        axis = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
    }

    void Update() {
        // revolution around the Sun
        transform.RotateAround(sun.position, axis, speed * Time.deltaTime);
    }
}
