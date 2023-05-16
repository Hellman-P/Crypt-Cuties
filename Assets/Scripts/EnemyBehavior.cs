using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Movements Variables
    public float speed;
    public float acceleration;
    public float rotationSpeed;

    // Combat Variables
    public float enemyHP;
    public bool stunned;
    private bool invincibiltyFrame;
    private float invincibiltyFrameTime;
    private float stunDuration;
    private float knockBackStrength = 4.0f;
    public Transform enemyPosition;
    private Rigidbody enemyRB;
    PlayerController player;
    PlayerComboDamage playerComboPoints;
    public EnemyDamage isAttacking;

    // Score and Level Keeping
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();

        stunned = false;
        stunDuration = 0.4f;
        invincibiltyFrameTime = 0.3f;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerComboPoints = GameObject.Find("Combo attack area").GetComponent<PlayerComboDamage>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunned == false && player !=null && player.IsAlive() && !isAttacking.isAttacking)
        {
            // Following and rotating towards player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);

            enemyRB.AddForce(enemyRB.transform.forward * acceleration);
            enemyRB.velocity = Vector3.ClampMagnitude(enemyRB.velocity, speed);

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            }
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
        // Remove Skeleton from map, Give player health, give player combo points
        if (enemyHP <= 0)
        {
            player.HPOnKill();
            gameManager.UpdateScore();
            playerComboPoints.GetComboPoints();
            Destroy(gameObject, 0.01f);
            //Make bones scatter
        }
    }
}
