using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerSetts : MonoBehaviour
{
    public static PlayerSetts playerStats;

    public GameObject player;

    public float maxHealth;

    public float health;

    public GameObject healthBar;

    public Slider healthBarSlider;

    public GameObject fill;

    public Text healthPercent;

    public Text lvlTextPallet;

    public GameObject dieLight;

    public GameObject tomb;

    public float evasionChance = 0.01f;
    
    public float criticalChance = 0.05f;
    
    public float criticalDamageMultiplier = 1.5f;
    
    public int xp = 0;
    
    public int level = 1;
    
    public int maxLevel = 50;
    
    public float armor = 0.0f;
    
    public float magicalResistance = 0.02f;

    public GameObject PlayerStats;

    private bool IsEvaded()
    {
        return Random.Range(0f, 1f) < evasionChance;
    }

    public float GetDamageMultiplier()
    {
        if (!(Random.Range(0f, 1f) < criticalChance)) return 1;
        Debug.Log("Crit");
        return criticalDamageMultiplier;
    }

    private int XpNeededToLevelUp()
    {
        if (level == maxLevel)
        {
            return Int32.MaxValue;
        }
        return 20 + 15 * level;
    }

    private void LevelUp()
    {
        xp -= XpNeededToLevelUp();
        ++level;

        evasionChance += 0.001f;
        criticalChance += 0.002f;
        criticalDamageMultiplier += 0.01f;
        lvlTextPallet.text = level.ToString();

        PlayerStats.GetComponent<PlayerStats>().UpdateStats();
    }

    public void AddXp(int addXp)
    {
        xp += addXp;
        while (xp > XpNeededToLevelUp())
        {
            LevelUp();
        }
    }

    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetActive(true);
        fill.GetComponent<Image>().color = new Color(0.06260932f, 0.8313726f, 0.01176468f, 1f);
    }

    private void CheckDeath()
    {
        if (!(health <= 0)) return;
        Debug.Log("Die*");
        fill.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        var pos = player.transform.position;
        pos.z = -2;
        Instantiate(dieLight, pos, Quaternion.identity);
        Instantiate(tomb, pos, Quaternion.identity);
        Destroy(player);
        health = 0f;
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverHeal();
        healthPercent.text = "" + health + "%";
        healthBarSlider.value = CalculateHealthPercentage();
        if (health / maxHealth > 0.6)
        {
            fill.GetComponent<Image>().color = new Color(0.06260932f, 0.8313726f, 0.01176468f, 1f);
        }
        else if (health / maxHealth > 0.3)
        {
            fill.GetComponent<Image>().color = new Color(0.8509804f, 0.8154773f, 0.08235291f, 1f);
        }
        PlayerStats.GetComponent<PlayerStats>().UpdateStats();
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
        if (!IsEvaded())
        {
            health -= damage * (Mathf.Pow(2.718f, 100.0f / (armor + 144.27f)) - 1);
            CheckDeath();
            healthPercent.text = "" + health + "%";
            healthBarSlider.value = CalculateHealthPercentage();
            if (health / maxHealth < 0.6)
            {
                fill.GetComponent<Image>().color = new Color(0.8509804f, 0.8154773f, 0.08235291f, 1f);
            }
            if (health / maxHealth < 0.3)
            {
                fill.GetComponent<Image>().color = new Color(0.8301887f, 0.01174793f, 0.01174793f, 1f);
            }
        }
        PlayerStats.GetComponent<PlayerStats>().UpdateStats();
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void ChangeArmor(float deltaArmor)
    {
        armor += deltaArmor;
        PlayerStats.GetComponent<PlayerStats>().UpdateStats();
    }
}
