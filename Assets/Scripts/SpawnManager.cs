using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject Enemy;
    private BoxCollider2D area;
    private List<Enemy> enemyList = new List<Enemy>();
    public IReadOnlyList<Enemy> EnemyList => enemyList; //읽기전용, 변경불가
    [SerializeField] private int maximumEnemy = 1;
    private Transform target;
    [SerializeField] private float minDistanceFromPlayer;

    private void Awake() {
        target = GameManager.Instance.Player.transform;
        area = GetComponent<BoxCollider2D>();
    }

    public void StartSpawning() {
        StartCoroutine("Spawn", Random.Range(2, 6));
    }

    public void StopSpawning() {
        StopAllCoroutines();
    }

    private IEnumerator Spawn(float delayTime) {
        if(enemyList.Count < maximumEnemy) {
            Vector3 spawnPos = GetRandomPosition();
            GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
            Enemy enemy = instance.GetComponent<Enemy>();
            enemyList.Add(enemy);
        }
        area.enabled = false;
        yield return new WaitForSeconds(delayTime);
        area.enabled = true;
        StartCoroutine("Spawn", Random.Range(5, 11));
    }

    private Vector2 GetRandomPosition() {
        Vector2 basePosition = transform.position; 
        Vector2 size = area.size;
        Vector2 spawnPos;            

        do {
            float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
            float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

            spawnPos = new Vector2(posX, posY);
        } while(Vector2.Distance(spawnPos, target.position) < minDistanceFromPlayer);

        return spawnPos;
    }

    public void RemoveEnemy(Enemy enemy) {
        enemyList.Remove(enemy);
    }

    public void ClearEnemies() {
        for(int i = 0; i < enemyList.Count; i++) {
            Destroy(enemyList[i].gameObject);
            enemyList.RemoveAt(i);
        }
    }
}