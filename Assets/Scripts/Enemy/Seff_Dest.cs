using UnityEngine;

public class Seff_Dest : MonoBehaviour
{
	private	AudioSource	sound;
	void Awake()
	{
		sound = GetComponent<AudioSource>();
		sound.Play();
	}
	void Update()
	{
		if (!sound.isPlaying)
			Destroy(gameObject);
	}
}
