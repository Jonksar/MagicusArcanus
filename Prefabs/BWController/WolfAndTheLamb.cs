using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfAndTheLamb : MonoBehaviour {

	public List<GameObject> targets;
	public bool getMovementSpeed;
	public float angle = 60f;
	public bool isDark = false;

	public bool GetMovementSpeed(float angle, bool isDark) {
		foreach (GameObject target in targets) {
			if (Vector3.Angle(transform.forward, transform.position - target.transform.position) < angle){
				return true ^ isDark;
			}
		}
		return false ^ isDark;
	}
}
