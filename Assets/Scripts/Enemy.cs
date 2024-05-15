using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float loading = 1;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    [SerializeField] private float playerRangeDistance, speed;
    private Transform target;
    private bool isFollowingMode = false;
    [SerializeField] private int health;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Wander", loading);
    }


    private void FixedUpdate() {  
        DetectPlayer();
    }

    private void DetectPlayer() {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if(distanceToPlayer > playerRangeDistance && isFollowingMode)
            EnterWanderingMode();
        else if(distanceToPlayer <= playerRangeDistance && !isFollowingMode)
            EnterFollowingMode();
    }

    private IEnumerator KeepFollowingPlayer() {
        while(isFollowingMode) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void EnterFollowingMode() {
        sprite.color = Color.red;
        isFollowingMode = true;
        rigid.velocity = Vector2.zero;
        CancelInvoke("Wander");
        StartCoroutine("KeepFollowingPlayer");
    }

    private void EnterWanderingMode() {
        sprite.color = Color.white;
        isFollowingMode = false;
        Wander();
    }

    private void Wander() {
        float nextMoveX = Random.Range(-1, 2);
        float nextMoveY = Random.Range(-1, 2);
        rigid.velocity = new Vector2(nextMoveX, nextMoveY);
        Invoke("Wander", loading);
    }

    public void Hit() {
        --health;
        if(health <= 0)
            Die();
    }

    private void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            player.HitFrom(transform);
        }
    }
}
