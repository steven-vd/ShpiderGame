using UnityEngine;

public class HeaterControls : MonoBehaviour, IInteractable {
    public static HeaterControls Instance;
    [ReadOnly]
    public bool interacted = false;

    void Awake() {
        Instance = this;
    }

	public void Interact() {
		interacted = true;
		GetComponent<Renderer>().material.color = Color.black;
	}

}
