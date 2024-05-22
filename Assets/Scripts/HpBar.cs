using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Player player;


    void Update()
    {
        if(player.CurHp <= 0) {
            gameObject.SetActive(false);
        }
    }
    
}
