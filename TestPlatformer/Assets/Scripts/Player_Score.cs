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

    private string currentScene;

    public GameObject timeLeftUI;
	
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;

        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int) timeLeft + "\nScore: " + playerScore + "\nLives: " + currentPlayThrough.GetLives());


        if (timeLeft < 0.01f)
        {
            SceneManager.LoadScene(currentScene);
        }
	}

    void OnTriggerEnter2D (Collider2D trig)
    {
        if (trig.gameObject.tag == "endlevel1")
        {
            CountScore();
            currentPlayThrough.IncrementScore(playerScore);
            SceneManager.LoadScene("Info");

        }
        if (trig.gameObject.tag == "endlevel2")
        {
            CountScore();
<<<<<<< HEAD
            currentPlayThrough.IncrementScore(playerScore);
=======
>>>>>>> a4d0ffb5cae330f265491eb0692a7f88184db500
            SceneManager.LoadScene("GameOver");

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
