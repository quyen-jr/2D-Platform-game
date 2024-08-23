using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Vector2 moveInput;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 22f;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityAtStart;
    [SerializeField] float climSpeed = 3f;
    bool isAlive = true;
    [SerializeField] Vector2 deadKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    // Start is called before the first frame update
    void Start()
    {
     //   Application.targetFrameRate = 60;
        myRigidBody = GetComponent<Rigidbody2D>();
        gravityAtStart = myRigidBody.gravityScale;
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimLadder();
        Dead();
    }
    void OnFire()
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
       // Debug.Log(myRigidBody.velocity.y);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void ClimLadder()
    {
      //  Debug.Log(1);
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityAtStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climSpeed);
       // Debug.Log(climbVelocity.y);
        myRigidBody.gravityScale = 0;
        myRigidBody.velocity = climbVelocity;
        bool playerVerticalSpeed = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerVerticalSpeed);
   
       // myAnimator.SetBool("isClimbing")
    }
    void Dead()
    {
        if (!myRigidBody.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazard")))
        {
            return;
        }
        myAnimator.SetTrigger("dying");
        isAlive = false;
        myRigidBody.velocity = deadKick;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
