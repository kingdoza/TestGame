using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private Player player;
    public Player Player => player;
    private bool isGame = true;
    public bool IsGame => isGame;
    private SpawnManager enemySpawner;
    [SerializeField] private UI uI;

    private void Awake() {
        enemySpawner = FindObjectOfType<SpawnManager>();
        player.dieEvent.AddListener(OnPlayerDie);
    }

    private void Start() {
        GameStart();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Retry();
        }
    }

    public void GameStart() {
        isGame = true;
        enemySpawner.ClearEnemies();
        enemySpawner.StopSpawning();
        enemySpawner.StartSpawning();
        player.Active();
    }

    public void Retry() => SceneManager.LoadScene("main"); 

    private void GameEnd() {
        isGame = false;
        enemySpawner.StopSpawning();
    }

    private void OnPlayerDie() {
        GameEnd();
        uI.Show();
    }
}
