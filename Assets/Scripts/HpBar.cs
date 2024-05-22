using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {
    [SerializeField] private Slider hpBar;
    [SerializeField] private float heightGap;

    private void Update() {
        if(hpBar.value <= 0.2f) {
            gameObject.SetActive(false);
        }
        FollowPlayer();
    }
    
    private void FollowPlayer() {
        transform.position = GameManager.Instance.Player.transform.position + Vector3.up * heightGap;
    }
}
