using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    public GameObject ScoreUI;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        ScoreUI.gameObject.GetComponent<Text>().text = ("Total Score: " + currentPlayThrough.GetScore());


    }
}