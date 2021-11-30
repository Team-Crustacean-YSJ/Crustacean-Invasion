using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = characterController.maxHealth;
        healthBar.value = characterController.curHealth;

    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
