﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleInputs : MonoBehaviour {

    private string currentScene;

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
        {
            if (currentScene == "Title")
            {
                SceneManager.LoadScene("Instructions");
            }
            if (currentScene == "Instructions")
            {
                SceneManager.LoadScene("Level 1");
            }
            if (currentScene == "Info")
            {
                SceneManager.LoadScene("Level 2");
            }
            if (currentScene == "Credits")
            {
                SceneManager.LoadScene("Title");
            }
            if (currentScene == "GameOver")
            {
                currentPlayThrough.Reset();
                SceneManager.LoadScene("Credits");
            }
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
	}
}
