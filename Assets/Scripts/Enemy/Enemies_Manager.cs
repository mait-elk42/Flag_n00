using UnityEngine;

public class Enemies_Manager : MonoBehaviour
{
	private	int		delay;
	[SerializeField]
	private GameObject[]	enemies_models;
	[SerializeField]
	private GameObject	psmall_model;
	private	Transform[]		positions = new Transform[1024];
	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			positions[i] = transform.GetChild(i);
		}
	}

	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (Player_Movement.player_still_alive == false)
			return ;
		if (delay >= 400)
		{
			int r = Random.Range(0, 999) % (transform.childCount - 1);
			if (Random.Range(0, 100) > 20)
			{
				int eni = Random.Range(0, enemies_models.Length) % enemies_models.Length;
				print("en : "+eni);
				GameObject e = Instantiate(enemies_models[eni], positions[r].position, Quaternion.identity);
				e.GetComponent<Enemy>().type = (Enemy_Type)eni;
				if (eni != 0)
					e.transform.localScale += new Vector3(eni * 0.3f, eni * 0.3f, 0);
				else
					e.transform.localScale += new Vector3(0.5f, 0.5f, 0);
			}
			else
			{
				GameObject e = Instantiate(psmall_model, positions[r].position, Quaternion.identity);
				e.transform.localScale = Vector3.one * Random.Range(1.0f, 1.5f);
			}
			delay = 0;
		}
		delay += (int)Mathf.Ceil(Game_Gloabl_Data.enemy_speed / 2.5f * Time.deltaTime);
	}
}
