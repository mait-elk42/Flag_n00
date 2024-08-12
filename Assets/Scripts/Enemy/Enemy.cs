using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform	p_tr;
	[SerializeField]
	private GameObject	die_effect;
	[SerializeField]
	private GameObject	p_die_effect;
	private bool		touch_player;
	private int			health_damage = 2;
	private int			score_gift = 50;
	public Enemy_Type	type = Enemy_Type.NORMAL;
	[SerializeField]
	private	GameObject	seff;
	void Start()
	{
		p_tr = GameObject.Find("/Player").transform;
		if (type == Enemy_Type.BOSS1)
		{
			health_damage = 4;
			score_gift = 100;
		}
		if (type == Enemy_Type.BOSS2)
		{
			health_damage = 5;
			score_gift = 120;
		}
		if (type == Enemy_Type.BOSS3)
		{
			health_damage = 10;
			score_gift = 300;
		}
		if (type == Enemy_Type.BOSS4)
		{
			health_damage = 15;
			score_gift = 700;
		}
	}
	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (Player_Movement.player_still_alive == false)
			return ;
		if (!touch_player)
		{
			Vector3 direction = (p_tr.position - transform.position).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
			float offset = 30f;
			transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
			transform.position = Vector3.MoveTowards (transform.position, p_tr.position, 0.01f * Time.deltaTime * Game_Gloabl_Data.enemy_speed);
		}
		if (touch_player)
		{
			if (Player_Movement.walking == true)
			{
				Instantiate(seff, transform.position, Quaternion.identity);
				Game_Gloabl_Data.enemy_speed += 5;
				Game_Gloabl_Data.player_speed += 5;
				Player_Movement.cam_shake = true;
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Player_Movement.score += score_gift;
				Destroy(this.gameObject);
			}
			else
			{
				Instantiate(seff, transform.position, Quaternion.identity);
				Player_Movement.cam_shake = true;
				Instantiate(p_die_effect, p_tr.transform.position, Quaternion.identity);
				Player_Movement.health_value -= health_damage;
				Destroy(this.gameObject);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			touch_player = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			touch_player = false;
		}
	}
}
