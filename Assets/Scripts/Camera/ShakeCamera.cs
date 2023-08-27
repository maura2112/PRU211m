using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance { get; private set; }
    private CinemachineVirtualCamera _camera;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBMCP
            = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBMCP.m_AmplitudeGain = intensity;
        shakeTimer = time;

    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                //Done shake camera
                CinemachineBasicMultiChannelPerlin cinemachineBMCP
            = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBMCP.m_AmplitudeGain = 0f;

            }
        }

    }
}
