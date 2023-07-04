using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerComboDamage : MonoBehaviour
{
    private float damage;
    private float currentComboMeter;
    public float fullComboMeter;
    public bool inCombo;
    public float duration;

    public Slider comboBar;

    public GameObject comboAttackIndicator;

    public PlayerController changeMoveSpeedOnCombo;

    // Animations
    public Animator playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        currentComboMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if the players combo bar is full and they press r start the combo attack
        if (currentComboMeter == fullComboMeter && Input.GetKeyDown(KeyCode.R) && !inCombo)
        {
            StartCoroutine(ComboAttackIndicator());
            playerAnimationController.SetBool("isSpecial", true);
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
        comboAttackIndicator.gameObject.SetActive(true);
        StartCoroutine(ComboDuration());

    }

    IEnumerator ComboDuration()
    {
        yield return new WaitForSeconds(duration);
        inCombo = false;
        playerAnimationController.SetBool("isSpecial", false);
        comboAttackIndicator.gameObject.SetActive(false);
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
