using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    public characterController playerAlive;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");			
        playerAlive = GameObject.FindGameObjectWithTag("Player").GetComponent<characterController>();
        hidePaused();
        hideFinished();

    }

    // Update is called once per frame
    void Update()
    {
        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                pause();
            }
            else if (Time.timeScale == 0)
            {
                unpause();
            }
        }

        if (playerAlive.isDead)
        {
            Time.timeScale = 0;
            showFinished();
        }
    }
    
    public void pause()
    {
        Time.timeScale = 0;
        showPaused();
    }

    public void unpause()
    {
        Time.timeScale = 1;
        hidePaused();
    }
    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnFinish tag
    public void showFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnFinish tag
    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }
}
