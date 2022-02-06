using UnityEngine;

public class Candle : MonoBehaviour, IInteractable {
    public static Candle Instance;
    [ReadOnly]
    public bool interacted = false;

    public GameObject goLight;

    void Awake() {
        Instance = this;
    }

	public void Interact() {
		interacted = true;
        if (HeaterControls.Instance.interacted && LampCable.Instance.interacted && OilPipe.Instance.interacted) {
            BigInfo.Instance.Print("YOU WON", Color.green, 0);
        } else {
            PlayerController.Instance.Reset();
        }
	}

    public void Light( ) {
        goLight.SetActive(true);
    }
}
