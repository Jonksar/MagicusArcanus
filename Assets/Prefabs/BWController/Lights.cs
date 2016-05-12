using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lights : MonoBehaviour {
	public List<GameObject> torchesInRange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		Debug.Log (c.gameObject.tag);
		if (c.gameObject.tag == "Torch") {
			torchesInRange.Add(c.gameObject);
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.gameObject.tag == "Torch") {
			torchesInRange.Remove(c.gameObject);
		}
	}
}
