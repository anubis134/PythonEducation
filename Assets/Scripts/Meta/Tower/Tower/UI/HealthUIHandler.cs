using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public sealed class HealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private Image _progressBarImage;
    private Health _bufferedHealth;

    private void Awake()
    {
        _bufferedHealth = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _bufferedHealth.OnHealthWaschanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _bufferedHealth.OnHealthWaschanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float newValue) 
    {
        _progressBarImage.fillAmount = newValue;
    }
}
