using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BWController : MonoBehaviour {

	// Generic properties of class
	[Header("Generic Class Settings")]
	public bool isDark = false;
	[Range(0.5f, 4f)][SerializeField] public float changingTime;
	[Range(0f, 1.1f)][SerializeField] public float rageBar = 0f; // percentage of rage bar filled
	public float rageTimeSpeed = 1f;

	[Header("Particle Effect References")]
	public ParticleSystem blackSmoke;
	public ParticleSystem whiteSmoke;

	// Lights on
	[Header("Lights On/Off! Settings")]
	public List<GameObject> lightsTorchesInRange;
	public float torchSetRate = 5f;
	public bool lightsOn;
	public bool lightsOff;

	private GameObject lightsOnOff;

	// Wolf and the Lamb
	[Header("Wolf and the Lamb Settings")]
	public float wolfAndTheLambAngle = 60f;
	public bool getMS;
	public bool wolf = false;
	public bool lamb = false;
	
	private GameObject wolfAndTheLamb;



	private bool isSwitching;
	private float startedSwitching;

	// Use this for initialization
	void Start () {
		// Generic 
		isSwitching = false;
		startedSwitching = -1000f;
		blackSmoke.enableEmission = false;
		whiteSmoke.enableEmission = false;

		// Lights on /off! 
		lightsOnOff = getChildGameObject (gameObject, "Lights");
		lightsTorchesInRange = lightsOnOff.GetComponent<Lights> ().torchesInRange;

		// Wolf and the Lamb
		wolfAndTheLamb = getChildGameObject (gameObject, "Wolf and the lamb");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rageBar > 1f) {
			rageBar = 0f;
			isSwitching = true;
			startedSwitching = Time.time;

			if (!isDark) {
				blackSmoke.enableEmission = true;
			} else {
				whiteSmoke.enableEmission = true;
			}

			isDark = !isDark;
		}

		if (isSwitching & Time.time - startedSwitching > changingTime) {
			isSwitching = false;
			blackSmoke.enableEmission = false;
			whiteSmoke.enableEmission = false;
		}

		// Add some rage to rageBar
		rageBar += Time.deltaTime * rageTimeSpeed * 0.01f;

		// Handle key presses:
		// When player is white

		if (!isDark) {
			// Toggle ability Lights On!
			if (Input.GetKey (KeyCode.Q)) {
				lightsOn = !lightsOn;
			}
		// When player is dark
		} else {

			// Toggle ability Lights Off!
			if (Input.GetKey (KeyCode.Q)) {
				lightsOff = !lightsOff;
			}
		}

	// Do magic
		LightsOnOffAbility ();
		WolfAndTheLambAbility ();
	}

	static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts)
			if (t.gameObject.name == withName)
				return t.gameObject;
		return null;
	}

	public void LightsOnOffAbility() {
		// when player is white
		if (!isDark & lightsOn) {
			foreach (GameObject torch in lightsTorchesInRange) {
				// if torch can be set now (has some time between sets)
				if (Time.time - torch.GetComponent<Torch> ().timeSet > torchSetRate) {
					torch.GetComponent<Torch> ().hasFire = true; // add fire
					torch.GetComponent<Torch> ().timeSet = Time.time;
				}
			}
			// player is dark
		} else if (isDark & lightsOff){
			foreach (GameObject torch in lightsTorchesInRange) {
				// if torch can be set now (has some time between sets)
				if (Time.time - torch.GetComponent<Torch> ().timeSet > torchSetRate) {
					torch.GetComponent<Torch> ().hasFire = false; // remove fire
					torch.GetComponent<Torch> ().timeSet = Time.time;
				}
			}
		}
	}

	public void WolfAndTheLambAbility() {
		getMS = wolfAndTheLamb.GetComponent<WolfAndTheLamb>().GetMovementSpeed (wolfAndTheLambAngle, isDark);
		if (!isDark) {
			if (getMS & lamb) {
			}
		} else {
			if(getMS & wolf) {

			}
		}

	}
}