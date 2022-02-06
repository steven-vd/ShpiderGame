using UnityEngine;

public class LampCable : MonoBehaviour, IInteractable {
    public static LampCable Instance;
    [ReadOnly]
    public bool interacted = false;

    void Awake() {
        Instance = this;
    }

	public void Interact() {
		interacted = true;
		GetComponent<Renderer>().material.color = Color.black;

        Candle.Instance.Light();
        Lamp.Instance.TurnOff();
	}
}
