using UnityEngine;
using UnityEngine.UI;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;

    public float maxHealth;

    public GameObject healthBar;

    public GameObject[] loot;

    public Slider healthBarSlider;

    public int xpPerKill;

    private Room _room;

    private void Start()
    {
        health = maxHealth;
        _room = GetComponentInParent<Room>();
    }

    private void CheckDeath()
    {
        if (!(health <= 0)) return;
        try
        {
            _room.enemies.Remove(gameObject);
        }
        catch
        {
            // ignored
        }

        Destroy(gameObject);

        var gameManager = GameObject.Find("Game Manager");

        gameManager.GetComponent<PlayerSetts>().AddXp(xpPerKill);

        var rand = Random.Range(2, 5);
        for (var i = 0; i < rand; i++)
        {
            Vector2 deathPos = transform.position;    
                
            var items = Instantiate(loot[Random.Range(0, loot.Length)], deathPos, Quaternion.identity);

            items.GetComponent<ItemTrigger>().item = items;
            items.GetComponent<ItemTrigger>().death = true;
            items.GetComponent<ItemTrigger>().startPos = deathPos;
                
            items.GetComponent<ItemTrigger>().startPos = deathPos;

            Vector2 deltaPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            var direction = (deltaPos).normalized;

            items.GetComponent<ItemTrigger>().deathDiscarding = deltaPos.magnitude;

            items.GetComponent<Rigidbody2D>().velocity = direction * 1.8f;
        }
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHeal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverHeal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        if (gameObject.GetComponent<Agent>())
        { 
            gameObject.GetComponent<Agent>().triggered = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        }

        health -= damage;
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
}
