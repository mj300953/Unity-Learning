using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab; // w prefabie dajemy cos co chcemy uzyc wiecej razy, dajemy prefab zeby zakomunikowac ze bedziemy to spawnowac
    [SerializeField] private Transform shotPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity); // identity to podstawowe zerowe wspolrzedne wektora
            bullet.Shot();
        }
    }
}

