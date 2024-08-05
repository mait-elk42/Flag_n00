using Unity.VisualScripting;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
	[SerializeField]
	private float player_speed;
	[SerializeField]
	private float player_jmpforce;
	[SerializeField]
	private Camera cam;
	private Rigidbody2D player_rb;
	private Vector2			velocity;
	private OEvents_Catch	ground_checker;
	void Awake()
	{
		player_rb = GetComponent<Rigidbody2D>();
		ground_checker = transform.GetChild(0).GetComponent<OEvents_Catch>();
	}

	void Start()
	{

	}

	void Update()
	{
		// print("player collided : " + ground_checker.grounded);
		cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, transform.position.x, 0.01f), cam.transform.position.y, cam.transform.position.z);
		if (cam.transform.position.x < 0)
			cam.transform.position = new Vector3(0, cam.transform.position.y, cam.transform.position.z);
		velocity = player_rb.velocity;
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
		{
			if (Input.GetKey(KeyCode.D))
				velocity.x = player_speed;
			if (Input.GetKey(KeyCode.A))
				velocity.x = -player_speed;
		}
		else
			velocity.x = 0;
		if (Input.GetKeyDown(KeyCode.Space) && ground_checker.grounded)
			velocity.y = player_jmpforce;
		player_rb.velocity = velocity;
	}
}
