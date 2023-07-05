using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float attackSpeed;
    private float waitAfterAttack;
    public bool isAttacking;

    public GameObject attackIndicator;

    private PlayerController player;

    public EnemyBehavior enemyScript;
    public Collider spear;



    // Animations & Sound
    public Animator skeletonAnimationController;
    SkeletonSoundPlayer skeletonSoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        skeletonSoundPlayer = GameObject.Find("Sound Manager").GetComponent<SkeletonSoundPlayer>();
        attackSpeed = 0.45f;
        waitAfterAttack = 0.3f;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isAttacking == false && enemyScript.stunned == false && (GameManager.instance.isGameActive))
        {
            skeletonAnimationController.SetBool("isAttacking", true);
            isAttacking = true;
            StartCoroutine(attackCooldownTimer());
            IEnumerator attackCooldownTimer()
            {
                yield return new WaitForSeconds(attackSpeed);
                skeletonSoundPlayer.PlayAttackSound();
                spear.enabled = true;
                spear.GetComponent<EnemyDamagePoint>().canDamage = true;
                yield return new WaitForSeconds(waitAfterAttack);
                isAttacking = false;
                skeletonAnimationController.SetBool("isAttacking", false);
                spear.enabled = false;
            }
        }
    }
}
