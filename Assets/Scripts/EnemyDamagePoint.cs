using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagePoint : MonoBehaviour
{
    private PlayerController player;
    private float damage;
    public bool canDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            damage = Random.Range(7, 13);
            player.TakeDamage(damage);
            canDamage = false;
        }
    }
}
