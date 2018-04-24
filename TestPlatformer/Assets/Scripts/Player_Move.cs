using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : MonoBehaviour
{
    const int playerSpeed = 10;
    bool facingRight = false;
    const int kPlayerJumpPower = 1250;
    float moveX;
    bool isGrounded;
    bool collisionTop = false;
    bool collisionBottom = false;
    bool collisionLeft = false;
    bool collisionRight = false;

    void Update()
    {
        playerMove();
        //PlayerRaycast();
    }

    void playerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true) // Only lets the character jump under condition
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
        //physics                   
        if ((moveX < 0.0f && collisionRight == true) || (moveX > 0.0f && collisionLeft == true))
        {
            moveX = 0.0f;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * kPlayerJumpPower); //Adds a temporary force upwards.
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
    bool ClimbLedge(Collision2D col, float yMax, Vector3 contactPoint)
    {
        if ((GetComponent<Renderer>().bounds.min.y > (yMax - 0.3f))) //feet close to level
        {
            ///contactPoint.y
                //GetComponent<Renderer>().bounds.min.y
            if ((GetComponent<Renderer>().bounds.min.y < yMax) && (col.gameObject.tag == "ground" || col.gameObject.tag == "platform" || col.gameObject.tag == "Untagged"))
            {
                collisionTop = true;
                this.transform.Translate(0.0f, yMax - contactPoint.y, 0.0f);
                return true;
            }

            if (col.gameObject.tag == "enemy") collisionTop = true;
        }
        return false;
    }
    void DetectCollision(Collision2D col)
    {

        collisionTop = false;
        collisionBottom = false;
        collisionLeft = false;
        collisionRight = false;



        float yMin = col.collider.bounds.min.y;
        float yMax = col.collider.bounds.max.y;
        float xMin = col.collider.bounds.min.x;
        float xMax = col.collider.bounds.max.x;


        for (int i = 0; i < col.contacts.Length; i++)
        {
            Vector3 contactPoint = col.contacts[i].point;
            Vector3 center = col.collider.bounds.center;

            if (contactPoint.y > yMax)
            {
                collisionTop = true;
            }
            else if (contactPoint.y < yMin)
            {
                collisionBottom = true;
            }
            else if (contactPoint.x > xMax)
            {
                if (!ClimbLedge(col, yMax, contactPoint)) collisionRight = true;
            }
            else if (contactPoint.x < xMin)
            {
                if (!ClimbLedge(col, yMax, contactPoint)) collisionLeft = true;
            }
            else//for angled blocks, it assumes they are on top
            {
                collisionTop = true;
            }

            if (collisionTop == true)
            {
                if (col.gameObject.tag == "ground" || col.gameObject.tag == "platform" || col.gameObject.tag == "Untagged")
                {
                    isGrounded = true; //lands on floor. allows jumping again.
                    GetComponent<Animator>().SetBool("IsJumping", false); //changes sprite to running or idling depending on current input
                }
                else if (col.gameObject.tag == "enemy")
                {
                    collisionTop = false;
                    collisionBottom = false;
                    collisionLeft = false;
                    collisionRight = false;
                    isGrounded = false;
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 700);                                     //player jumps
                    col.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000); //enemy starts to drop down

                    //Makes enemy just drop through the floor << disables their hitboxes and such. 
                    col.collider.GetComponent<BoxCollider2D>().enabled = false;
                    col.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
                }
            }

        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        DetectCollision(col);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        DetectCollision(col);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        DetectCollision(col);
    }
}

