using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private IEnumerator movingRoutine;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int MaxHp = 5, CurHp = 5;
    private Gun gun;
    private EnemyDetector enemyDetector;
    private bool isControllable = true;
    [SerializeField] private Slider hpBar;
    [HideInInspector] public UnityEvent dieEvent;

    private void Awake() {
        gun = GetComponent<Gun>();
        enemyDetector = transform.GetChild(0).gameObject.GetComponent<EnemyDetector>();
        movingRoutine = MoveTo(transform.position);
        hpBar.value = (float)CurHp / (float)MaxHp;
    }

    public void Active() {
        CurHp = MaxHp;
        gameObject.SetActive(true);
        StopAllCoroutines();
        isControllable = true;
        transform.position = Vector3.zero;
    }

    private void Update() {
        CheckMoveInput();
        CheckAttackInput();
        UpdateHP();
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
        --CurHp;
        RunawayFrom(attackerPos);   //맞으면 반대방향으로 잠깐 도망감
        if(CurHp <= 0) {
            UpdateHP();
            gameObject.SetActive(false);
            dieEvent?.Invoke();
        }
    }

    private void RunawayFrom(Transform attackerPos) {
        StopCoroutine(movingRoutine);
        Vector2 runawayDir = (transform.position - attackerPos.position).normalized;
        const float RunawayPower = 3f;
        Vector2 runawayDestination = (Vector2)transform.position + RunawayPower * runawayDir;
        StartCoroutine(KeepRunningTo(runawayDestination));
    }

    private IEnumerator KeepRunningTo(Vector2 escapePos) {
        float runawayDuration = 0.5f;
        const float RunawaySpeed = 5f;
        isControllable = false;
        while(runawayDuration > 0) {
            transform.position = Vector2.Lerp(transform.position, escapePos, RunawaySpeed * Time.deltaTime);
            runawayDuration -= Time.deltaTime;
            yield return null;
        }
        isControllable = true;
    }

    private void UpdateHP() {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)CurHp / (float)MaxHp, Time.deltaTime * 10);
    }
}
