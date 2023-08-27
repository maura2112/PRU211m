using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animation : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .075f;

    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;



    private void Start()
    {
        IsDone = false;
        PlayAnim();
    }
    public void Func_StopUIAnim()
    {
        IsDone = !IsDone;
        PlayAnim();
    }
    private void PlayAnim()
    {
        StartCoroutine(Func_PlayAnimUI());
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
}
