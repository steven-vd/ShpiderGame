using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance;

	public float moveSpeed, rotateSpeed;
	public float surfaceStickDistance;
	public float gravity;
	public Transform map;

	[ReadOnly]
	public IInteractable interactableInRange;
	[ReadOnly]
	public Vector3 velocity = Vector3.zero;
	[ReadOnly]
	public bool grounded = false;

	void Awake() {
		Instance = this;
	}

	void Update() {
		// Interact
		if (Input.GetKeyDown(KeyCode.E)) {
			if (interactableInRange != null) {
				interactableInRange.Interact();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		const int interactable = 8;
		const int killOnTouch = 9;
		switch (other.gameObject.layer) {
			case interactable: {
				SmallInfo.Instance.Print("Press 'E' to interact", Color.green, 0f);
				interactableInRange = other.GetComponent<IInteractable>();
				break;
			} case killOnTouch: {
				if (other.name == "oilSpill") {
					BigInfo.Instance.Print("YOU DROWNED IN OIL", Color.red, .5f);
				} else {
					BigInfo.Instance.Print("YOU DIED", Color.red, .5f);
				}
				Reset();
				break;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (interactableInRange == null) {
			return;
		}
		if (other.gameObject == (interactableInRange as MonoBehaviour).gameObject) {
			SmallInfo.Instance.Print("Press 'E' to interact", Color.green, 1f);
			interactableInRange = null;
		}
	}

	void FixedUpdate() {
		// Movement
		if (Input.GetKey(KeyCode.W)) {
			transform.parent.Translate(transform.forward * moveSpeed, Space.World);
		}
		if (Input.GetKey(KeyCode.S)) {
			//transform.parent.Translate(-transform.forward * moveSpeed, Space.World);
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.up, -rotateSpeed, Space.Self);
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up, rotateSpeed, Space.Self);
		}

		RaycastHit hit;
		const int interactable = 1 << 8;
		const int killOnTouch = 1 << 9;
		const int layer = ~(interactable | killOnTouch);
		if (Physics.Raycast(transform.position, transform.forward, out hit, surfaceStickDistance, layer)) {
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

	public void Reset() {
		SceneManager.LoadScene(0);
	}

}
