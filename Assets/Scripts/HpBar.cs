using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {
    [SerializeField] private Slider hpBar;
    [SerializeField] private float heightGap;
    [SerializeField] private Player player;

    private void Update() {
        if(player.curHp <= 0f) {
            gameObject.SetActive(false);
        }
        FollowPlayer();
    }
    
    private void FollowPlayer() {
        transform.position = GameManager.Instance.Player.transform.position + Vector3.up * heightGap;
    }
}
