using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Movements Variables
    public float speed = 7.0f;
    public float acceleration = 100f;
    public float rotationSpeed;
    public Transform playerPosition;

    // Combat Variables
    public float enemyHP;
    public bool stunned;
    private bool invincibiltyFrame;
    private float invincibiltyFrameTime;
    private float stunDuration;
    private float knockBackStrength = 3f;
    public Transform enemyPosition;
    private Rigidbody enemyRB;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 12;

        enemyRB = GetComponent<Rigidbody>();

        stunned = false;
        stunDuration = 0.4f;
        invincibiltyFrameTime = 0.4f;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunned == false)
        {
            // Following and rotating towards player
            Vector3 direction = (playerPosition.position - transform.position).normalized;

            enemyRB.AddForce(direction * acceleration);
            enemyRB.velocity = Vector3.ClampMagnitude(enemyRB.velocity, speed);

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        // Dying
        if (enemyHP <= 0)
        {
            player.HPOnKill();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Taking Damage
        if (!invincibiltyFrame)
        {
            invincibiltyFrame = true;

            enemyHP -= damageAmount;

            // Pushing enemy away from player when taking damage
            Vector3 awayFromPlayer = (enemyPosition.position - playerPosition.position);
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
