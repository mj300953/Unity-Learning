using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float ShotPower;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

    public void Shot()
    {
        _rigidbody.AddForce(ShotPower * Vector2.up);
    }
     
}
