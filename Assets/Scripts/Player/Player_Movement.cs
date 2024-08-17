using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
	[SerializeField]
	Camera				cam;
	private Vector2		pos;
	public static bool	walking;
	[SerializeField]
	public static bool	cam_shake;
	private float			angle;
	[SerializeField]
	private TextMeshProUGUI	scorevalue;
	public static int		score;
	public static int	health_value;
	public static bool	player_still_alive;
	[SerializeField]
	private Slider	health;
	[SerializeField]
	private	GameObject	Loser_Panel;
	[SerializeField]
	private	TextMeshProUGUI	score_go;

	private bool				show_panel;

	[SerializeField]
	private GameObject			pause_panel;
	[SerializeField]
	private AudioSource	mv_seffect;

	[SerializeField]
	private	GameObject	loose_seff;

	/**
	*			REISZE THE PLAYER 
	*			SCORE++ == SLOW INCR
	*			ENEMIES DIFF SIZE
	*			GLOW 
	*			COMBO
	**/
	void Awake()
	{
		pos = transform.position;
		score = 0;
		health_value = 100;
		player_still_alive = true;
		Loser_Panel.SetActive(false);
		pause_panel.SetActive(false);
	}
	void Start()
	{
		show_panel = false;
		Game_Gloabl_Data.show = true;
		StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && player_still_alive)
		{
			show_panel = show_panel == false;
			Game_Gloabl_Data.game_started = false;
		}
		if (show_panel)
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				show_panel = false;
				Game_Gloabl_Data.game_started = true;
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				Game_Gloabl_Data.Set_High_Score(score);
				Game_Gloabl_Data.load_scene(0);
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Game_Gloabl_Data.Set_High_Score(score);
				Application.Quit();
			}
			pause_panel.SetActive(show_panel);
			return ;
		}
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (player_still_alive == false)
		{
			cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			Loser_Panel.SetActive(true);
			score_go.text = ""+score;
			return ;
		}
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
		transform.position = Vector3.Lerp(transform.position , pos, 0.01f * Time.deltaTime * Game_Gloabl_Data.player_speed);;
		walking = Vector3.Distance(transform.position, pos) > 0.5;
		scorevalue.text = ""+score;
		health.value = health_value;
		if (health_value <= 0)
		{
			Instantiate(loose_seff, transform.position, Quaternion.identity);
			player_still_alive = false;
			cam_shake = true;
		}
	}
}
