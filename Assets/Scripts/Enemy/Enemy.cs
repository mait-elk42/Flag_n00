using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	public int			speed;
	public Transform	p_tr;
	[SerializeField]
	private GameObject	die_effect;
	[SerializeField]
	private GameObject	p_die_effect;
	private bool		touch_player;
	private	long		optimize_effect;
	void Start()
	{
		p_tr = GameObject.Find("/Player").transform;
	}
	void Update()
	{
		if (Game_Gloabl_Data.game_started == false)
			return ;
		if (Player_Movement.player_still_alive == false)
			return ;
		transform.position = Vector3.MoveTowards(transform.position, p_tr.position, 0.01f * speed);
		if (touch_player)
		{
			if (Player_Movement.walking == true)
			{
				Player_Movement.cam_shake = true;
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Player_Movement.score += 99;
				Destroy(this.gameObject);
			}
			else if (optimize_effect % 100 == 0)
			{
				Player_Movement.cam_shake = true;
				Instantiate(p_die_effect, transform.position, Quaternion.identity);
				Player_Movement.health_value -= 2;
				// Destroy(p_tr.gameObject);
				optimize_effect = 0;
			}
		}
		optimize_effect++;
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
