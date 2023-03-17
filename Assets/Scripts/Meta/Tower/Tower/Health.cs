using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, IDamagable
{
    internal int HealthProperty { get; private set; } = 0;
    internal Action<int> OnHealthWaschanged;
    [SerializeField]
    private UnityEvent _onDestruct;


    private void Awake()
    {
        SetRandomHealth();
    }

    public void TakeDamage(float damage)
    {
        HealthProperty -= 1;

        if (HealthProperty <= 0F) 
        {
            AttackManager.Instance.AllowAttack = false;
            HealthProperty = 0;
            GameState.Instance.ShowCompleteScreen();
            _onDestruct?.Invoke();
        }
      
        OnHealthWaschanged?.Invoke(HealthProperty);
    }

    private void SetRandomHealth()
    {
        HealthProperty = Random.Range(2, 6);
        OnHealthWaschanged?.Invoke(HealthProperty);
    }
}
