using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Vector3[] spawners;
    public GameObject enemy;
    private float spawnRate = 1.4f;
    private int enemiesRemaing;

    public TextMeshProUGUI levelText;
    private int level;

    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        levelText.text = "LEVEL: " + level;

        score = 0;
        scoreText.text = "KILLS: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesRemaing = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesRemaing == 0)
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5.0f);
        UpdateLevel();
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnEnemies());
    }

    // Spawn a number of enemies according to current level
    IEnumerator SpawnEnemies()
    {
        for (int l = 0; l < level; l++)
        {
            yield return new WaitForSeconds(spawnRate);
            for (int s = 0; s < spawners.Length; s++)
            {
                Instantiate(enemy, spawners[s], enemy.transform.rotation);
            }
        }
    }

    private void UpdateLevel()
    {
        level++;
        levelText.text = "LEVEL: " + level;
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = "KILLS: " + score;
    }
}
