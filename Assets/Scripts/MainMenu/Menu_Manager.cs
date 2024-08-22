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
	private GameObject	Uname_panel;


	[SerializeField]
	private GameObject	my_hs;
	void Awake()
	{
		// PlayerPrefs.SetInt("_score", 0);
		if (Game_Gloabl_Data.can_switch)
			StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
		else
			Game_Gloabl_Data.game_started = true;
		my_hs.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+Game_Gloabl_Data.Get_Uname();
		my_hs.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Game_Gloabl_Data.Get_High_Score();
	}

	void Start()
	{
		hs_panel.SetActive(false);
		hs_panel_wait.SetActive(false);
		Uname_panel.SetActive(!Game_Gloabl_Data.can_switch && !PlayerPrefs.HasKey("_uname"));
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
		return ;
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
			hs_names[0].text = ranks[0].AsDictionary["uname"].AsString;
			hs_scores[0].text = ranks[0].AsDictionary["mscore"].AsInt64+"";

			hs_names[1].text = ranks[1].AsDictionary["uname"].AsString;
			hs_scores[1].text = ranks[1].AsDictionary["mscore"].AsInt64+"";

			hs_names[2].text = ranks[2].AsDictionary["uname"].AsString;
			hs_scores[2].text = ranks[2].AsDictionary["mscore"].AsInt64+"";

			print("completed :) " + Time.deltaTime);
			hs_panel_wait.SetActive(false);
		}catch (Exception e)
		{
			print("Error :(" + e.Message);
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
	public void save_uname(TextMeshProUGUI t)
	{
		print(">> [" + t.text + "]:" + t.text.Length);
		if (t.text.Length <= 1)
			return ;
		Uname_panel.SetActive(false);
		PlayerPrefs.SetString("_uname", t.text);
		Awake();
	}
}
