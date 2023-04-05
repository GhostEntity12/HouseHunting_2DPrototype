using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FurnitureDropCollectionText : MonoBehaviour
{
	float life;
	Vector3 cachePos;
	bool triggered = false;
	TextMeshPro textMesh;

	[SerializeField]
	float maxLife = 0.5f;
	[SerializeField]
	float maxYOffset;

	private void Awake()
	{
		textMesh = GetComponent<TextMeshPro>();
	}

	// Update is called once per frame
	void Update()
	{
		if (triggered)
		{
			life -= Time.deltaTime;
			float percentage = Mathf.Lerp(1, 0, life / maxLife);
			textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - percentage);
			transform.position = Vector3.Lerp(cachePos, cachePos + Vector3.up * maxYOffset, percentage);
			if (life <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

	public void Trigger(string type)
	{
		cachePos = transform.position;
		transform.SetParent(null);
		//transform.position = cachePos;
		triggered = true;
		life = maxLife;
		textMesh.enabled = true;
		textMesh.text = $"Collected {type}";
	}
}
