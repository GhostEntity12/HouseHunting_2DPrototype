using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FurnitureDrop : MonoBehaviour
{
	[SerializeField]
	FurnitureInfo info = new();
	[SerializeField]
	FurnitureDropCollectionText collectionText;

	bool collected;

	Rigidbody rb;
	Transform playerTransform;
	Vector3 collectionCachePos;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (collected)
		{
			float distanceRemaining = Vector3.Distance(transform.position, playerTransform.position);
			float step = 7.5f * Mathf.Max(1, distanceRemaining) * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
			float distancePercentage = 1 - (Vector3.Distance(transform.position, playerTransform.position) / Vector3.Distance(collectionCachePos, playerTransform.position));
			transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, distancePercentage);
			if (Vector3.Distance(transform.position, playerTransform.position) < 0.05f)
			{
				Destroy(gameObject);
			}
		}
	}

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlayerController player))
		{
			playerTransform = player.transform;
			collectionCachePos = transform.position;
			player.CollectFurniture(info);
			collectionText.Trigger(info.FurnitureName);
			rb.isKinematic = true;
			collected = true;
			GetComponent<Collider>().enabled = false;
			//Debug.Break();
		}
	}
}

[System.Serializable]
public class FurnitureInfo
{
	public enum Type
	{
		Table
	}
	static readonly Dictionary<Type, string> furnitureNames = new()
	{
		{ Type.Table, "Table" }
	};


	public Type furnitureType;

	public string FurnitureName => furnitureNames[furnitureType];
}
