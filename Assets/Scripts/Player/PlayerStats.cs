using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text healthValue;
    
    public Text lvlValue;
    
    public Text armorValue;
    
    public Text evasionValue;
    
    public Text magicalResistanceValue;
    
    public Text criticalChanceValue;
    
    public Text criticalMultiplierValue;
    
    public Text minDamageValue;
    
    public Text maxDamageValue;
    
    public GameObject gameManager;

    public void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        healthValue.text = gameManager.GetComponent<PlayerSetts>().health.ToString();
        lvlValue.text = gameManager.GetComponent<PlayerSetts>().level.ToString();
        armorValue.text = gameManager.GetComponent<PlayerSetts>().armor.ToString();
        evasionValue.text = (gameManager.GetComponent<PlayerSetts>().evasionChance * 100).ToString() + "%";
        magicalResistanceValue.text = (gameManager.GetComponent<PlayerSetts>().magicalResistance * 100).ToString() + "%";
        criticalChanceValue.text = (gameManager.GetComponent<PlayerSetts>().criticalChance * 100).ToString() + "%";
        criticalMultiplierValue.text = gameManager.GetComponent<PlayerSetts>().criticalDamageMultiplier.ToString();
    }
}
