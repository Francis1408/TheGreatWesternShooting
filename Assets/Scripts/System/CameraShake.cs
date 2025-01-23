using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    
    public static CameraShake Instance { get; private set; }
    
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startIntensity;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple Camera instances detected! Destroying extra instance.");
            Destroy(gameObject);
        }
        
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Method to shake main camera when is called based on a intensity and time
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity; // Applies intensity
        startIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time; // Starts the shakingtime
    }

    private void Update()
    {
        if (shakeTimer > 0) // Counts down the shaking time
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f) // Time is over
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
                // Smooth stabilization of the camera

            }
        } 
    }
}
