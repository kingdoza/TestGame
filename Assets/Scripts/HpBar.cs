using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
public class HpBar : MonoBehaviour {
    [SerializeField] private Slider hpBar;
    [SerializeField] private float heightGap;

    private void Update() {
        if(hpBar.value <= 0.2f) {
=======
public class HpBar : MonoBehaviour
{
    [SerializeField] Player player;


    void Update()
    {
        if(player.CurHp <= 0) {
>>>>>>> 7d7bef7305303eb1b20713c5c683692462817656
            gameObject.SetActive(false);
        }
        FollowPlayer();
    }
    
    private void FollowPlayer() {
        transform.position = GameManager.Instance.Player.transform.position + Vector3.up * heightGap;
    }
}
