using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        SubtractHealth(amount);

        if (RunOutOfHealth())
        {
            Destroy(gameObject);
        }
    }

    private void SubtractHealth(int amount)
    {
        _currentHealth -= amount;
    }

    private bool RunOutOfHealth()
    {
        return _currentHealth <= 0;
    }
}
