using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Gloabl_Data
{
	public static bool			can_switch;
	public static bool			show;
	public static bool			game_started;
	public static int			loading_opacity = 0;
	public static int			loading_opacity_incv = 1;

	public static void	load_scene(int index)
	{
		can_switch = true;
		show = true;
		SceneManager.LoadScene(index);
		game_started = false;
	}
	public static IEnumerator Wait_Before_Hide_LDNG()
	{
		game_started = false;
		yield return new WaitForSeconds(3);
		show = false;
		game_started = true;
	}
	public static void Set_High_Score(int new_score)
	{
		if (PlayerPrefs.GetInt("_score", -1) < new_score)
			PlayerPrefs.SetInt("_score", new_score);
	}
	public static int Get_High_Score()
	{
		return PlayerPrefs.GetInt("_score", -1);
	}
}
