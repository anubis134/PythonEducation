using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public sealed class HealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private Image[] _progressBarImages;
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

    private void UpdateHealthBar(int newValue)
    {
        ClearAllHeath();
        
        for (int i = 0; i < newValue; i++)
        {
            _progressBarImages[i].enabled = true;
        }
    }

    private void ClearAllHeath()
    {
        foreach (var heathImage in _progressBarImages)
        {
            heathImage.enabled = false;
        }
    }
}
