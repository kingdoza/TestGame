using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireDelay;
    private float remainedFireDelay = 0;

    private void Update() {
        if(remainedFireDelay > 0) 
            remainedFireDelay -= Time.deltaTime;
    }
    
    public void PullTrigger(Enemy _target) {
        if(_target == null)
            return;
        if(remainedFireDelay > 0.01f)
            return;
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Fire(_target);
        remainedFireDelay = fireDelay;
    }
}
