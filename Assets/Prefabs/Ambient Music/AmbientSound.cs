using UnityEngine;
using System.Collections;

public class AmbientSound : MonoBehaviour {

	public AudioSource audio;
	[Range(0f, 1f)][SerializeField]public float dt = 0.1f;

	void Start(){
		InvokeRepeating("PlaySound",0.001f, audio.clip.length + Random.value * dt * audio.clip.length);
	}

	void PlaySound(){
		audio.pitch = 1f + 0.3f * Random.value;
		audio.Play();
	}
}
