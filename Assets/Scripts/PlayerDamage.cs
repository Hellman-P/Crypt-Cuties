using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private float showingTrigger;
    private bool showingAttack;
    private bool damageFrame;

    public GameObject attackIndicator;

    public PlayerComboDamage comboCheck;

    public GameManager isGameActive;

    public PlayerController changeMoveSpeedOnAttack;

    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 0.3f;
        showingTrigger = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Show attack indicator when holding down space
        if (Input.GetKey(KeyCode.Space) && showingAttack == false && !comboCheck.inCombo && isGameActive.isGameActive)
        {
            showingAttack = true;
            StartCoroutine(attackCooldownTimer());
            StartCoroutine(modifyMoveSpeedOnAttack());
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                damageFrame = true;
                attackIndicator.gameObject.SetActive(true);
                yield return new WaitForSeconds(showingTrigger);
                attackIndicator.gameObject.SetActive(false);
                damageFrame = false;
                showingAttack = false;
            }
        }
    }

    IEnumerator modifyMoveSpeedOnAttack()
    {
        changeMoveSpeedOnAttack.speed = 0.7f;
        yield return new WaitForSeconds(attackSpeed + 0.2f);
        changeMoveSpeedOnAttack.speed = 3;
    }
    

    // Dealing damage with attack
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && damageFrame && !comboCheck.inCombo)
        {
            damage = Random.Range(6, 12);
            other.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }
}
