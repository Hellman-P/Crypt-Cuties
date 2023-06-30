using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private float showingTrigger;
    private bool attackOnCooldown;
    private bool damageFrame;

    public GameObject attackIndicator;

    public PlayerComboDamage comboCheck;

    public GameManager isGameActive;

    public PlayerController changeMoveSpeedOnAttack;
    public PlayerController changeRotationSpeedOnAttack;

    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float bulletSpeed = 10;

    private float attackCooldown;

    // Animations
    public Animator playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 0.2f;
        attackCooldown = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {
        // Show attack indicator when holding down space
        if (Input.GetKeyDown(KeyCode.Space) && !comboCheck.inCombo && isGameActive.isGameActive && !attackOnCooldown)
        {
            StartCoroutine(attackCooldownTimer());
            StartCoroutine(modifyMoveSpeedOnAttack());
            attackOnCooldown = true;
            playerAnimationController.SetBool("isAttacking", true);
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

                yield return new WaitForSeconds(attackCooldown);
                playerAnimationController.SetBool("isAttacking", false);
                attackOnCooldown = false;
            }
        }
    }

    IEnumerator modifyMoveSpeedOnAttack()
    {
        changeMoveSpeedOnAttack.speed = 0.7f;
        changeRotationSpeedOnAttack.rotationSpeed = 300f;
        yield return new WaitForSeconds(attackSpeed);
        changeMoveSpeedOnAttack.speed = 3;
        changeRotationSpeedOnAttack.rotationSpeed = 720f;
    }
}
