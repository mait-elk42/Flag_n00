using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	[SerializeField]
	Camera				cam;
	private Vector2		pos;
	public static bool	walking;
	[SerializeField]
	public static bool	cam_shake;
	private int			angle;
	[SerializeField]
	private TextMeshProUGUI	scorevalue;
	public static long		score;
	void Awake()
	{
		pos = transform.position;
	}
	void Start()
	{
		
	}

	void Update()
	{
		if (cam_shake)
		{
			cam.transform.position = new Vector3(cam.transform.position.x + (Mathf.Cos(angle) * 0.5f), cam.transform.position.y+ (Mathf.Sin(angle) * 0.5f), cam.transform.position.z);
			angle+=10;
			if (angle > 360)
			{
				angle = 0;
				cam_shake = false;
				cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			pos = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		transform.position = Vector3.Lerp(transform.position , pos, 0.05f);;
		walking = Vector3.Distance(transform.position, pos) > 0.5;
		scorevalue.text = ""+score;
		// if (walking)  print("Walking :" + walking);
	}

}
