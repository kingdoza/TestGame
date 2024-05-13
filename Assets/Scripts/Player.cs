using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    private IEnumerator movingRoutine;
    [SerializeField] private float moveSpeed;
    private const int MaxHealth = 100;
    private int health = MaxHealth;
    [SerializeField] private Enemy enemy;
    private Gun gun;
    private EnemyDetector enemyDetector;

    private void Start() {
        gun = GetComponent<Gun>();
        enemyDetector = transform.GetChild(0).gameObject.GetComponent<EnemyDetector>();
        movingRoutine = MoveTo(transform.position);
    }

    private void Update() {
        CheckMoveInput();
        CheckAttackInput();
    }

    private void CheckMoveInput() {
        if(Input.GetMouseButtonDown(1)) {
            Vector3 clickPoint = GetMouseWorldPosition();
            StopCoroutine(movingRoutine);
            movingRoutine = MoveTo(clickPoint);
            StartCoroutine(movingRoutine);
        }
    }

    private void CheckAttackInput() {
        if(Input.GetKey(KeyCode.A)) {
            Enemy target = enemyDetector.GetProximateEnemyInRange();
            gun.PullTrigger(target);
        }
    }
    
    private IEnumerator MoveTo(Vector3 _targetPos) {
        while(Vector3.Distance(transform.position, _targetPos) > 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Hit() {
        health -= 20;   //게임매니저내의 수치
        if(health <= 0);
            Destroy(gameObject);
            //게임 종료
    }
}
