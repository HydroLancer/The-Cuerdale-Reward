using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Health : MonoBehaviour {

    private string currentScene;
    

    // Use this for initialization
    void Start ()
    {
        currentScene = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -10)
        {
            Die();
        }
	}
    IEnumerator Die()
    {
        SceneManager.LoadScene(currentScene);
        return null;
    }
}
