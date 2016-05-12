using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {
	public GameObject fire;
	public float lerpSpeed = 10f;
	[Range(0f, 1f)][SerializeField]public float minIntensity = 0f;
	[Range(1f, 8f)][SerializeField] public float maxIntensity = 4f;
	public float flickerSpeed = 0.5f;
	public Light selfLight;
	public Color[] colors;
	public bool hasFire = true;
	public float timeSet = Time.time;

	private float targetIntensity;
	private float nextActionTime;

	// Use this for initialization
	void Start () {
		nextActionTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		// when we should have fire and dont have one;
		GameObject fireobj = getChildGameObject (gameObject, "Fire");
		if (hasFire & fireobj == null) {
			// create the fire
			GameObject childFire = (GameObject)Instantiate (fire, transform.position, transform.rotation);
			childFire.name = "Fire";
			childFire.transform.parent = transform;
			childFire.transform.localPosition +=  new Vector3(0f, 2f, 0.95f);
			selfLight = getChildGameObject(childFire, "Point light").GetComponent<Light>();
			childFire.transform.localScale = Vector3.one;
			// when we should not have fire and we have one;
		} else if (!hasFire & fireobj != null) {
			if (getChildGameObject(fireobj, "Point light").GetComponent<Light>().intensity < (maxIntensity - minIntensity) / 5) {
				// If has faded out enough
				Destroy (fireobj);
				gameObject.GetComponent<AudioSource>().volume = 0f;
			} else {
				// Fade out
				getChildGameObject(fireobj, "Point light").GetComponent<Light>().intensity = Mathf.Lerp(getChildGameObject(fireobj, "Point light").GetComponent<Light>().intensity,
				                                                                                        0.0f,
				                                                                                        Time.deltaTime * lerpSpeed);

				gameObject.GetComponent<AudioSource>().volume = Mathf.Lerp(gameObject.GetComponent<AudioSource>().volume,
				                                                           0f,
				                                                           lerpSpeed * Time.deltaTime);
			}
		} else if (hasFire & fireobj != null) {
			Flicker();
		}
		// If it is time to flicker again
	}

	static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts)
			if (t.gameObject.name == withName)
				return t.gameObject;
		return null;
	}

	private void Flicker() {
		if (Time.time > nextActionTime) {
			nextActionTime += flickerSpeed * Random.value;
			targetIntensity = minIntensity + Random.value * (maxIntensity - minIntensity);
			// change light intensity smoothly
			selfLight.intensity = Mathf.Lerp (selfLight.intensity, targetIntensity, Time.deltaTime * lerpSpeed);
			if (colors.Length != 0) 
				selfLight.color = Color.Lerp(selfLight.color, colors[Random.Range(0, colors.Length)], Time.deltaTime * lerpSpeed);
		}
	}
	
}