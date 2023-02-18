using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private string damageableTag;

    private Damageable _damageable;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TryGetDamageable(other))
        {
            DealDamage();
        }
    }

    private bool TryGetDamageable(Collider2D other)
    {
        return other.CompareTag(damageableTag) && other.TryGetComponent(out _damageable);
    }

    private void DealDamage()
    {
        _damageable.TakeDamage(damageAmount);
    }
}