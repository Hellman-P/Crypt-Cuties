using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private float damage;
    public float speed;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDuration());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            damage = Random.Range(4, 7);
            other.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.1f);
            // explode animation
        }
    }
    IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject, 0.1f);
    }
}
