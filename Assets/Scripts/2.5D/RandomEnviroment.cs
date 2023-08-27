using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnviroment : MonoBehaviour
{
    public GameObject[] allSprites, foliage;
    public int copiesToMake, foliageCopies;
    public float minDistance, maxDistance;

    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spr in allSprites)
        {
            SelectSpawnPoint(minDistance);

            Vector3 originalPosition = spr.transform.position;
            spawnPoint.y = originalPosition.y; // Preserve the original Y-axis value

            spr.transform.position = spawnPoint;

            for (int i = 0; i < copiesToMake; i++)
            {
                SelectSpawnPoint(minDistance);

                Vector3 originalPositionCopy = spr.transform.position;
                spawnPoint.y = originalPositionCopy.y; // Preserve the original Y-axis value

                Instantiate(spr, spawnPoint, spr.transform.rotation).transform.parent = transform;
            }
        }

        foreach (GameObject fol in foliage)
        {
            SelectSpawnPoint(0f);

            Vector3 originalPosition = fol.transform.position;
            spawnPoint.y = originalPosition.y; // Preserve the original Y-axis value

            fol.transform.position = spawnPoint;

            for (int i = 0; i < foliageCopies; i++)
            {
                SelectSpawnPoint(0f);

                Vector3 originalPositionCopy = fol.transform.position;
                spawnPoint.y = originalPositionCopy.y; // Preserve the original Y-axis value

                Instantiate(fol, spawnPoint, fol.transform.rotation).transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectSpawnPoint(float minDist)
    {
        spawnPoint.x = Random.Range(-1f, 1f);
        spawnPoint.z = Random.Range(-1f, 1f);

        spawnPoint = Vector3.ClampMagnitude(spawnPoint, 1f) * maxDistance;

        if (spawnPoint.magnitude < minDist)
        {
            SelectSpawnPoint(minDist);
        }
    }

}
