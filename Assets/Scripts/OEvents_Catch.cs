using Unity.VisualScripting;
using UnityEngine;

public class OEvents_Catch : MonoBehaviour
{
	public bool grounded;
	void Start()
	{
		grounded = false;
	}
	void Update()
	{
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}
}
