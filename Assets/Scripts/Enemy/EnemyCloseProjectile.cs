using UnityEngine;

public class EnemyCloseProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        switch (Collision.tag)
        {
            case "Enemy":
            case "Projectile":
                return;
            case "Player":
                PlayerSetts.playerStats.DealDamage(damage);
                break;
        }
    }
}
