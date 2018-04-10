using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {
    public int EnemySpeed;
    public int xMoveDirection;
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * EnemySpeed;
        if (hit.distance < 0.9f)
        {
            if (hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
                Flip();
            }
            if (hit.collider.tag == "Tree")
            {
                Flip();
            }
            if (hit.collider.tag == "platform")
            {
                Flip();
            }
        }
	}
    void Flip()
    {
        if (xMoveDirection > 0)
        {
            xMoveDirection = -1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else
        {
            xMoveDirection = 1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
