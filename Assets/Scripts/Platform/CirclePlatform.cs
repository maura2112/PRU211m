using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePlatform : MonoBehaviour
{
    public Transform RotationCenter;
    public float AngularSpeed, RotataionRadius;
    private float posX, posY, angle = 0;

    private void Update()
    {
        MovingCircle();
    }

    private void MovingCircle()
    {
        posX = RotationCenter.position.x + Mathf.Cos(angle) * RotataionRadius;
        posY = RotationCenter.position.y + Mathf.Sin(angle) * RotataionRadius;
        transform.position = new Vector2(posX, posY);
        angle += Time.deltaTime * AngularSpeed;
        if (angle >= 360)
        {
            angle = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere (transform.position, RotataionRadius);
    }
}
