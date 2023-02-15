using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : Shooting
{
    [Header("Weapon Settings")]
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    private float _shootTime;
    [SerializeField]
    private float _shootDelay;
    [SerializeField]
    private UnityEvent OnShoot;

    public void StartShooting(TMP_InputField inputField) 
    {
        _bulletPrefab.ShootingCount = int.Parse(inputField.text);
        StartCoroutine(ShootingRoutine());
    }

    internal override void Shoot(Bullet bulletPrefab, Transform targetTransform, float jumpPower, float time)
    {
        base.Shoot(bulletPrefab, targetTransform, jumpPower, time);
        OnShoot?.Invoke();
    }

    private IEnumerator ShootingRoutine() 
    {
        while (AttackManager.Instance.AllowAttack) 
        {
            Shoot(_bulletPrefab, _targetTransform, _jumpPower, _shootTime);
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
