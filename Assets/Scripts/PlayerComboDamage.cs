using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    public float CurrentComboMeter; // make this private later
    public float fullComboMeter;
    public bool inCombo;

    public GameObject comboAttackIndicator;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 0.4f;
        CurrentComboMeter = 10; // make this fill up with kills and make it start at 0
    }

    // Update is called once per frame
    void Update()
    {
        // if the players combo bar is full and they press r start the combo attack
        if (CurrentComboMeter == fullComboMeter && Input.GetKeyDown(KeyCode.R) && !inCombo)
        {
            Debug.Log("You are comboing!");
            while (CurrentComboMeter > 0)
            {
                inCombo = true;
                comboAttackIndicator.gameObject.SetActive(true);
                CurrentComboMeter--;

                StartCoroutine(ComboAttack());
                IEnumerator ComboAttack()
                {
                    yield return new WaitForSeconds(attackSpeed);
                    comboAttackIndicator.gameObject.SetActive(false);
                }
            }
            inCombo = false;
        }
        // if the player tries to combo attack without points
        else if (CurrentComboMeter < fullComboMeter && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("You have no combo points!");
        }
    }
    
    // adding points to combo meter if it's not full and player is not currently in combo
    public void GetComboPoints()
    {
        if (CurrentComboMeter < fullComboMeter && !inCombo)
        {
            Debug.Log("You Got Combo Points!");
            CurrentComboMeter++;
        }
    }

    // Deals damage to skeletons in damage area when cpmboing
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && inCombo)
        {
            StartCoroutine(attackCooldownTimer());
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                damage = Random.Range(3, 5);
                other.GetComponent<EnemyBehavior>().TakeDamage(damage);
            }
        }
    }
}
