using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character {
    [SerializeField] private float playerRangeDistance, speed;
    private Transform target;
    private bool isFollowingMode = false;
    [HideInInspector] public UnityEvent dieEvent;
    private SpawnManager enemySpawner;
    private IEnumerator followingRoutine;

    private new void Awake() {
        base.Awake();
        enemySpawner = FindObjectOfType<SpawnManager>();
        target = GameManager.Instance.Player.transform;
        const float IntialDelayTime = 1f;
        Invoke("Wander", IntialDelayTime);
    }


    private void FixedUpdate() {  
        DetectPlayer();
    }

    private void DetectPlayer() {
        if(!GameManager.Instance.IsGame)
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if(distanceToPlayer > playerRangeDistance && isFollowingMode)
            EnterWanderingMode();
        else if(distanceToPlayer <= playerRangeDistance && !isFollowingMode)
            EnterFollowingMode();
    }

    private IEnumerator KeepFollowingPlayer() {
        while(GameManager.Instance.IsGame) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        EnterWanderingMode();
    }

    private void EnterFollowingMode() {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isFollowingMode = true;
        CancelInvoke("Wander");
        followingRoutine = KeepFollowingPlayer();
        StartCoroutine(followingRoutine);
    }

    private void EnterWanderingMode() {
        GetComponent<SpriteRenderer>().color = Color.white;
        isFollowingMode = false;
        StopCoroutine(followingRoutine);
        Wander();
    }

    private void Wander() {
        float nextMoveX = Random.Range(-1, 2);
        float nextMoveY = Random.Range(-1, 2);
        GetComponent<Rigidbody2D>().velocity = new Vector2(nextMoveX, nextMoveY);
        const float WanderingDuration = 1f;
        Invoke("Wander", WanderingDuration);
    }

    private new void Die() {
        base.Die();
        dieEvent.Invoke();
        enemySpawner.RemoveEnemy(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeHit(transform);
        }
    }
}
