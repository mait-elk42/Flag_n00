using System.IO;
using TMPro;
using UnityEngine;
using System.Net.Http;
using System;
using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;

public class Menu_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject	hs_panel;
	[SerializeField]
	private GameObject	hs_panel_wait;
	private float		hs_panel_anim;
	[SerializeField]
	private	TextMeshProUGUI[]	hs_names = new TextMeshProUGUI[3];
	[SerializeField]
	private	TextMeshProUGUI[]	hs_scores = new TextMeshProUGUI[3];
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
		Game_Gloabl_Data.Set_High_Score(5000);
		hs_panel.SetActive(false);
		hs_panel_wait.SetActive(false);
	}
	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (load_board_visibility)
		{
			if (hs_panel_anim < 1.0)
				hs_panel_anim += 7f * Time.deltaTime;
			if (hs_panel_anim > 1.0)
				hs_panel_anim = 1.0f;
			hs_panel.transform.localScale = new Vector3(hs_panel_anim, hs_panel_anim, 1);
		}
	}
	public void	PlayGame()
	{
		Game_Gloabl_Data.load_scene(1);
	}
	public async void LeaderBoardGame()
	{
		load_board_visibility = true;
		hs_panel_anim = 0;
		hs_panel.SetActive(true);
		hs_panel_wait.SetActive(true);
		TextMeshProUGUI t = hs_panel_wait.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		t.text = "Wait...";
		t.color = Color.white;
		try{
			var data = await new HttpClient().GetStringAsync("https://psychoflix-mae-nw-default-rtdb.firebaseio.com/game/.json");
			fsData all = fsJsonParser.Parse(data);
			List<fsData>	ranks = all.AsList;
			hs_names[0].text = ranks[0].AsDictionary["uname"].ToString().Replace("\"", "");
			hs_scores[0].text = ranks[0].AsDictionary["mscore"].ToString().Replace("\"", "");

			hs_names[1].text = ranks[1].AsDictionary["uname"].ToString().Replace("\"", "");
			hs_scores[1].text = ranks[1].AsDictionary["mscore"].ToString().Replace("\"", "");

			hs_names[2].text = ranks[2].AsDictionary["uname"].ToString().Replace("\"", "");
			hs_scores[2].text = ranks[2].AsDictionary["mscore"].ToString().Replace("\"", "");

			Debug.Log("completed :) " + Time.deltaTime);
			hs_panel_wait.SetActive(false);
		}catch (Exception e)
		{
			Debug.Log("Error :(" + e.Message);
			t.text = "Ooops! Cannot Load :(";
			t.color = Color.red;
		}
	}
	public void closeLeaderBoardGame()
	{
		hs_panel.SetActive(false);
		load_board_visibility = false;
	}
	public void	ExitGame()
	{
		Application.Quit();
	}
}
