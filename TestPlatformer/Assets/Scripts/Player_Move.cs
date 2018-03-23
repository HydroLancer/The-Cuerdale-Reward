using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public int playerSpeed = 10;
    public bool facingRight = true;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;
    public float distanceToBottomOfPlayer = 1.2824f;
	
	// Update is called once per frame
	void Update () {
        playerMove();
        PlayerRaycast();
	}

    void playerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown ("Jump") && isGrounded == true) // Only lets the character jump under condition
        {
            Jump();
        }

        //animations
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", true); //IsRunning is set in Unity's animator - triggers animation for running
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false); 
        }

        //player direction - Changes which way the character faces
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer(); 
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //physics                                                     //moves only in x orientation. y direction stays the same
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y); 
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower); //Adds a temporary force upwards.
        GetComponent<Animator>().SetBool("IsJumping", true);                // Sets character animation to jumping
        isGrounded = false; //Character is not standing on something. Forbids jumping
    }
    void FlipPlayer() //Flips the sprite around when changing direction
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {                               //objects can be tagged. This is the floor.
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "platform")
        {
            isGrounded = true; //lands on floor. allows jumping again.
            GetComponent<Animator>().SetBool("IsJumping", false); //changes sprite to running or idling depending on current input
        }
    } 
    void PlayerRaycast() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if ( hit != null && hit.collider != null && hit.distance < distanceToBottomOfPlayer && hit.collider.tag == "enemy") //landing on top of the enemy basically. 
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 700); //player jumps
            hit.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000); //enemy starts to drop down

            //Makes enemy just drop through the floor << disables their hitboxes and such. 
            hit.collider.GetComponent<BoxCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
        }
    }
}

