using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
	[SerializeField]
	Camera				cam;
	private Vector2		pos;
	public static bool	walking;
	[SerializeField]
	public static bool	cam_shake;
	private int			angle;
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
	private	RectTransform	rt;
	[SerializeField]
	private	TextMeshProUGUI	score_go;
	private Vector3[]			mpoints = new Vector3[3];
	private int					ms_points_index;
	private Vector3				ms_dest;

	private bool				show_panel;

	[SerializeField]
	private GameObject			pause_panel;


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
		ms_points_index = 0;
		mpoints[0] = rt.transform.position;
		mpoints[1] = rt.transform.position + Vector3.down * 150;
		mpoints[2] = rt.transform.position + Vector3.down * 300;
		ms_dest = rt.transform.position;
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
		if (Input.GetKeyDown(KeyCode.Escape))
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
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (ms_points_index == 0)
				{
					Game_Gloabl_Data.Set_High_Score(score);
					Game_Gloabl_Data.load_scene(1);
				}
				else if (ms_points_index == 1)
				{
					Game_Gloabl_Data.Set_High_Score(score);
					Game_Gloabl_Data.load_scene(0);
				}
				else if (ms_points_index == 2)
				{
					Game_Gloabl_Data.Set_High_Score(score);
					Application.Quit();
				}
				print((ms_points_index == 0) ? "Retry" : (ms_points_index == 1) ? "Menu" : (ms_points_index == 2) ? "Exit" : "NOTHING");
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					ms_points_index++;
				}
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					ms_points_index--;
				}
				if (ms_points_index < 0)
					ms_points_index = 2;
				if (ms_points_index > 2)
					ms_points_index = 0;
				ms_dest = mpoints[ms_points_index];
			}
			rt.transform.position = Vector3.Lerp(rt.transform.position, ms_dest, 0.1f);
			cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			Loser_Panel.SetActive(true);
			score_go.text = ""+score;
			return ;
		}
		if (cam_shake)
		{
			cam.transform.position = new Vector3(cam.transform.position.x + (Mathf.Cos(angle) * 0.5f), cam.transform.position.y+ (Mathf.Sin(angle) * 0.5f), cam.transform.position.z);
			angle+=10;
			if (angle > 360)
			{
				angle = 0;
				cam_shake = false;
				cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			pos = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector3.Lerp(transform.position , pos, 0.05f);;
		walking = Vector3.Distance(transform.position, pos) > 0.5;
		scorevalue.text = ""+score;
		health.value = health_value;
		if (health_value <= 0)
		{
			player_still_alive = false;
			cam_shake = true;
			print("You Loose !");
		}
	}
}
