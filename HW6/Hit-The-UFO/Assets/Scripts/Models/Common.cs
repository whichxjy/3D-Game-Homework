using UnityEngine;

// Location of UFO
public enum Location {
    Left,
    Right
}

// GameObject Loader
public static class Loader {
    // instantiate game object from the given path and set its position
    public static GameObject LoadObj(string path, Vector3 position) {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, position, Quaternion.identity, null) as GameObject;
    }
    
    // instantiate material from the given path
    public static Material LoadMat(string path) {
        var mat = Resources.Load<Material>(path);
        return Object.Instantiate(mat) as Material;
    }
}