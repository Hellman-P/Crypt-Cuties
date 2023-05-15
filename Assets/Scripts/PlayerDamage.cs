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
        if (Input.GetKey(KeyCode.Space) && showingAttack == false && !comboCheck.inCombo)
        {
            showingAttack = true;
            StartCoroutine(attackCooldownTimer());
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

    // Deals damage to skeletons in damage area when holding space
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && damageFrame && !comboCheck.inCombo)
        {
            damage = Random.Range(4, 7);
            other.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }
}
