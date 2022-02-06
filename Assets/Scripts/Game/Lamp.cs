using UnityEngine;

public class Lamp : MonoBehaviour {

    public static Lamp Instance;

    void Awake() {
        Instance = this;
    }

    public void TurnOff() {
        GetComponent<Light>().enabled = false;
        transform.parent.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

}
