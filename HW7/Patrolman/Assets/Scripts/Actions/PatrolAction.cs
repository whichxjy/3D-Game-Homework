using UnityEngine;
using System.Collections;

public class PatrolAction : MonoBehaviour {
    private float smoothing = 2f;
    private Vector3 target;
    public Vector3 Target {
        get { return target; }
        set {
            target = value;
            StopCoroutine("MoveTo");
            StartCoroutine("MoveTo", target);
        }
    }

    IEnumerator MoveTo(Vector3 target) {
        while (Vector3.Distance(transform.position, target) > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
            yield return null;
        }
        PatrolEventManager.GetInstance().Stop(gameObject);
    }
}
