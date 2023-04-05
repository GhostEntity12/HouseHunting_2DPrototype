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
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
		transform.position += transform.forward * Time.deltaTime * speed;
	}

	private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit?");
        if (other.TryGetComponent(out Furniture furniture))
        {
            Debug.Log("Hit!");
            furniture.TakeDamage(damage);
        }
    }
}
