using UnityEngine;

public class ActiveStats : MonoBehaviour
{
    public GameObject statsPanel;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        statsPanel.SetActive(!statsPanel.activeSelf);
    }
}
