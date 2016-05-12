using UnityEngine;
using System.Collections;

public class Relic : MonoBehaviour {

	public float speedOfRotation = 5f;
	public GameObject currentlyCapturing;
	public float captureTime;
	public bool beingCaptured = false;

	private float startedCapturingTime;

	void Update () {

		transform.Rotate (Vector3.up + Vector3.right, Time.deltaTime * speedOfRotation);

		if (Time.time - startedCapturingTime > captureTime & beingCaptured) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player") {
			currentlyCapturing = c.gameObject;
			startedCapturingTime = Time.time;
			beingCaptured = true;
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.gameObject.tag == "Player") {
			currentlyCapturing = c.gameObject;
			startedCapturingTime = Time.time;
			beingCaptured = false;
		}
	}
}
