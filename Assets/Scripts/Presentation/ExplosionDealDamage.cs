using UnityEngine;

public class ExplosionDealDamage : MonoBehaviour
{
    public GameObject spell;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Projectile" || collision.tag == "Open_door") return;
        if (collision.GetComponent<EnemyReceiveDamage>() == null) return;
        collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
        Destroy(spell);
    }
}
