using UnityEngine;

public class PatrolCollide : MonoBehaviour {
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            PatrolEventManager.GetInstance().HitPlayer(gameObject);
        }
        else {
            PatrolEventManager.GetInstance().HitObstacle(gameObject);
        }
    }
}
