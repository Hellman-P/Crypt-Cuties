using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3[] spawners;
    public GameObject enemy;
    private float spawnRate = 1.4f;
    private int enemiesRemaing;

    public TextMeshProUGUI levelText;
    public int level;

    public TextMeshProUGUI scoreText;
    private int score;

    public Button startButton;

    private bool spawnRateWait = true;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        level = 1;
        levelText.text = "LEVEL: " + level;

        score = 0;
        scoreText.text = "KILLS: " + score;
    }

    public void StartGame()
    {
        isGameActive = true;
        startButton.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        enemiesRemaing = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesRemaing == 0 && !spawnRateWait)
        {
            UpdateLevel();
            StartCoroutine(SpawnEnemies());
            spawnRateWait = true;
        }
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
        spawnRateWait = false;
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
