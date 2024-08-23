using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
	private Camera				cam;
	private Vector2				pos;
	public static bool			walking;
	[SerializeField]
	public static bool			cam_shake;
	private float				angle;
	[SerializeField] private TextMeshProUGUI		scorevalue;
	[SerializeField] private Slider				health;

	[SerializeField] private AudioSource	mv_seffect;
	private	AudioSource	loose_seff;

	public static int combohit = 0;
	[SerializeField] private GameObject	comboprefab;

	[SerializeField] private Sprite[] pframes;
	private SpriteRenderer sprite;

	/*
	*			GLOW 
	*			COMBO
	*/
	
	void Awake()
	{
		Application.targetFrameRate = 60;
		pos = transform.position;
		Game_Gloabl_Data.player_current_score = 0;
		Game_Gloabl_Data.player_health = 100;
		Game_Gloabl_Data.player_alive = true;
		cam = Camera.main;
		loose_seff = transform.GetChild(1).GetComponent<AudioSource>();
		sprite = GetComponent<SpriteRenderer>();
	}
	void Start()
	{
		Game_Gloabl_Data.show = true;
		StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
	}
	void health_sprite_manage()
	{
		int	phealth = Game_Gloabl_Data.player_health;
		if (phealth >= 0 && phealth < 20)
			sprite.sprite = pframes[4];
		if (phealth >= 20 && phealth < 40)
			sprite.sprite = pframes[3];
		if (phealth >= 40 && phealth < 60)
			sprite.sprite = pframes[2];
		if (phealth >= 60 && phealth < 80)
			sprite.sprite = pframes[1];
		if (phealth >= 80 && phealth <= 100)
			sprite.sprite = pframes[0];
	}
	void Update()
	{
		health_sprite_manage();
		if (Input.GetKeyDown(KeyCode.W))
			Game_Gloabl_Data.player_health -= 20;
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (cam_shake)
		{
			cam.transform.position = new Vector3(cam.transform.position.x + (Mathf.Cos(angle) * 0.15f), cam.transform.position.y+ (Mathf.Sin(angle) * 0.15f), cam.transform.position.z);
			angle += Time.deltaTime * 600f;
			if (angle > 360)
			{
				angle = 0;
				cam_shake = false;
				cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			}
		}
		if (Input.GetMouseButtonDown(0) && !Game_Gloabl_Data.player_freeze)
		{
			mv_seffect.Play();
			pos = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector3.Lerp(transform.position , pos, 0.01f * Time.deltaTime * Game_Gloabl_Data.player_speed);
		if (Vector3.Distance(transform.position, pos) < 0.5)
		{
			if (combohit > 1)
			{
				new ComboEffect().MakeCombo(combohit, transform.position, comboprefab);
				Game_Gloabl_Data.player_current_score += combohit * Enemy.score_gift;
			}
			walking = false;
			combohit = 0;
		}
		else
			walking = true;
		scorevalue.text = ""+Game_Gloabl_Data.player_current_score;
		health.value = Game_Gloabl_Data.player_health;
		if (Game_Gloabl_Data.player_health <= 0)
		{
			loose_seff.Play();
			Game_Gloabl_Data.player_alive = false;
			Game_Gloabl_Data.game_started = false;
			cam_shake = true;
		}
	}
}
