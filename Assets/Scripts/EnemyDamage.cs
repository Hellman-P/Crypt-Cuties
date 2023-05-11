using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private bool isAttacking;

    public GameObject attackIndicator;

    PlayerController player;

    public EnemyBehavior enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        attackSpeed = 0.8f;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isAttacking == false && enemyScript.stunned == false)
        {
            isAttacking = true;
            StartCoroutine(attackCooldownTimer());
            IEnumerator attackCooldownTimer()
            {
                attackIndicator.gameObject.SetActive(true);
                damage = Random.Range(2, 3);
                player.TakeDamage(damage);
                yield return new WaitForSeconds(attackSpeed);

                isAttacking = false;
                attackIndicator.gameObject.SetActive(false);
            }
        }
    }
}
