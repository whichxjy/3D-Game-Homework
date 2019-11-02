using UnityEngine;

public class PatrolTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            PatrolEventManager.GetInstance().SeePlayer(gameObject);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            PatrolEventManager.GetInstance().LosePlayer(gameObject);
        }
    }
}
