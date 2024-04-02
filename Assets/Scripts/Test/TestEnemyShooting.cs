using System.Collections;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
    public GameObject projectile;

    private GameObject _player;

    public float minDamage;

    public float maxDamage;

    public float dist;

    public float projectileForce;

    public float cooldown;

    private void Start()
    {
        StartCoroutine(ShootPlayer());
        _player = FindObjectOfType<MoveObject>().gameObject;
    }

    private IEnumerator ShootPlayer()
    {

        yield return new WaitForSeconds(cooldown);

        if (_player != null && gameObject.GetComponent<Agent>().triggered == true)
        {
            var spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 playerPos = _player.transform.position;
            Vector2 myPos = transform.position;
            var direction = (playerPos - myPos).normalized;

            var spellTest = transform.eulerAngles;

            spellTest.z = Mathf.Atan((playerPos.y - myPos.y) / (playerPos.x - myPos.x)) * Mathf.Rad2Deg;

            if (playerPos.x - myPos.x < 0) spell.GetComponent<SpriteRenderer>().flipX = true;
            spell.GetComponent<Transform>().eulerAngles = spellTest;

            spell.GetComponent<SpellDistance>().startPos = myPos;
            spell.GetComponent<SpellDistance>().distance = dist;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);

            StartCoroutine(ShootPlayer());
        }
        else
        {
            StartCoroutine(ShootPlayer());
        }
    }
}
