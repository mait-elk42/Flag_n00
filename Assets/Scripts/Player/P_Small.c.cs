using UnityEngine;

public class P_Small : MonoBehaviour
{
	[SerializeField]
	public int			speed;
	public Transform	p_tr;
	[SerializeField]
	private GameObject	die_effect;
	[SerializeField]
	private	GameObject	seff;
	[SerializeField]
	private	GameObject	hseff;
	void Start()
	{
		p_tr = GameObject.Find("/Player").transform;
	}
	void Update()
	{
		if (Player_Movement.player_still_alive == false)
			return ;
		Vector3 direction = (p_tr.position - transform.position).normalized;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
		var offset = 30f;
		transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
		transform.position = Vector3.MoveTowards(transform.position, p_tr.position, 0.01f * 800 * Time.deltaTime);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			if (Player_Movement.walking == true)
			{
				Instantiate(seff, transform.position, Quaternion.identity);
				Player_Movement.cam_shake = true;
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Player_Movement.score -= 50;
				Destroy(this.gameObject);
			}
			else
			{
				if (Player_Movement.health_value < 100)
				{
					Instantiate(hseff, transform.position, Quaternion.identity);
					Player_Movement.health_value += 10;
				}
				Instantiate(die_effect, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}
}
