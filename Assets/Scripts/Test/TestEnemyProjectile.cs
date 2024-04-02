using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
            case "Projectile":
            case "Level":
                return;
            case "Player":
                PlayerSetts.playerStats.DealDamage(damage);
                break;
        }
        Destroy(gameObject);
    }
}
