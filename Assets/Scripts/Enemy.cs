using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{ 
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GotHitByAttack(collision))
        {
            DamageSelf();
        }
    }

    private bool GotHitByAttack(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Attack");
    }

    private void DamageSelf()
    {
        _health.TakeDamage(10);
    }
}
