using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject Enemy;
    private BoxCollider2D area;
    public List<Enemy> EnemyList = new List<Enemy>();
    [SerializeField] private int maximumEnemy = 1;
    private Transform target;
    public float playerRangeDistance;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        area = GetComponent<BoxCollider2D>();
        StartCoroutine("Spawn", Random.Range(5, 11));
    }

    private IEnumerator Spawn(float delayTime) {
        if(EnemyList.Count < maximumEnemy) {
            Vector3 spawnPos = GetRandomPosition();
            GameObject instance = Instantiate(Enemy, spawnPos, Quaternion.identity);
            EnemyList.Add(instance.GetComponent<Enemy>());
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
        } while(Vector2.Distance(spawnPos, target.position) < playerRangeDistance);

        return spawnPos;
    }

}