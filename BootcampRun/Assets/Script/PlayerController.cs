using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane =1;//0:sol;1:orta;2:sag;
    public float landDistance=2;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;    
    public float Gravity = -20;//rigidbody karakteri asagi cekiyordu anlayamadim boyle bir yol buldum netten
    
    public Animator animator;
    public bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStart)
        {            
            return;
        }
        animator.SetBool("isGameStart",true);
        direction.z=forwardSpeed;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("jumpTime",isGrounded);
        if(isGrounded)
        {
            direction.y = -2;
            if(SwipeManager.swipeUp)
            {
                Jump();
            }

        }
        else
        {           
            direction.y += Gravity * Time.deltaTime;            
        }

        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
                desiredLane=2;
        }
        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if(desiredLane == -1)
                desiredLane=0;
        }
        Vector3 targetPosition = transform.position.z*transform.forward + transform.position.y*transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * landDistance;
        }else if(desiredLane==2)
        {
            targetPosition += Vector3.right * landDistance;
        }
        if(transform.position== targetPosition)
            return;
        Vector3 diff= targetPosition - transform.position;
        Vector3 moveDir = diff.normalized*25*Time.deltaTime;
        if(moveDir.sqrMagnitude<diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    private void FixedUpdate()
    {
        if(!PlayerManager.isGameStart)
            return;
        controller.Move(direction*Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag=="Object")
        {
            PlayerManager.gameOver=true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
        if(hit.transform.tag=="Line")
        {
            PlayerManager.nextLevel=true;
        }

    }
}
