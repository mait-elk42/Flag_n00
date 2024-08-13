using System.IO;
using TMPro;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject	cursor;
	private bool		load_board_visibility;

	[SerializeField]
	private TextMeshProUGUI	score_t;
	void Awake()
	{
		if (Game_Gloabl_Data.can_switch)
			StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
		else
			Game_Gloabl_Data.game_started = true;
		score_t.text = ""+Game_Gloabl_Data.Get_High_Score();
	}

	void Start()
	{
		print(Directory.GetCurrentDirectory());
	}
	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (load_board_visibility)
		{

		}
	}
	public void	PlayGame()
	{
		Game_Gloabl_Data.load_scene(1);
	}
	public void	LeaderBoardGame()
	{
		Debug.Log("SHOW LOADBOARD");
		load_board_visibility = true;
	}
	public void	ExitGame()
	{
		Application.Quit();
	}
}
