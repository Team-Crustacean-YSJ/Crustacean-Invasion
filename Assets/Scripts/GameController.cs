using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] finishObjects;

    public characterController playerAlive;

    [SerializeField]
    private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        finishObjects = FindGameObjectsWithTags(new string[] { "ShowOnFinish", "ExitButton" }); //So I can have the exit button with it's own tag
        
        playerAlive = GameObject.FindGameObjectWithTag("Player").GetComponent<characterController>();
        hidePaused();
        hideFinished();
        updateScore(characterController.score);
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

    public void updateScore(int playerScore)
    {
        scoreText.text = "Coins: " + playerScore.ToString();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    GameObject[] FindGameObjectsWithTags(params string[] tags)
    {
        var all = new List<GameObject>();

        foreach (string tag in tags)
        {
            var temp = GameObject.FindGameObjectsWithTag(tag).ToList();
            all = all.Concat(temp).ToList();
        }

        return all.ToArray();
    }
}
