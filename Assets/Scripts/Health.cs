using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Enemy,
    Player
}

public class Health : MonoBehaviour
{
    [SerializeField] private CharacterType type;
    [SerializeField] private int maxHealth;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if(_currentHealth <= 0 && type == CharacterType.Enemy)
        {
            Destroy(gameObject);
        }

        if (_currentHealth <= 0 && type == CharacterType.Player)
        {
            // Game Over
        }
    }
}
