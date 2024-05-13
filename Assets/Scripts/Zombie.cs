using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float loading = 1;
    Rigidbody2D rigid;
    public int nextMoveX, nextMoveY;
    public float playerRangeDistance, speed;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        Invoke("Think", loading);
    }


    void FixedUpdate()
    {   
        if(Vector2.Distance(transform.position, target.position) > playerRangeDistance) {
            rigid.velocity = new Vector2(nextMoveX, nextMoveY);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void Think()
    {
        nextMoveX = Random.Range(-1, 2);
        nextMoveY = Random.Range(-1, 2);

        Invoke("Think", loading);
    }
}
