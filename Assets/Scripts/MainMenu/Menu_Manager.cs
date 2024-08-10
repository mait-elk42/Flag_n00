using TMPro;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject	cursor;
	private	int			item_index;
	private	Vector3[]  cursor_dests = new Vector3[2];

	[SerializeField]
	private TextMeshProUGUI	score_t;
	void Awake()
	{
		if (Game_Gloabl_Data.can_switch)
			StartCoroutine(Game_Gloabl_Data.Wait_Before_Hide_LDNG());
		else
			Game_Gloabl_Data.game_started = true;
		item_index = 0;
		cursor_dests[0] = cursor.transform.position;
		cursor_dests[1] = cursor.transform.position + (Vector3.down * 120);
		score_t.text = ""+Game_Gloabl_Data.Get_High_Score();
	}

	void Start()
	{
		
	}
	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		item_index += (Input.GetKeyDown(KeyCode.UpArrow) == true) ? -1 : 0;
		item_index -= (Input.GetKeyDown(KeyCode.DownArrow) == true) ? 1 : 0;
		if (item_index < 0)
			item_index = 1;
		if (item_index > 1)
			item_index = 0;
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (item_index == 0)
			{
				Game_Gloabl_Data.load_scene(1);
			}
			if (item_index == 1)
			{
				Application.Quit();
			}
		}
		cursor.transform.position = Vector3.Lerp(cursor.transform.position, cursor_dests[item_index], 0.1f);
	}
}
