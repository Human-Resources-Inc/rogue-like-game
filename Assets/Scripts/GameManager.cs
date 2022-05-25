using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int currentFloor { get; set; } = 1;
    public int enemiesKilled { get; set; } = 0;
    public int enemiesTotal { get; private set; }
    public int enemiesForNextFloor { get; private set; }
    public bool elevatorActive { get; private set; } = false;


    [SerializeField] private GameObject elevatorPrefab;

    private bool gameActive = false;


    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameProcess");
        StartCoroutine(RecalculateEnemies());
        gameActive = true;
    }

    public IEnumerator RecalculateEnemies()
    {
        yield return new WaitForSeconds(1);
        enemiesTotal = Resources.FindObjectsOfTypeAll(typeof(EnemyBase)).Length;
        Debug.Log($"{enemiesTotal} enemies found on the floor {currentFloor}");
        enemiesForNextFloor = Mathf.RoundToInt(enemiesTotal / Random.Range(3, 5));

    }

    private void SpawnElevator()
    {
        var rooms = FindObjectsOfType<RoomEvents>();
        Debug.Log(rooms.Length);
        var room = rooms[Random.Range(0, rooms.Length - 1)].gameObject;

        GameObject spawnedElevator = Instantiate(elevatorPrefab);
        spawnedElevator.transform.SetParent(room.transform);
        spawnedElevator.transform.localPosition = new Vector3(8, 8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            if (enemiesKilled != 0 && enemiesKilled >= enemiesForNextFloor && !elevatorActive)
            {
                Debug.Log("Spawning elevator");
                elevatorActive = true;
                SpawnElevator();
            }
        }
    }
}
