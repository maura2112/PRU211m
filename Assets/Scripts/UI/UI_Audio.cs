using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Audio : MonoBehaviour
{
    private bool isOn = true;
    private AudioListener audioListener;

    private void Start()
    {
        audioListener = GetComponent<AudioListener>();
    }
    public void Func_StopUIAnim()
    {
        isOn = !isOn;
        StartCoroutine(Func_PlayAudio());
    }
    IEnumerator Func_PlayAudio()
    {
        yield return new WaitForSeconds(0.1f);
            audioListener.enabled = isOn;        
    }
}
