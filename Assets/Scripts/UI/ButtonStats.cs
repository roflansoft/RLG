using UnityEngine;

public class ButtonStats : MonoBehaviour
{
    public void Button(GameObject statsPanel)
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }
}
