using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Projectile" || collision.tag == "Open_door" ||
            collision.tag == "Level") return;
        if (collision.GetComponent<EnemyReceiveDamage>() != null)
        {
            collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
        }

        Destroy(gameObject);
    }
}
