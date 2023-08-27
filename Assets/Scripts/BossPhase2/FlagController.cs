using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    private float endingBorder;

    void Start()
    {
        endingBorder = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            if (targetObject.transform.position.x < endingBorder)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
