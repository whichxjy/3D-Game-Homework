using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {
    public MarioController marioController;

    private void Start() {
        VirtualButtonBehaviour[] vbbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        foreach (VirtualButtonBehaviour vbb in vbbs) {
            vbb.RegisterEventHandler(this);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb) {
        switch (vb.VirtualButtonName) {
            case "ForwardButton":
                marioController.MoveForward();
                break;
            case "BackwardButton":
                marioController.MoveBackward();
                break;
            case "LeftButton":
                marioController.RotateLeft();
                break;
            case "RightButton":
                marioController.RotateRight();
                break;
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb) {
        marioController.Stop();
    }
}

