using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
	[SerializeField]
	Bullet bulletPrefab;
	[SerializeField]
	float cooldown = 2;
	float cooldownTimer;

	Plane defaultPlane = new Plane(Vector3.up, Vector3.zero);

	Vector3 point;

	private void Update()
	{
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
		}
	}

	public void TryFire()
	{
		if (cooldownTimer <= 0)
		{
			Fire();
			cooldownTimer = cooldown;
		}
	}

	public void Fire()
	{
		// Calculate direction
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (defaultPlane.Raycast(ray, out float dist))
		{
			Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation((ray.GetPoint(dist) - transform.position).normalized));
		}
	}
}
