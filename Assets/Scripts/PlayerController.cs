using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 movementVector = Vector3.zero;
    [SerializeField]
    float speed = 1;
    PlayerGun gun;

    public Vector3 MovementVector => movementVector;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponent<PlayerGun>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            gun.TryFire();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (movementVector * speed * Time.deltaTime));
    }
}
