using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance;

	public float moveSpeed, rotateSpeed;
	public float surfaceStickDistance;
	public float gravity;

	[ReadOnly]
	public Vector3 velocity = Vector3.zero;
	[ReadOnly]
	public bool grounded = false;

	void Awake() {
		Instance = this;
	}

	void FixedUpdate() {
		// Movement
		if (Input.GetKey(KeyCode.W)) {
			transform.parent.Translate(transform.forward * moveSpeed, Space.World);
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.parent.Translate(-transform.forward * moveSpeed, Space.World);
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.up, -rotateSpeed, Space.Self);
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up, rotateSpeed, Space.Self);
		}

		RaycastHit hit;
		// Interact
		if (Input.GetKeyDown(KeyCode.E)) {
			if (Physics.Raycast(transform.position, transform.forward, out hit, 10f)) {
				hit.transform.GetComponent<IInteractable>().Interact();
			}
		}

		if (Physics.Raycast(transform.position, transform.forward, out hit, surfaceStickDistance)) {
			transform.parent.up = hit.normal;
			if (Physics.Raycast(transform.position, transform.forward, out hit, surfaceStickDistance)) {
				//Make sure spider doesn't get stuck
				transform.Rotate(0, 180, 0);
			}
		}

		velocity.y -= gravity;
		grounded = false;
		int layers = ~(1 << 8); // Ignore interactables
		if (velocity.y < 0 && Physics.Raycast(transform.position, -transform.up, out hit, surfaceStickDistance, layers)) {
			grounded = true;
			if (hit.distance < surfaceStickDistance * 0.99f) {
				//Adjust position if in floor
				transform.parent.Translate(0, surfaceStickDistance * 0.99f - hit.distance, 0, Space.Self);
			}
			velocity.y = 0;
		} else {
			transform.parent.up = Vector3.up;
		}
		//Fall
		transform.parent.Translate(velocity, Space.World);
	}
}
