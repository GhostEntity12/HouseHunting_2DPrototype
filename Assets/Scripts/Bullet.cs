using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float lifespan = 20;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    int damage = 5;

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            // Should be pooled instead
            Destroy(gameObject);
        }
    }

	private void FixedUpdate()
	{
		transform.position += speed * Time.deltaTime * transform.forward;
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Furniture furniture))
        {
            furniture.TakeDamage(damage);
            // Repool instead
            Destroy(gameObject);
        }
    }
}
