using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasementDweller : MonoBehaviour {

    [ReadOnly]
    public NavMeshAgent ai;
    [ReadOnly]
    public float timeSinceLastSeenPlayer = 0;
    public Transform restingPosition;
    public Transform eyes;

    void Awake() {
        ai = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (CanSeePlayer()) {
            timeSinceLastSeenPlayer = 0;
            ai.destination = PlayerController.Instance.transform.position;
        } else {
            timeSinceLastSeenPlayer += Time.deltaTime;
            if (timeSinceLastSeenPlayer > 5.0f) {
                ai.destination = restingPosition.position;
                if (Vector3.Distance(restingPosition.position, transform.position) < 1.0f) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, restingPosition.rotation, 0.1f);
                }
            }
        }
    }

    public bool CanSeePlayer() {
		RaycastHit hit;
		Vector3 playerDirection = (PlayerController.Instance.transform.position - eyes.position);
        Debug.DrawLine(eyes.transform.position, eyes.transform.position + playerDirection, Color.red, 1);
		if (Physics.Raycast(eyes.position, playerDirection, out hit)) {
			if (hit.transform == PlayerController.Instance.transform) {
				return true;
			}
		}
		return false;
	}

}
