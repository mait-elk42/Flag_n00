using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
	private static int			comboval;
	private float				scale;
	private float				tadd;
	private TextMeshPro			tmpro;
	public void MakeCombo(int comboval, Vector3 initpos, GameObject	prefab)
	{
		ComboEffect.comboval = comboval;
		Instantiate(prefab, initpos, Quaternion.identity).GetComponent<TextMeshPro>();
	}
	void Awake()
	{
		tadd = 0;
		scale = 0.05f;
		tmpro = GetComponent<TextMeshPro>();
	}
	void Update()
	{
		// if (finish == 1)
		// {
		// 	// Destroy(gameObject);
		// 	return ;
		// }
		if (scale > 1.5f)
		{
			tadd = -3;
			Destroy(gameObject);
		}
		if (scale < 0.1f)
			tadd = 3;
		tmpro.text = "x" + comboval;
		scale += Time.deltaTime * tadd;
		tmpro.transform.localScale = new Vector3(scale, scale, 1f);
	}
	void OnDestroy()
	{

	}
}