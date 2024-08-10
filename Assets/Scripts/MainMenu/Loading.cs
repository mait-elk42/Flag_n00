using UnityEngine;

public class loading : MonoBehaviour
{
	void Awake()
	{
		
	}
	void Update()
	{
		gameObject.SetActive(Game_Gloabl_Data.show);
	}
}