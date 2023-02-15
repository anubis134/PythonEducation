using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGameCotroller : MonoBehaviour
{
    [SerializeField]
    private Transform _cityzenTransform;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private Animator _cityzenAnimator;

    public void StartGameSimylation() 
    {
        StartCoroutine(SetTraficLightsRoutine());
    }

    private IEnumerator SetTraficLightsRoutine() 
    {
        yield return new WaitForSeconds(2F);
        TrafficLightsManager.Instance.EnableLight(0);
        yield return new WaitForSeconds(2F);
        TrafficLightsManager.Instance.EnableLight(1);
        yield return new WaitForSeconds(2F);
        TrafficLightsManager.Instance.EnableLight(2);
        _cityzenAnimator.SetTrigger("Run");
        _cityzenTransform.DOMove(_targetTransform.position, 2F).OnComplete(() => 
        {
            _cityzenAnimator.SetTrigger("Idle");
            GameState.Instance.ShowCompleteScreen();
        });
    }
}
