using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThemeChange : MonoBehaviour
{

    public AudioSource sfx1;
    public AudioSource sfx2;
    public AudioSource sfx3;
    public AudioSource sfx4;

    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    public GameObject bg4;

    private int valueZ;

    private void Start()
    {
        
    }
    private void Update()
    {
        
        //Debug.Log("eulerAngles x, y, z: "   + transform.eulerAngles.x + " " 
        //                                    + transform.eulerAngles.y +" "
        //                                    + transform.eulerAngles.z);
        ChangeSFX();
    }
    private void ChangeSFX()
    {   

        if (Mathf.Approximately(transform.eulerAngles.z, 180))
        {
            bg4.SetActive(true);
            sfx4.gameObject.SetActive(true);
        }
        else
        {
            bg4.SetActive(false);
            sfx4.gameObject.SetActive(false);
        }
        if (Mathf.Approximately(transform.eulerAngles.z, 270))
        {
            bg3.SetActive(true);
            sfx3.gameObject.SetActive(true);
        }
        else
        {
            bg3.SetActive(false);
            sfx3.gameObject.SetActive(false);
        }
        if (Mathf.Approximately(transform.eulerAngles.z, -0.00001001791f))
        {
            bg2.SetActive(true);
            sfx2.gameObject.SetActive(true);
        }
        else
        {
            bg2.SetActive(false);
            sfx2.gameObject.SetActive(false);
        }
        if (Mathf.Approximately(transform.eulerAngles.z, 90))
        {
            bg1.SetActive(true);
            sfx1.gameObject.SetActive(true);
        }
        else
        {
            bg1.SetActive(false);
            sfx1.gameObject.SetActive(false);
        }
    }
}
