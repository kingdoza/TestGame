using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;


    void Update()
    {
        if(hpBar.value <= 0.2f) {
            gameObject.SetActive(false);
        }
    }
    
}
