using UnityEngine;

public class PatrolTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PatrolEventManager.GetInstance().SeePlayer(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            PatrolEventManager.GetInstance().LosePlayer(gameObject);
        }
    }
}
