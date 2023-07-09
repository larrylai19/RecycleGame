using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public static int health;
    public static int healthMax = 5;

    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)health / (float)healthMax;
        healthText.text = health.ToString();
    }
}
