using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    GameObject[] upgradeObjects;
    public Button damageUpgrade;
    public Button healthUpgrade;
    public Button speedUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        upgradeObjects = GameObject.FindGameObjectsWithTag("Upgrades");
        hideUpgrades();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        characterController characterController = player.GetComponent<characterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController.playerDamage >= 10)
        {
            damageUpgrade.interactable = false;
        }
        if(characterController.maxHealth >= 30)
        {
            healthUpgrade.interactable = false;
        }
        if(characterController.movementSpeed >= 9f)
        {
            speedUpgrade.interactable = false;
        }
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
