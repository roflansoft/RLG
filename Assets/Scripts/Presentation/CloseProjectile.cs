using UnityEngine;

public class CloseProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Projectile" || collision.tag == "Open_door") return;
        
        if (collision.GetComponent<EnemyReceiveDamage>() != null)
        {
            collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
        }
    }
}
