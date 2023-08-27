using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private Vector3 lastPosition;

    private void LateUpdate()
    {
        Vector3 newPosition = transform.position + new Vector3(parallaxEffectMultiplier.x, parallaxEffectMultiplier.y);
        transform.position = newPosition;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(newPosition.x - lastPosition.x) >= 1f)
            {
                float offsetPositionX = (newPosition.x - lastPosition.x) % 1f;
                transform.position = new Vector3(newPosition.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(newPosition.y - lastPosition.y) >= 1f)
            {
                float offsetPositionY = (newPosition.y - lastPosition.y) % 1f;
                transform.position = new Vector3(transform.position.x, newPosition.y + offsetPositionY);
            }
        }

        lastPosition = newPosition;
    }
}
