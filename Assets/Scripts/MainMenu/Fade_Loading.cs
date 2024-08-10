using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Loading : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI	t;
	void Start()
	{
		
	}

	void Update()
	{
		Color c = t.color;
		c.a = (float)1 / 255 * Game_Gloabl_Data.loading_opacity;
		print((float)1 / 255 * Game_Gloabl_Data.loading_opacity);
		t.color = c;
		Game_Gloabl_Data.loading_opacity += Game_Gloabl_Data.loading_opacity_incv;
		if (Game_Gloabl_Data.loading_opacity > 255)
			Game_Gloabl_Data.loading_opacity_incv = -1;
		if (Game_Gloabl_Data.loading_opacity < 0)
			Game_Gloabl_Data.loading_opacity_incv = 1;
	}
}
