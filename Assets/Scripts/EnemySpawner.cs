using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Canvas canvasToHide;
    [SerializeField] private Canvas canvasToShow;
    [SerializeField] private GameObject game;

    [SerializeField] TextMeshProUGUI waveCountDownText;
    [SerializeField] TextMeshProUGUI waveCounterText;
    [SerializeField] TextMeshProUGUI enemiesDestroyedText;
    [SerializeField] TextMeshProUGUI gameOverText;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 6f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent onGameOver = new UnityEvent();
    public static UnityEvent onGameStart = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;

    private float actualEnemiesPerSecond;
    private bool isSpawning = false;
    private bool gameIsStarted = false;

    private float countDown = 0f;
    private int enemiesDestroyed = 0;

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        onGameOver.AddListener(GameOver);
        onGameStart.AddListener(StartGame);
    }

    private void Start() {
    }

    private void Update() {
        enemiesDestroyedText.text = enemiesDestroyed.ToString();
        if (isSpawning == false && enemiesAlive == 0 && enemiesLeftToSpawn == 0 && gameIsStarted == true) {
            if (countDown <= 0f) {
                StartCoroutine(StartWave());
                countDown = timeBetweenWaves;
            }


            if (countDown < 0f) {
                countDown = 0f;
            }
            waveCountDownText.text = Mathf.Floor(countDown).ToString();

            countDown -= Time.deltaTime;
        }

        if (!isSpawning) return;
        
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / actualEnemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
        enemiesDestroyed++;
    }

    private IEnumerator StartWave() {
        waveCounterText.text = currentWave.ToString();
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        actualEnemiesPerSecond = EnemiesPerSecond();
        yield return new WaitForSeconds(0.5f);
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
    }

    private void SpawnEnemy() {
        int enemyPrefabIndex;
        if (currentWave < 3) {
            enemyPrefabIndex = 0;
        } else {
            enemyPrefabIndex = Random.Range(0, enemyPrefabs.Length);
        }
        GameObject prefabToSpawn = enemyPrefabs[enemyPrefabIndex];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond() {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    private void GameOver() {
        Debug.Log("Game Ooooover!");
        gameOverText.gameObject.SetActive(true);
        isSpawning = false;
        gameIsStarted = false;
        StartCoroutine(ClearPreviousGame());
    }

    private IEnumerator ClearPreviousGame() {
        yield return new WaitForSeconds(8f);
        clearGameItems();
        gameOverText.gameObject.SetActive(false);
        canvasToHide.enabled=false;
        canvasToShow.enabled=true;
        game.SetActive(false);
    }

    private void clearGameItems() {
        GameObject[] allGameItems = GameObject.FindGameObjectsWithTag("gameItem");

        foreach (GameObject gameItem in allGameItems)
        {
            Destroy(gameItem);
        }
    }

    private void StartGame() {
        Debug.Log("Starting game!");
        enemiesAlive = 0;
        enemiesLeftToSpawn = 0;
        currentWave = 1;
        timeSinceLastSpawn = 0f;
        LevelManager.main.SetCurrency();
        countDown = timeBetweenWaves;
        enemiesDestroyed = 0;
        gameIsStarted = true;
    }
}
