using UnityEngine;

// GameObject Loader
public static class Loader {
    // instantiate game object from the given path and set its position
    public static GameObject LoadObj(string path, Vector3 position) {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, position, Quaternion.identity, null) as GameObject;
    }
}