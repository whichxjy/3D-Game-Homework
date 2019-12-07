using UnityEngine;

public class CoinBlock : MonoBehaviour {
	public GameObject coinModel;
	bool boxHit = false;

	void Start () {
		coinModel.SetActive (false);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "Mario" && boxHit == false) {
			coinModel.SetActive(true);
			Destroy(coinModel, 1.5f);
			boxHit = true;
		}
	}
}
