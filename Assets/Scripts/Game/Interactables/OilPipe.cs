using UnityEngine;

public class OilPipe : MonoBehaviour, IInteractable {

	public static OilPipe Instance;
	public bool interacted = false;

	void Awake() {
		Instance = this;
	}

	void Update() {
		if (interacted && HeaterControls.Instance.interacted) {
			transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	public void Interact() {
		interacted = true;
		GetComponent<Renderer>().material.color = Color.black;
	}
}
