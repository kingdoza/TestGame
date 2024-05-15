using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour {
    [SerializeField] private IReadOnlyList<Enemy> enemies;
    [SerializeField] private float attackRadius;

    private void Start() {
        enemies = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnManager>().EnemyList;
    }

    public Enemy GetProximateEnemyInRange() {
        Enemy proximateEnemy = GetProximateEnemy();
        if(proximateEnemy == null)
            return null;
        float distance = Vector3.Distance(proximateEnemy.transform.position, transform.position);
        if(distance <= attackRadius)
            return proximateEnemy;
        return null;
    }

    private Enemy GetProximateEnemy() {
        Enemy proximateEnemy = null;
        for(int i = 0; i < enemies.Count; ++i) {
            if(proximateEnemy == null || IsFarther(proximateEnemy, enemies[i]))
                proximateEnemy = enemies[i];
        }
        return proximateEnemy;
    }

    private bool IsFarther(Enemy _standardEnemy, Enemy _comparisonEnemy) {
        float standardDistance = Vector3.Distance(_standardEnemy.transform.position, transform.position);
        float comparisonDistance = Vector3.Distance(_comparisonEnemy.transform.position, transform.position);
        return standardDistance > comparisonDistance;
    }
}
