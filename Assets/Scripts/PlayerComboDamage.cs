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
    public float duration;
    private bool damageframe;

    public GameObject comboAttackIndicator;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 0.08f;
        CurrentComboMeter = 0; // make this fill up with kills and make it start at 0
    }

    // Update is called once per frame
    void Update()
    {
        // if the players combo bar is full and they press r start the combo attack
        if (CurrentComboMeter == fullComboMeter && Input.GetKeyDown(KeyCode.R) && !inCombo)
        {
            StartCoroutine(ComboAttackIndicator());
        }
        // if the player tries to combo attack without points
        else if (CurrentComboMeter < fullComboMeter && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("You have no combo points!");
        }
    }

    IEnumerator ComboAttackIndicator()
    {
        yield return new WaitForEndOfFrame();

        CurrentComboMeter = 0;
        inCombo = true;
        StartCoroutine(ComboDuration());
        while (inCombo)
        {
            comboAttackIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(attackSpeed);
            comboAttackIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    IEnumerator ComboDuration()
    {
        yield return new WaitForSeconds(duration);
        inCombo = false;
    }

    // adding points to combo meter if it's not full and player is not currently in combo
    public void GetComboPoints()
    {
        if (CurrentComboMeter < fullComboMeter && !inCombo)
        {
            CurrentComboMeter++;
        }
    }

    // Deals damage to skeletons in damage area when cpmboing
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && inCombo)
        {
            damage = Random.Range(3, 5);
            other.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }
}
