using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Enemy_Type
{
	NORMAL,
	BOSS1,
	BOSS2,
	BOSS3,
	BOSS4,
}

public class Game_Gloabl_Data
{
	public static bool			can_switch;
	public static bool			show;
	public static bool			game_started;
	public static int			loading_opacity = 0;
	public static int			loading_opacity_incv = 1;
	public static int			player_speed = 1200;
	public static int			enemy_speed = 700;
	public static bool			player_alive = true;
	public static int			player_current_score = 0;
	public static int			player_health = 100;


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
		yield return new WaitForSeconds(1);
		show = false;
		game_started = true;
	}
	public static void Set_High_Score(int new_score)
	{
		if (PlayerPrefs.GetInt("_score", -1) < new_score)
		{
			PlayerPrefs.SetInt("_score", new_score);
			// try_upload(new_score);
		}
	}
	public static async void try_upload(int new_score)
	{
		var data = await new HttpClient().GetStringAsync("https://psychoflix-mae-nw-default-rtdb.firebaseio.com/game/.json");
		fsData all = fsJsonParser.Parse(data);
		List<fsData>	ranks = all.AsList;
		Dictionary<string, fsData> p1 = ranks[0].AsDictionary;
		Dictionary<string, fsData> p2 = ranks[1].AsDictionary;
		Dictionary<string, fsData> p3 = ranks[2].AsDictionary;

		if (p1["mscore"].AsInt64 < new_score)
		{
			all.AsList[0].AsDictionary["uname"] = new fsData(Get_Uname());
			all.AsList[0].AsDictionary["mscore"] = new fsData(new_score);
		}

		else if (p2["mscore"].AsInt64 < new_score)
		{
			all.AsList[1].AsDictionary["uname"] = new fsData(Get_Uname());
			all.AsList[1].AsDictionary["mscore"] = new fsData(new_score);
		}

		else if (p3["mscore"].AsInt64 < new_score)
		{
			all.AsList[2].AsDictionary["uname"] = new fsData(Get_Uname());
			all.AsList[2].AsDictionary["mscore"] = new fsData(new_score);
		}

		var content = new StringContent(all.ToString(), Encoding.UTF8, "application/json");
		HttpResponseMessage s = await new HttpClient().PutAsync("https://psychoflix-mae-nw-default-rtdb.firebaseio.com/game/.json", content);
		Debug.Log("finish == " + s);
	}
	public static int Get_High_Score()
	{
		return PlayerPrefs.GetInt("_score", -1);
	}
	public static String Get_Uname()
	{
		return PlayerPrefs.GetString("_uname", "You");
	}
}
