using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private float attackCooldown;
    private bool showingAttack;

    public GameObject attackIndicator;

    EnemyBehavior enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<EnemyBehavior>();
        attackSpeed = 0.2f;
        attackCooldown = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        // Show attack indicator when holding down space
        if (Input.GetKey(KeyCode.Space) && showingAttack == false)
        {
            showingAttack = true;
            attackIndicator.gameObject.SetActive(true);
            StartCoroutine(attackCooldownTimer());
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                attackIndicator.gameObject.SetActive(false);
                yield return new WaitForSeconds(attackCooldown);
                showingAttack = false;
            }
        }
    }

    // Deals damage to skeletons in damage area when holding space
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(attackCooldownTimer());
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                damage = Random.Range(2, 3);
                other.GetComponent<EnemyBehavior>().TakeDamage(damage);
            }
        }
    }
}
