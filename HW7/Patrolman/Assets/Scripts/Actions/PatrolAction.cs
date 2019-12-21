using UnityEngine;
using System.Collections;

public class PatrolAction : MonoBehaviour {
    private readonly float smoothing = 2f;
    private Vector3 target;
    public Vector3 Target {
        get { return target; }
        set {
            target = value;
            StopCoroutine("MoveTo");
            StartCoroutine("MoveTo", target);
        }
    }

    IEnumerator MoveTo(Vector3 other) {
        while (Vector3.Distance(transform.position, other) > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, other, smoothing * Time.deltaTime);
            transform.LookAt(other);
            yield return null;
        }
        PatrolEventManager.GetInstance().Stop(gameObject);
    }
}
