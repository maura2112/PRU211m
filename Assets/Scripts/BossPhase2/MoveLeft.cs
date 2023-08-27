using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField]private PlayerManager player;
    public float m_Speed = 5f;

    [SerializeField]private float leftBorder = -40f;

    private bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<PlayerManager>();
        isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop when the game is over
        if (!player.gameOver && !isStop)
        {
            transform.Translate(Vector2.left * m_Speed * Time.deltaTime);
        }

        // Destroy the object if it goes off bound
        if (transform.position.x < leftBorder)
        {
            Destroy(gameObject);
        }

    }

    public void setStop()
    {
        isStop = true;
    }
}
