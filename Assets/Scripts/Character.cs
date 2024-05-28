using UnityEngine;

abstract public class Character : MonoBehaviour {
    [SerializeField] private GameObject hpBarPrefab;
    private Canvas canvas;
    protected int currentHealth;
    [SerializeField] protected int maxHealth;
    public float HealthPercentage { get { return (float)currentHealth / maxHealth; }}

    protected virtual void Awake() {
        currentHealth = maxHealth;
        canvas = transform.GetComponentInChildren<Canvas>();
        Instantiate(hpBarPrefab, canvas.transform);
    }

    public virtual void TakeHit(Transform attacker = null) {
        --currentHealth;
        if (currentHealth <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
