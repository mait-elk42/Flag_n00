using System.Collections;
using UnityEngine;

public class Freeze_Manager : MonoBehaviour
{
	private bool running = false;
	private SpriteRenderer sprite;

	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.enabled = false;
	}
	void Start()
	{
		
	}

	void Update()
	{
		if (Game_Gloabl_Data.player_freeze && running == false)
		{
			StartCoroutine(freeze());
			running = true;
		}
	}
	IEnumerator freeze ()
	{
		sprite.enabled = true;
		GetComponent<AudioSource>().Play();
		GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(2);
		Game_Gloabl_Data.player_freeze = false;
		running = false;
		sprite.enabled = false;
		GetComponent<AudioSource>().Play();
		GetComponent<ParticleSystem>().Play();
	}
}
