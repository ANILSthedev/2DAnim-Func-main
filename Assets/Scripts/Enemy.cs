using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI healthText;
    public Image enemyHealthBar;
    public Animator controller;

    void Start()
    {
        healthText.text = "Enemy HP: " + health.ToString();
        enemyHealthBar.fillAmount = (float)health / 100;
    }
}
