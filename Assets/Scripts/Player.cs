using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    private IEnumerator movingRoutine;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int life = 5;
    private Gun gun;
    private EnemyDetector enemyDetector;
    private bool isControllable = true;

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
        if(Input.GetMouseButtonDown(1) && isControllable) {
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

    public void HitFrom(Transform attackerPos) {
        --life;
        StartCoroutine(RunawayFrom(attackerPos));   //맞으면 반대방향으로 잠깐 도망감
        if(life <= 0) {
            Destroy(gameObject);
            //게임 종료
        }
    }

    private IEnumerator RunawayFrom(Transform attackerPos) {
        StopCoroutine(movingRoutine);
        isControllable = false;
        Vector2 runawayDir = (transform.position - attackerPos.position).normalized;
        Vector2 runawayDestination = (Vector2)transform.position + 4 * runawayDir;
        float runawayDuration = 0.8f;
        const float RunawaySpeed = 5f;
        while(runawayDuration > 0) {
            transform.position = Vector2.Lerp(transform.position, runawayDestination, RunawaySpeed * Time.deltaTime);
            runawayDuration -= Time.deltaTime;
            yield return null;
        }
        isControllable = true;
    }
}
