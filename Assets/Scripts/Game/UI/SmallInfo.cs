using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SmallInfo : MonoBehaviour {

    public static SmallInfo Instance;
    [ReadOnly]
    public TMP_Text text;
    [ReadOnly]
    public float textLen;

    void Awake() {
		if (Instance != null) {
			Destroy(transform.parent.gameObject);
		} else {
            Instance = this;
            text = GetComponent<TMP_Text>();
			DontDestroyOnLoad(transform.parent.gameObject);
		}
    }

    public void Print(string msg, Color c, float len) {
        text.text = msg;
        text.color = c;
        textLen = len;
    }

    void Update() {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * textLen);
    }
}

