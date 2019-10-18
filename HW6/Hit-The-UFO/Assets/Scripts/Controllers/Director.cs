using UnityEngine;

public class Director : System.Object {
    // singleton instance
    private static Director instance;
    // controller of current scene
    public ISceneController currentSceneController { get; set; }
    // is it still running?
    public bool running { get; set; }
    // frame rate
    public int fps {
        get {
            return Application.targetFrameRate;
        }
        set {
            Application.targetFrameRate = value;
        }
    }

    public static Director GetInstance() {
        if (instance == null) {
            instance = new Director();
        }
        return instance;
    }
}