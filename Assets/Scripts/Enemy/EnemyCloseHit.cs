using System.Collections;
using UnityEngine;

public class EnemyCloseHit : MonoBehaviour
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

    IEnumerator ShootPlayer()
    {

        yield return new WaitForSeconds(cooldown);

        if (_player != null && gameObject.GetComponent<Agent>().triggered == true)
        {
            GameObject Spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 playerPos = _player.transform.position;
            Vector2 myPos = transform.position;
            Vector2 direction = (playerPos - myPos).normalized;

            Vector3 spell_test = transform.eulerAngles;

            spell_test.z = Mathf.Atan((playerPos.y - myPos.y) / (playerPos.x - myPos.x)) * Mathf.Rad2Deg;

            if (playerPos.x - myPos.x < 0) Spell.GetComponent<SpriteRenderer>().flipX = true;
            Spell.GetComponent<Transform>().eulerAngles = spell_test;

            Spell.GetComponent<SpellDistance>().startPos = myPos;
            Spell.GetComponent<SpellDistance>().distance = dist;

            Spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            Spell.GetComponent<EnemyCloseProjectile>().damage = Random.Range(minDamage, maxDamage);

            StartCoroutine(ShootPlayer());
        }
        else
        {
            StartCoroutine(ShootPlayer());
        }
    }
}
