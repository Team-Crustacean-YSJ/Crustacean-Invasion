using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    GameObject[] upgradeObjects;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        upgradeObjects = GameObject.FindGameObjectsWithTag("Upgrades");
        hideUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //shows objects with Upgrade tag
    public void showUpgrades()
    {
        Time.timeScale = 0;
        foreach (GameObject g in upgradeObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with Upgrade tag
    public void hideUpgrades()
    {
        Time.timeScale = 1;
        foreach (GameObject g in upgradeObjects)
        {
            g.SetActive(false);
        }
    }
}
