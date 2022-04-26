using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public float speed = 0;
    public GameObject respawner;
    public GameObject respawn1;
    public GameObject respawn2;
    public GameObject respawn3;
    [SerializeField] private int level = 1;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Death"))
        {
            this.transform.position = (new Vector3(respawner.transform.position.x, respawner.transform.position.y, respawner.transform.position.z));
        }
    }
}
