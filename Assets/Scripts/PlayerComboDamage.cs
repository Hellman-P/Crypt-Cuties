using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerComboDamage : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private float currentComboMeter;
    public float fullComboMeter;
    public bool inCombo;
    public float duration;

    public Slider comboBar;

    public GameObject comboAttackIndicator;

    public PlayerController changeMoveSpeedOnCombo;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 0.08f;
        currentComboMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if the players combo bar is full and they press r start the combo attack
        if (currentComboMeter == fullComboMeter && Input.GetKeyDown(KeyCode.R) && !inCombo)
        {
            StartCoroutine(ComboAttackIndicator());
        }
        // if the player tries to combo attack without points
        else if (currentComboMeter < fullComboMeter && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("You have no combo points!");
        }
        // showing combo points
        comboBar.value = currentComboMeter;
    }

    IEnumerator ComboAttackIndicator()
    {
        yield return new WaitForEndOfFrame();
        changeMoveSpeedOnCombo.speed = 6;
        currentComboMeter = 0;
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
        changeMoveSpeedOnCombo.speed = 3;
    }

    // adding points to combo meter if it's not full and player is not currently in combo
    public void GetComboPoints()
    {
        if (currentComboMeter < fullComboMeter && !inCombo)
        {
            currentComboMeter++;
        }
    }

    // Deals damage to skeletons in damage area when cpmboing
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && inCombo)
        {
            damage = Random.Range(3, 9);
            other.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }
}
