using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
	public static TextMeshPro	pub_instance;
	public static bool			alive = false;
	private static int			comboval;
	private float				scale;
	private float				tadd;
	private TextMeshPro			tmpro;
	private  int				finish;
	public void MakeCombo(int comboval, Vector3 initpos, GameObject	prefab)
	{
		ComboEffect.comboval = comboval;
		if (alive == false)
		{
			pub_instance = Instantiate(prefab, initpos, Quaternion.identity).GetComponent<TextMeshPro>();
		}
	}
	void Awake()
	{
		alive = true;
		tadd = 0;
		scale = 0.05f;
		tmpro = GetComponent<TextMeshPro>();
		// FIX COMBO COUNTER DOES NOT WORK :)
		pub_instance = gameObject.GetComponent<TextMeshPro>();
	}
	void Update()
	{
		// if (finish == 1)
		// {
		// 	// Destroy(gameObject);
		// 	return ;
		// }
		if (scale > 2.0f)
		{
			tadd = -1;
			finish++;
		}
		if (scale < 0.1f)
		{
			tadd = 1;
			finish++;
		}
		tmpro.text = "x" + comboval;
		scale += Time.deltaTime * tadd;
		tmpro.transform.localScale = new Vector3(scale, scale, 1f);
	}
	void OnDestroy()
	{
		alive = false;
		pub_instance = null;
	}
}