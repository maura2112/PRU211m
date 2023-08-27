using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	Rigidbody2D rb;
	Vector2 initialPosition;
	bool platformMovingBack;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		initialPosition = transform.position;
		audio = gameObject.GetComponent<AudioSource> ();
	}
	void Update()
	{
		FallingBrick();
	}
	private void FallingBrick()
	{
        if (platformMovingBack)
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);

        if (transform.position.y == initialPosition.y)
            platformMovingBack = false;
    }
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Player") && !platformMovingBack) {
			audio.Play ();
            ShakeCamera.Instance.Shake(1f, 0.5f);
            Invoke ("DropPlatform", 0.115f);
		}
	}
	void DropPlatform()
	{
		rb.isKinematic = false;
		Invoke ("GetPlatformBack", 1.5f);
	}
	void GetPlatformBack()
	{
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		platformMovingBack = true;
	}
}
