using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 65f;
    public float maxZoom = 30f;
    public float zoomLimiter = 11f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();   
    }
    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }
        Move();
        Zoom();
    }

    void Zoom()
    {
        Debug.Log((GetGreatestDistanceX() + GetGreatestDistanceY()) /2);
        Debug.Log("Height: "+GetGreatestDistanceY());

        float newZoom = Mathf.Lerp(maxZoom, minZoom, ((GetGreatestDistanceX() + GetGreatestDistanceY()) / 2) / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistanceX()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    float GetGreatestDistanceY()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.y;
    }

    Vector3 GetCenterPoint()
    {
        
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        if (((GetGreatestDistanceX() + GetGreatestDistanceY()) / 2) > 13f || GetGreatestDistanceY() >10f)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i  = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
