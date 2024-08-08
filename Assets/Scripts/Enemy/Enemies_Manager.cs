using UnityEngine;

public class Enemies_Manager : MonoBehaviour
{
	private	int		delay;
	[SerializeField]
	private GameObject	enemy_model;
	[SerializeField]
	private GameObject	psmall_model;
	private	Transform[]		positions = new Transform[1024];
	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			positions[i] = transform.GetChild(i);
		}
	}

	void Update()
	{
		if (delay == 200)
		{
			int r = Random.Range(0, 999) % (transform.childCount - 1);
			if (GameObject.Find("/Player") == null)
					return ;
			if (Random.Range(0, 100) > 10)
				Instantiate(enemy_model, positions[r].position, Quaternion.identity);
			else
				Instantiate(psmall_model, positions[r].position, Quaternion.identity);
			delay = 0;
		}
		delay++;
	}
}
