using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;

    public float minDamage;

    public float maxDamage;

    public float projectileForce;

    public float projectileDistance;

    public float cooldownTime;

    private float _nextFireTime;

    private void Start()
    {
        _nextFireTime = Time.time;
    }

    private void Update()
    {
        if (!(Time.time > _nextFireTime)) return;
        if (Inventory.IsOpenInventory) return;
        if (!Input.GetMouseButtonDown(1)) return;
        _nextFireTime = Time.time + cooldownTime;
        var spell = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        spell.GetComponent<SpellDistance>().startPos = myPos;
        spell.GetComponent<SpellDistance>().distance = projectileDistance;

        var spellTest = transform.eulerAngles;

        spellTest.z = Mathf.Atan((mousePos.y - myPos.y) / (mousePos.x - myPos.x)) * Mathf.Rad2Deg;

        if (mousePos.x - myPos.x < 0)
        {
            spell.GetComponent<SpriteRenderer>().flipX = true;
            spell.GetComponent<Collider2D>().offset *= -1;
        }
        spell.GetComponent<Transform>().eulerAngles = spellTest;

        var direction = (mousePos - myPos).normalized;

        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
    }
}
