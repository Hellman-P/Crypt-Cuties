using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Movements Variables
    public float speed = 7.0f;
    public float acceleration = 100f;
    public float rotationSpeed;

    // Combat Variables
    public float enemyHP;
    public bool stunned;
    private bool invincibiltyFrame;
    private float invincibiltyFrameTime;
    private float stunDuration;
    private float knockBackStrength = 4.5f;
    public Transform enemyPosition;
    private Rigidbody enemyRB;
    PlayerController player;
    PlayerComboDamage playerComboPoints;

    // Score and Level Keeping
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 12;

        enemyRB = GetComponent<Rigidbody>();

        stunned = false;
        stunDuration = 0.4f;
        invincibiltyFrameTime = 0.4f;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerComboPoints = GameObject.Find("Combo attack area").GetComponent<PlayerComboDamage>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunned == false)
        {
            // Following and rotating towards player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            enemyRB.AddForce(direction * acceleration);
            enemyRB.velocity = Vector3.ClampMagnitude(enemyRB.velocity, speed);

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        // Remove Skeleton from map, Give player health, give player combo points
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
            player.HPOnKill();
            playerComboPoints.GetComboPoints();
            player.HPOnKill();
            gameManager.UpdateScore();
            //Make bones scatter
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Taking Damage
        if (!invincibiltyFrame)
        {
            invincibiltyFrame = true;

            enemyHP -= damageAmount;
            Debug.Log(enemyHP);

            // Pushing enemy away from player when taking damage
            Vector3 awayFromPlayer = (enemyPosition.position - player.transform.position);
            enemyRB.AddForce(awayFromPlayer * knockBackStrength, ForceMode.Impulse);

            StartCoroutine(stunTimer());
            IEnumerator stunTimer()
            { 
            // Stunning enemy when taking damage
                stunned = true;
                yield return new WaitForSeconds(invincibiltyFrameTime);
                invincibiltyFrame = false;
                enemyRB.velocity=Vector3.zero;

                yield return new WaitForSeconds(stunDuration);
                stunned = false;
            }
        }
    }
}
