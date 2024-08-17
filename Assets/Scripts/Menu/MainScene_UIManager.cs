using UnityEngine;

public class MainScene_UIManager : MonoBehaviour
{
	void Awake()
	{
		
	}
	void Start()
	{
		
	}
	void Update()
	{
		
	}
	public void RetryGame()
	{
		Game_Gloabl_Data.Set_High_Score(Player_Movement.score);
		Game_Gloabl_Data.load_scene(1);
	}
	public void GotoMenuGame()
	{
		Game_Gloabl_Data.Set_High_Score(Player_Movement.score);
		Game_Gloabl_Data.load_scene(0);
	}
	public void ExitGame()
	{
		Game_Gloabl_Data.Set_High_Score(Player_Movement.score);
		Application.Quit();
	}
	public void Continue()
	{
		print("con");
		Player_Movement.show_panel = false;
		Game_Gloabl_Data.game_started = true;
	}
}
