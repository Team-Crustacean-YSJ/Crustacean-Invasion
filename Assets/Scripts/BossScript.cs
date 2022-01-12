using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public Slider healthBar;
    public GameObject boss;
    int maxHealth = 40;
    enemyController eC;

    GameObject[] victoryScreen;

    [SerializeField]
    private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        eC = boss.GetComponent<enemyController>();

        victoryScreen = FindGameObjectsWithTags(new string[] {"VictoryScreen", "ExitButton" });

        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = eC.health;
        healthText.text = eC.health.ToString() + "/" + maxHealth.ToString();
        HideEOG();
    }

    public void SetHealth(int hp)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = hp;
        healthText.text = eC.health.ToString() + "/" + maxHealth.ToString();
    }

    public void HideEOG()
    {
        foreach (GameObject g in victoryScreen)
        {
            g.SetActive(false);
        }
    }

    public void ShowEOG()
    {
        foreach (GameObject g in victoryScreen)
        {
            g.SetActive(true);
        }
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
