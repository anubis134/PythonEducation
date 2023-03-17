using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityGameCotroller : MonoBehaviour
{
    [SerializeField]
    private Transform _cityzenTransform;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private Transform _carTransform;
    [SerializeField]
    private Transform _targetCarTransform;
    [SerializeField]
    private Transform _targetDeathTransform;
    [SerializeField]
    private Animator _cityzenAnimator;

    [SerializeField] 
    private TMP_Dropdown _dropdownFirst;

    [SerializeField] 
    private TMP_Dropdown _dropdownSecond;
    
    [SerializeField] 
    private TMP_Dropdown _dropdownThird;

    private Dictionary<TMP_Dropdown, int> _cachedValues = new ();

    private TMP_Dropdown _lastChanged;

    private void Awake()
    {
        _dropdownFirst.value = 2;
        _dropdownSecond.value = 0;
        _dropdownThird.value = 1;
        
        _dropdownFirst.onValueChanged.AddListener((value) => Call(_dropdownFirst, value));
        _dropdownSecond.onValueChanged.AddListener((value) => Call(_dropdownSecond, value));
        _dropdownThird.onValueChanged.AddListener((value) => Call(_dropdownThird, value));
        
        _cachedValues.Add(_dropdownFirst, _dropdownFirst.value);
        _cachedValues.Add(_dropdownSecond, _dropdownSecond.value);
        _cachedValues.Add(_dropdownThird, _dropdownThird.value);
    }

    private void Call(TMP_Dropdown tmpDropdown, int value)
    {
        if (_lastChanged == tmpDropdown)
        {
            _lastChanged = null;
            
            return;
        }

        foreach (var item in _cachedValues.Keys)
        {
            if (_cachedValues[item] == value)
            {
                _cachedValues[item] = _cachedValues[tmpDropdown];

                _lastChanged = item;
                
                item.value = _cachedValues[tmpDropdown];
                
                _cachedValues[tmpDropdown] = value;
                
                return;
            }
        }
    }


    private void OnDestroy()
    {
        _dropdownFirst.onValueChanged.RemoveListener((value) => Call(_dropdownFirst, value));
        _dropdownSecond.onValueChanged.RemoveListener((value) => Call(_dropdownSecond, value));
        _dropdownThird.onValueChanged.RemoveListener((value) => Call(_dropdownThird, value));
    }

    public void StartGameSimulation() 
    {
        StartCoroutine(SetTraficLightsRoutine());

        _dropdownFirst.interactable = false;
        _dropdownSecond.interactable = false;
        _dropdownThird.interactable = false;
    }

    private IEnumerator SetTraficLightsRoutine()
    {
        yield return new WaitForSeconds(2F);
        Check(0);
        yield return new WaitForSeconds(2F);
        Check(1);
        yield return new WaitForSeconds(2F);
        Check(2);
    }

    private void Check(int value)
    {
        TrafficLightsManager.Instance.EnableLight(value);
        
        if (_dropdownThird.value != value) return;

        bool death = value is 0 or 1;
        
        StopCoroutine(SetTraficLightsRoutine());
        
        Run(death);
    }

    private void Run(bool isDeath = false)
    {
        if (!isDeath)
        {
            _cityzenAnimator.SetTrigger("Run");
            _cityzenTransform.DOMove(_targetTransform.position, 2F).OnComplete(() =>
            {
                _cityzenAnimator.SetTrigger("Idle");
                GameState.Instance.ShowCompleteScreen();
            });
        }
        else
        {
            _cityzenAnimator.SetTrigger("Run");
            _carTransform.DOMove(_targetCarTransform.position, 2F);
            _cityzenTransform.DOMove(_targetDeathTransform.position, 2F).OnComplete(() =>
            {
                _cityzenAnimator.SetTrigger("Idle");
                _cityzenTransform.DORotate(new Vector3(0F, 0F, 90F), 0.5F);
                _cityzenTransform.DOMove(_cityzenTransform.position + Vector3.up * 3F + Vector3.forward * 10F, 0.3F);
                GameState.Instance.ShowCompleteScreen();
            });
        }
    }
}
