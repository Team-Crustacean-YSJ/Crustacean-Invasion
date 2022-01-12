using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    [SerializeField]
    private Text healthText;

    // Start is called before the first frame update
    void Start()
    { 
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = characterController.maxHealth;
        healthBar.value = characterController.curHealth;
        healthText.text = characterController.curHealth.ToString() + "/" + characterController.maxHealth.ToString();
    }

    public void SetHealth(int hp)
    {
        healthBar.maxValue = characterController.maxHealth;
        healthBar.value = hp;
        healthText.text = characterController.curHealth.ToString() + "/" + characterController.maxHealth.ToString();
    }


}
