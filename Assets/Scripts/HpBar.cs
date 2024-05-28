using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {
    private Slider slider;
    [SerializeField] private float heightGap;
    private Character character;

    private void Awake() {
        slider = GetComponent<Slider>();
        character = transform.GetComponentInParent<Character>();
    }

    private void Update() {
        if(slider.value <= 0f) {
            gameObject.SetActive(false);
        }
        FollowPlayer();
        KeepHpValue();
    }
    
    private void FollowPlayer() {
        Vector3 pos = Camera.main.WorldToScreenPoint(character.transform.position);
        pos.y += heightGap;
        transform.position = pos;
    }

    private void KeepHpValue() {
        slider.value = Mathf.Lerp(slider.value, character.HealthPercentage, Time.deltaTime * 10);
    }
}
