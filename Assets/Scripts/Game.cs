using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Game : MonoBehaviour
{
    [Header("Monster Spawning Settings")]
    public int spawnCount = 5; 
    public Vector3 spawnArea = new Vector3(10, 0, 10);
    public float spawnHeight = 0f;
    public GameObject[] monsterPrefabs;
    public float spawnInterval = 5f;
    [Header("Game Settings")]
    public float timeLimit = 480f;
    public string winScene = "Win";
    [Header("UI")]
    public TextMeshProUGUI timerText;
    private float spawnTimer;
    void Start()
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
            monsterPrefabs = Resources.LoadAll<GameObject>("monsters");
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogError("No prefabs found in Resources/monsters!");
            enabled = false;
            return;
        }
        spawnTimer = spawnInterval;
        for (int i = 0; i < spawnCount; i++)
            SpawnRandomMonster();
    }
    void Update()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0f)
        {
            SceneManager.LoadScene(winScene);
            return;
        }
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeLimit / 60f);
            int seconds = Mathf.FloorToInt(timeLimit % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnRandomMonster();
            spawnTimer = spawnInterval;
        }
    }
    void SpawnRandomMonster()
    {
        if (monsterPrefabs.Length == 0) return;
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        Vector3 randomPos = new Vector3(
            transform.position.x + Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
            spawnHeight,
            transform.position.z + Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f)
        );
        Instantiate(prefab, randomPos, Quaternion.identity);
    }
}