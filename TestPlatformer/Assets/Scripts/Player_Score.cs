using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour {
    private float timeLeft = 120;
    public int playerScore = 0;
    public int xMoveDirection;
    public int MoveSpeed = 2;

    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
	
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int) timeLeft);

        if (timeLeft < 0.01f)
        {
            SceneManager.LoadScene("Level 1");
        }
	}

    void OnTriggerEnter2D (Collider2D trig)
    {
        if (trig.gameObject.tag == "endlevel1")
        {
            CountScore();
            SceneManager.LoadScene("heck");

        }
        if (trig.gameObject.tag == "collectible")
        {
            playerScore += 10;
            Destroy(trig.gameObject);
        }
        
        
    }
    void CountScore()
    {
        playerScore = playerScore + (int)( timeLeft * 10);
        
    }
}
