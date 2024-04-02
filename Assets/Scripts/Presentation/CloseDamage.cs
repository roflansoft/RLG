using System;
using UnityEngine;

public class CloseDamage : MonoBehaviour
{
    public GameObject projectile;
    
    public float minDamage;

    public float maxDamage;

    public float cooldownTime;

    public float projectileForce;

    public float projectileDistance;

    private float _nextFireTime;

    public GameObject gameManager;

    public Action Attack;

    private void Start()
    {
        _nextFireTime = Time.time;
        Attack += GetDamage;
    }

    private void Update()
    {
        if (!(Time.time > _nextFireTime)) return;
        
        if (Inventory.IsOpenInventory) return;
        
        if (!Input.GetMouseButtonDown(0)) return;
        
        _nextFireTime = Time.time + cooldownTime;
        Attack.Invoke();

    }

    public void GetDamage()
    {
        var hit = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        hit.GetComponent<SpellDistance>().startPos = myPos;
        hit.GetComponent<SpellDistance>().distance = projectileDistance;

        var hitTest = transform.eulerAngles;

        hitTest.z = Mathf.Atan((mousePos.y - myPos.y) / (mousePos.x - myPos.x)) * Mathf.Rad2Deg;

        if (mousePos.x - myPos.x < 0)
        {
            hit.GetComponent<SpriteRenderer>().flipX = true;
            hit.GetComponent<Collider2D>().offset *= -1;
        }
        hit.GetComponent<Transform>().eulerAngles = hitTest;

        var direction = (mousePos - myPos).normalized;

        hit.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        hit.GetComponent<CloseProjectile>().damage = UnityEngine.Random.Range(minDamage, maxDamage) * gameManager.GetComponent<PlayerSetts>().GetDamageMultiplier();
    }
}
