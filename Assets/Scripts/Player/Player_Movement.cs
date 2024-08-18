using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
	[SerializeField]
	Camera						cam;
	private Vector2				pos;
	public static bool			walking;
	[SerializeField]
	public static bool			cam_shake;
	private float				angle;
	[SerializeField]
	private TextMeshProUGUI		scorevalue;
	// public static int			health_value;
	[SerializeField]
	private Slider				health;
	// [SerializeField]
	// private	GameObject			Loser_Panel;
	// [SerializeField]
	// private	TextMeshProUGUI	score_go;

	[SerializeField]
	private AudioSource	mv_seffect;

	[SerializeField]
	private	GameObject	loose_seff;

	/*
	*			REISZE THE PLAYER 
	*			SCORE++ == SLOW INCR
	*			ENEMIES DIFF SIZE
	*			GLOW 
	*			COMBO
	*/
	
	void Awake()
	{
		pos = transform.position;
		Game_Gloabl_Data.player_current_score = 0;
		Game_Gloabl_Data.player_health = 100;
		// Loser_Panel.SetActive(false);
	}
	void Start()
	{
		Game_Gloabl_Data.show = true;
		StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
	}

	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		// if (Game_Gloabl_Data.player_alive == false)
		// {
		// 	cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
		// 	Loser_Panel.SetActive(true);
		// 	score_go.text = ""+score;
		// 	return ;
		// } 
		if (cam_shake)
		{
			cam.transform.position = new Vector3(cam.transform.position.x + (Mathf.Cos(angle) * 0.15f), cam.transform.position.y+ (Mathf.Sin(angle) * 0.15f), cam.transform.position.z);
			angle += Time.deltaTime * 800f;
			if (angle > 360)
			{
				angle = 0;
				cam_shake = false;
				cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			mv_seffect.Play();
			pos = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector3.Lerp(transform.position , pos, 0.01f * Time.deltaTime * Game_Gloabl_Data.player_speed);
		walking = Vector3.Distance(transform.position, pos) > 0.5;
		scorevalue.text = ""+Game_Gloabl_Data.player_current_score;
		health.value = Game_Gloabl_Data.player_health;
		if (Game_Gloabl_Data.player_health <= 0)
		{
			Instantiate(loose_seff, transform.position, Quaternion.identity);
			Game_Gloabl_Data.player_alive = false;
			cam_shake = true;
		}
	}
}
