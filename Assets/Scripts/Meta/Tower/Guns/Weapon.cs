using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] 
    private TMP_InputField _inputField;
    [SerializeField] 
    private Health _health;

    private void Awake()
    {
        _inputField.onValueChanged.AddListener(Call);
    }

    private void OnDestroy()
    {
        _inputField.onValueChanged.RemoveListener(Call);
    }

    private void Call(string value)
    {
        if(int.Parse(value) > _health.HealthProperty || int.Parse(value) < 0)
        {
            _inputField.text = "0";
        }
    }

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
        int shootCount = int.Parse(_inputField.text);
        
        while (shootCount > 0)
        {
            shootCount--;
            Shoot(_bulletPrefab, _targetTransform, _jumpPower, _shootTime);
            yield return new WaitForSeconds(_shootDelay);
        }
        
        GameState.Instance.ShowCompleteScreen();
    }
}
