using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    float shakeTimer;
    float shakeTimerTotal;
    float startingIntensity;
    private void Awake()
    {
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float shakeTime)
    {
        StartCoroutine(ShakeCR(shakeTime));
    }

    private void FixedUpdate()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));

            }
        }
    }

    IEnumerator ShakeCR(float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        

        
        

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = timer;
            yield return new WaitForFixedUpdate();
            
            
        }
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;


    }
}
