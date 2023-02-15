using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : MonoBehaviour
{
    internal static TrafficLightsManager Instance;
    [SerializeField]
    private List<Light> _lights = new List<Light>();

    private void Awake()
    {
        Instance = this;
    }

    internal void EnableLight(int lightIndex) 
    {
        DisableAllLights();
        _lights[lightIndex].enabled = true;
    }

    internal void DisableAllLights() 
    {
        foreach (var light in _lights) 
        {
            light.enabled = false;
        }
    }
}
