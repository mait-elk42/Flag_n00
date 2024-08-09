using UnityEngine;

public class P_Small : MonoBehaviour
{
	[SerializeField]
	public int			speed;
	public Transform	p_tr;
	[SerializeField]
	private GameObject	die_effect;
	void Start()
	{
		p_tr = GameObject.Find("/Player").transform;
	}
	void Update()
	{
		if (Player_Movement.player_still_alive == false)
			return ;
		transform.position = Vector3.MoveTowards(transform.position, p_tr.position, 0.01f * speed);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			if (Player_Movement.walking == true)
			{
				Player_Movement.cam_shake = true;
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Player_Movement.score -= 99;
				Destroy(this.gameObject);
			}
			else
			{
				Player_Movement.health_value += 3;
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}
}
