using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Furniture : MonoBehaviour
{
	[SerializeField]
	int health = 100;

	[SerializeField]
	float wanderRange = 10f;

	Vector3 destination;

	[SerializeField]
	float delta = 0.1f;

	NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		destination = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(transform.position, destination) < delta)
		{
			destination = GetNewDestination();
			agent.SetDestination(destination);
		}
	}

	Vector3 GetNewDestination()
	{
		Vector2 relativeWander = (Random.insideUnitCircle * wanderRange);
		NavMesh.FindClosestEdge(transform.position + new Vector3(relativeWander.x, 0, relativeWander.y), out NavMeshHit hit, NavMesh.AllAreas);
		return hit.position;
	}

	public void TakeDamage(int damageToTake)
	{
		health -= damageToTake;
		if (health <= 0)
		{
			Die();
		}
	}

	/// <summary>
	/// Temp - Just uses Destroy()
	/// </summary>
	void Die()
	{
		Destroy(gameObject);
	}
}

