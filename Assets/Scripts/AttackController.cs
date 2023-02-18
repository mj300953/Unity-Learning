using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    public Animator _animator;
    
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackHash);
    }
}
