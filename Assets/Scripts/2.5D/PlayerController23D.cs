using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController23D : MonoBehaviour
{
    public Animator anim;

    public float moveSpeed, jumpForce = 15f, rotateSpeed = 25f, gravityScale = 5f;
    //public Rigidbody theRB;
    public CharacterController charController;

    private bool isFP;

    public Camera theCam;

    private Vector3 moveDirection;

    public Transform model;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFP)
        {
            //inputVelocity = (Input.GetAxis("Horizontal") * thirdPersonCam.transform.right * moveSpeed) + (Input.GetAxis("Vertical") * thirdPersonCam.transform.forward * moveSpeed);

            //theRB.velocity = new Vector3(inputVelocity.x, theRB.velocity.y, inputVelocity.z);

            //model.rotation = Quaternion.Euler(0f, thirdPersonCam.rotation.eulerAngles.y, 0f);

            float yStore = moveDirection.y;
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    anim.Play("jump-Animation");
                    moveDirection.y = jumpForce;
                    Debug.Log("Jump");
                    
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //playerModel.transform.rotation = newRotation;

                model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        anim.SetFloat("Speed", new Vector3(moveDirection.x, 0f, moveDirection.z).magnitude);
        anim.SetBool("Idle", charController.isGrounded);
    }
}
