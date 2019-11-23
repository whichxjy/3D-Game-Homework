using UnityEngine;
using MyDOTween;

public class Test : MonoBehaviour {
    void Start() {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMove(new Vector3(0, 5, 0), 1));
        mySequence.Append(transform.DOScale(new Vector3(2, 2, 2), 1));
        mySequence.Append(GetComponent<Renderer>().material.DOColor(Color.blue, 1));
        mySequence.Append(
            DOTween.Sequence()
                .Append(transform.DOMove(new Vector3(0, 1, 0), 1))
                .Append(GetComponent<Renderer>().material.DOColor(Color.white, 1))
                .Append(transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1))
        );
    }
}