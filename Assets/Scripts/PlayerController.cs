using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Movements Variables
    public float speed;
    public float rotationSpeed;
    public float acceleration;

    //Combat Variables
    public float playerHP;
    public float maxHP;

    // Hit detection Variables
    private Rigidbody playerRB;

    //UI
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Slider hpBar;
    public GameObject Blood;

    public GameManager isGameActive;

    // Animations
    public Animator playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = maxHP;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameActive.isGameActive)
        {
            // Moving and Rotating Player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 planeVelocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            movementDirection.Normalize();

            playerRB.AddForce(movementDirection * acceleration);
            planeVelocity = Vector3.ClampMagnitude(planeVelocity, speed);
            playerRB.velocity = planeVelocity + playerRB.velocity.y * Vector3.up;

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            if (horizontalInput == 0 || verticalInput == 0)
            {
                playerAnimationController.SetBool("isIdle", true);
            }
            else
            {
                playerAnimationController.SetBool("isIdle", false);
            }

            hpBar.value = playerHP;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        playerHP -= damageAmount;
        Instantiate(Blood, transform.position + transform.up * 1.5f, Quaternion.identity);
        // Dying
        if (playerHP <= 0)
        {
            playerHP = 0;
            Destroy(gameObject, 0.01f);
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive.isGameActive = false;
            hpBar.value = playerHP;
        }
    }
    
    public bool IsAlive()
    {
        return playerHP > 0;
    }

    public void HPOnKill()
    {
        float heal = Random.Range(1, 2);
        if (playerHP < maxHP)
        {
            playerHP += heal;
        }
        if (playerHP > maxHP)
        {
            playerHP = maxHP;
        }
    }
}
