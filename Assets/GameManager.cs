using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3[] spawners;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            Instantiate(enemy, spawners[i], enemy.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies()
    {
        
    }
}
