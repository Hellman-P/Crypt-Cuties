using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Vector3[] spawners;
    public GameObject enemy;
    private float spawnRate = 0.8f;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI levelText;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "KILLS: " + score;

        level = 0;
        levelText.text = "LEVEL: " + level;

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            for (int i = 0; i < spawners.Length; i++)
            {
                Instantiate(enemy, spawners[i], enemy.transform.rotation);
            }
        }
    }
}
