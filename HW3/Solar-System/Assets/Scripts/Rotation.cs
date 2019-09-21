using UnityEngine;

public class Rotation : MonoBehaviour {
    void Update() {
        transform.RotateAround(transform.position, Vector3.up, Random.Range(1f, 5f));
    }
}