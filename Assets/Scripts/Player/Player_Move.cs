using Unity.VisualScripting;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	[SerializeField]
	private float player_speed;
	[SerializeField]
	private float player_jmpforce;
	private Rigidbody2D player_rb;
	private Vector2		velocity;
	void Awake()
	{
		player_rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{

	}

	void Update()
	{
		velocity = player_rb.velocity;
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
		{
			if (Input.GetKey(KeyCode.D))
				velocity.x = Time.deltaTime * player_speed;
			if (Input.GetKey(KeyCode.A))
				velocity.x = Time.deltaTime * -player_speed;
		}
		else
			velocity.x = 0;
		if (Input.GetKeyDown(KeyCode.Space))
			velocity.y = player_jmpforce;
		player_rb.velocity = velocity;
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, -transform.up * 1f);
	}
}
