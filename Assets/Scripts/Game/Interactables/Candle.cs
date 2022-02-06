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
        if (!HeaterControls.Instance.interacted) {
            BigInfo.Instance.Print("HEATER MUST BE ON FIRST", Color.red, .5f);
            PlayerController.Instance.Reset();
        } else if (OilPipe.Instance.interacted) {
            BigInfo.Instance.Print("BOILER PIPE MUST BE DESTROYED", Color.red, .5f);
            PlayerController.Instance.Reset();
        } else if (LampCable.Instance.interacted) {
            BigInfo.Instance.Print("CANDLE MUST BE ON", Color.red, .5f);
            PlayerController.Instance.Reset();
        } else {
            BigInfo.Instance.Print("YOU WON", Color.green, 0);
        }
	}

    public void Light( ) {
        goLight.SetActive(true);
    }
}
