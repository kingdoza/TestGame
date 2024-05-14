using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;
    }
}
