using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private GameObject fireSoundPrefab;

    public void Fire(Enemy _target) {
        SetRotationTo(_target);
        StartCoroutine(KeepFlyingTo(_target));
        GameObject fireSoundObject = Instantiate(fireSoundPrefab, transform.position, Quaternion.identity);
        Destroy(fireSoundObject, fireSoundObject.GetComponent<AudioSource>().clip.length);
    }

    private void SetRotationTo(Enemy _target) {
        float xGap = _target.transform.position.x - transform.position.x;
        float yGap = _target.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(yGap,xGap) * 180 / Mathf.PI + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private IEnumerator KeepFlyingTo(Enemy _target) {
        while(Vector3.Distance(transform.position, _target.transform.position) > 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed * Time.deltaTime);
            yield return null;
        }
        Strike(_target);
    }

    private void Strike(Enemy _target) {
        if(_target != null) {
            _target.TakeHit();  //Enemy 코드에서 처리
            Destroy(gameObject);
        }
    }
}
