using TMPro;
using UnityEngine;

public class MainScene_UIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject			pause_panel;
	[SerializeField]
	Camera						cam;
	[SerializeField]
	private	GameObject			Loser_Panel;
	[SerializeField]
	private	TextMeshProUGUI	score_go;
	void Awake()
	{
		pause_panel.SetActive(false);
		Loser_Panel.SetActive(false);
	}
	void Start()
	{
		
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && Game_Gloabl_Data.player_alive)
		{
			pause_panel.SetActive(true);
			Game_Gloabl_Data.game_started = false;
		}
		if (Game_Gloabl_Data.player_alive == false)
		{
			cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			Loser_Panel.SetActive(true);
			score_go.text = ""+Game_Gloabl_Data.player_current_score;
			return ;
		} 
	}
	public void RetryGame()
	{
		Game_Gloabl_Data.Set_High_Score(Game_Gloabl_Data.player_current_score);
		Game_Gloabl_Data.load_scene(1);
	}
	public void GotoMenuGame()
	{
		Game_Gloabl_Data.Set_High_Score(Game_Gloabl_Data.player_current_score);
		Game_Gloabl_Data.load_scene(0);
	}
	public void ExitGame()
	{
		Game_Gloabl_Data.Set_High_Score(Game_Gloabl_Data.player_current_score);
		Application.Quit();
	}
	public void Continue()
	{
		pause_panel.SetActive(false);
		Game_Gloabl_Data.game_started = true;
	}
}
