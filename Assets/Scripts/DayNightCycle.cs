using System;
using UnityEngine;

public class DayNightCycle: MonoBehaviour
{
    [SerializeField] private float oneCycleDuration = 120f;
        
    private Material skyboxMat;
    
    private void Start()
    {
        skyboxMat = RenderSettings.skybox;
    }

    private void Update()
    {
        float cycleProgress = (Time.time % oneCycleDuration) / oneCycleDuration;
        float transition = Mathf.PingPong(cycleProgress * 2f, 1f);
        skyboxMat.SetFloat("_CubemapTransition", transition);
        DynamicGI.UpdateEnvironment();
    }
}
