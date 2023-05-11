using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movements Variables
    public float speed = 10.0f;
    public float rotationSpeed;
    public float acceleration = 1000f;

    //Combat Variables
    public float playerHP;
    public float maxHP;

    // Hit detection Variables
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = maxHP;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moving and Rotating Player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        playerRB.AddForce(movementDirection * acceleration);
        playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, speed);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Dying
        if (playerHP < 1)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        playerHP -= damageAmount;
    }
    
    public void HPOnKill()
    {
        if (playerHP < maxHP)
        {
            playerHP += 1;
        }
    }
}
