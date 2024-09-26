using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 10;
    private int currentHealth;

    public ParticleSystem hitParticles;
    public float deathShrinkDuration = 1f;

    public Button increaseHealthButton; 
    public Button decreaseHealthButton;
    public Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();

        
        increaseHealthButton.onClick.AddListener(IncreaseHealth);
        decreaseHealthButton.onClick.AddListener(DecreaseHealth);
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Debug.Log("Character is dead!");
            StartCoroutine(DeathShrink());
        }
    }

    private IEnumerator DeathShrink()
    {
        float timer = 0f;
        Vector3 originalScale = transform.localScale;

        while (timer < deathShrinkDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, timer / deathShrinkDuration);
            yield return null;
        }

        gameObject.SetActive(false);
    }

    
    public void IncreaseHealth()
    {
        maxHealth++;
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    
    public void DecreaseHealth()
    {
        maxHealth = Mathf.Max(1, maxHealth - 1);
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    
    private void UpdateHealthText()
    {
        healthText.text = maxHealth.ToString();
    }
}