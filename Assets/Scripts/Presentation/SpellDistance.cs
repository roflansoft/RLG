using UnityEngine;

public class SpellDistance : MonoBehaviour
{
    public GameObject spell;

    public Vector3 startPos;

    public float distance;

    private void Update()
    {
        if (Vector2.Distance(startPos, spell.GetComponent<Transform>().position) > distance)
        {
            Destroy(spell);
        }
    }
}
