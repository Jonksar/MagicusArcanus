using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float rotationSpeedH = 7.0f;
	public float rotationSpeedV = 7.0f;
	public float movementSpeed = 5.0f;
	
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private Vector3 initialAngle;

	void Start() {
		initialAngle = this.transform.eulerAngles;
	}
	
	void Update () {
		yaw += rotationSpeedH * Input.GetAxis("Mouse X");
		pitch -= rotationSpeedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = initialAngle + new Vector3(pitch, yaw, 0.0f);

		if(Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.S)) {
			transform.position -= transform.forward * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.A)) {
			transform.position -= transform.right * Time.deltaTime * movementSpeed;
		}
		else if(Input.GetKey(KeyCode.D)) {
			transform.position += transform.right * Time.deltaTime * movementSpeed;
		}

	}
}