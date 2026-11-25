using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HeroPlayer : MonoBehaviour
{
    public Animator animController;
    public int heroHealth = 100;
    public TextMeshProUGUI healthText;
    public Image p1HealthBar;

    public Enemy enemy;
    public UIManager uimanager;

    public GameObject hitEffect1;
    public GameObject hitEffect2;

    void Start()
    {
        healthText.text = "Player 1 HP: " + heroHealth.ToString();
        p1HealthBar.fillAmount = (float)heroHealth / 100;
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Attack1();
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Attack2();
        }
    }

    public void Attack1()
    {
        if (heroHealth <= 0 || enemy.health <= 0)
        {
            return;
        }

        animController.SetTrigger("Attack");
        hitEffect1.SetActive(false);
        hitEffect1.SetActive(true);

        enemy.health = enemy.health - 10;
        if (enemy.health < 0)
        {
            enemy.health = 0;
        }

        enemy.healthText.text = "Enemy HP: " + enemy.health.ToString();
        enemy.enemyHealthBar.fillAmount = (float)enemy.health / 100;

        if (enemy.health <= 0)
        {
            enemy.controller.SetBool("Death", true);
            GameOverWin();
        }
        else
        {
            enemy.controller.SetTrigger("Hurt");
            Invoke("GetHurt", 0.8f);
        }
    }

    public void Attack2()
    {
        if (heroHealth <= 0 || enemy.health <= 0)
        {
            return;
        }

        animController.SetTrigger("Attack");
        hitEffect2.SetActive(false);
        hitEffect2.SetActive(true);

        enemy.health = enemy.health - 20;
        if (enemy.health < 0)
        {
            enemy.health = 0;
        }

        enemy.healthText.text = "Enemy HP: " + enemy.health.ToString();
        enemy.enemyHealthBar.fillAmount = (float)enemy.health / 100;

        if (enemy.health <= 0)
        {
            enemy.controller.SetBool("Death", true);
            GameOverWin();
        }
        else
        {
            enemy.controller.SetTrigger("Hurt");
            Invoke("GetHurt", 0.8f);
        }
    }

    public void GetHurt()
    {
        if (enemy.health <= 0 || heroHealth <= 0)
        {
            return;
        }

        enemy.controller.SetTrigger("Attack");
        int randomDamage = Random.Range(12, 26);
        heroHealth = heroHealth - randomDamage;

        if (heroHealth <= 0)
        {
            heroHealth = 0;
            healthText.text = "Player 1 HP: 0";
            p1HealthBar.fillAmount = 0;
            PlayerDeath();
        }
        else
        {
            animController.SetTrigger("Hurt");
            healthText.text = "Player 1 HP: " + heroHealth.ToString();
            p1HealthBar.fillAmount = (float)heroHealth / 100;
        }
    }

    public void PlayerDeath()
    {
        animController.SetBool("Death", true);
        uimanager.inGamePanel.SetActive(false);
        uimanager.gameOverPanel.SetActive(true);
        uimanager.gameOverText.text = "Game Over You Lose!";
    }

    public void GameOverWin()
    {
        uimanager.inGamePanel.SetActive(false);
        uimanager.gameOverPanel.SetActive(true);
        uimanager.gameOverText.text = "Game Over You Win";
    }
}
