using UnityEngine;

public class UFOController : MonoBehaviour {
    public UFOInfo info { get; set; }
    public GameObject obj { get; set; }

    public void Appear() {
        obj.SetActive(true);
    }

    public void Disappear() {
        obj.SetActive(false);
    }

    public void SetInfo(UFOInfo info) {
        this.info = info;
        obj.transform.localScale.Set(info.size, info.size, info.size);
    }
}
