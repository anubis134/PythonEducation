using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Range(0.2F, 100F)]
    private float _weight = .2F;
    [SerializeField]
    internal float ShootingCount;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(ShootingCount);
            Destroy(this.gameObject);
        }
    }
}
