using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamagable
{
    internal float HealthProperty { get; private set; } = 1F;
    internal Action<float> OnHealthWaschanged;
    [SerializeField]
    private UnityEvent _onDestruct;

    public void TakeDamage(float damage)
    {
        HealthProperty -= (1F/damage) + 0.001F;

        if (HealthProperty <= 0F) 
        {
            AttackManager.Instance.AllowAttack = false;
            HealthProperty = 0F;
            GameState.Instance.ShowCompleteScreen();
            _onDestruct?.Invoke();
        }
      
        OnHealthWaschanged?.Invoke(HealthProperty);
    }
}
