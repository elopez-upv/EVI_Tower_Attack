using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Canvas canvasToHide;
    [SerializeField] private Canvas canvasToShow;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
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

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        onGameOver.AddListener(GameOver);
        onGameStart.AddListener(StartGame);
    }

    private void Start() {
    }

    private void Update() {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / actualEnemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

        actualEnemiesPerSecond = EnemiesPerSecond();
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy() {
        int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Length);
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
        isSpawning = false;
        clearGameItems();
        Debug.Log("Game Ooooover!");
        canvasToHide.enabled=false;
        canvasToShow.enabled=true;
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
        StartCoroutine(StartWave());
    }
}
