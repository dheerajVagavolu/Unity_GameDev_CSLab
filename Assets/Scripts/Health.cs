using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float StartHealth = 5f;

    public float currentHealth;

    public PlayerController thePlayer;

    public float InvincibilityL;

    private float InvincibilityC;

    public Renderer playerRenderer;

    public float flashL = 0.1f;

    private float flashC;

    public Image healthBar;

    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = StartHealth;

        thePlayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / StartHealth;
        if (InvincibilityC > 0)
        {
            InvincibilityC -= Time.deltaTime;

            flashC -= Time.deltaTime;

            if(flashC <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashC = flashL;
            }
            if (InvincibilityC <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    public void TakeDamage(int damageAmount, Vector3 direction)
    {

        if (InvincibilityC <= 0)
        {
            

            currentHealth -= damageAmount;

            thePlayer.knockBack(direction);

            InvincibilityC = InvincibilityL;

            playerRenderer.enabled = false;

            flashC = flashL;

            if (currentHealth <= 0)
            {
                FindObjectOfType<GameManager>().Die();
            }
        }
    }

    public void Heal()
    {
        currentHealth = StartHealth;


    }

/*    private void Die()
    {
        gameObject.SetActive(false);
    }*/


}
