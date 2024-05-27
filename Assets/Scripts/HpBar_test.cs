using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
 
public class HpBar_test : MonoBehaviour
{
    public Slider prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    [SerializeField] private float height = 1.2f;
    [SerializeField] private Enemy enemy;
    
    public void MakeHpBar()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
    }

}
