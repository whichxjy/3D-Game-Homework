using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using Vuforia;

public class MarioController : MonoBehaviour, ITrackableEventHandler {

    private Rigidbody marioRigidbody;

    private Animation idleAnimation;
    private Animation runAnimation;
    private Animation jumpAnimation;

    public float turnSmoothing = 15f;
    public float speed = 0.3f;
    public float jumpSpeed = 20f;

    private float distToGround;
    public Collider ground;
    public GameObject imageTarget;
	private TrackableBehaviour marioBehaviour;
    private bool marioFound = false;

    void Start() {
        marioRigidbody = GetComponent<Rigidbody>();  

        idleAnimation = GetComponent<Animation>();
        runAnimation = GetComponent<Animation>();
        jumpAnimation = GetComponent<Animation>();
    }

    void FixedUpdate() {
        // check if mario is tracked
        if (marioFound) {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0) {
                Rotating (horizontal, vertical);
                Moving (horizontal, vertical);
                runAnimation.Play("run");
            }
            else {
                idleAnimation.Play("idle");
            }

            if (CrossPlatformInputManager.GetButtonDown("Jump") && IsGrounded () == true) {
                marioRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
        else {
            // reset position
			gameObject.transform.localPosition = new Vector3 (0, 0, 0);
        }
    }

	private void Rotating (float horizontal, float vertical) {
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(marioRigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		marioRigidbody.MoveRotation(newRotation);
	}

	private void Moving(float horizontal, float vertical) {
        if (horizontal != 0) {
            transform.Translate (0, 0, horizontal * Time.fixedDeltaTime * speed);
        }
        if (vertical != 0) {
            transform.Translate (0, 0, vertical * Time.fixedDeltaTime * speed);
        }
	}

    private bool IsGrounded() {
		return Physics.Raycast(transform.position, - Vector3.up, distToGround + 0.2f);
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			marioFound = true;
		}
		else {
			marioFound = false;
		}
	}
}
