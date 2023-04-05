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

	[SerializeField]
	Transform furnitureDrop;
	Rigidbody dropRb;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		destination = transform.position;
		dropRb = furnitureDrop.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(transform.position, destination) <= delta)
		{
			destination = GetNewDestination();
			agent.SetDestination(destination);
		}
	}

	Vector3 GetNewDestination()
	{
		Vector2 relativeWander = (Random.insideUnitCircle * wanderRange);
		NavMesh.SamplePosition(transform.position + new Vector3(relativeWander.x, 0, relativeWander.y), out NavMeshHit hit, wanderRange, NavMesh.AllAreas);
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
		Drop();
		Destroy(gameObject);
	}

	void Drop()
	{
		furnitureDrop.SetParent(null);
		furnitureDrop.gameObject.SetActive(true);
		dropRb.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, destination);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(destination, 0.1f);
	}
}

