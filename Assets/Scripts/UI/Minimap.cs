using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
        
        //transform.rotation = Quaternion.identity;
    }
}
