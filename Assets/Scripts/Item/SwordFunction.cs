using UnityEngine;

public class SwordFunction : PutOnFunction
{
    public GameObject projectile;
    
    public GameObject particle;
    
    public GameObject sword;
    
    public float projectileDistance;
    
    public float minDamage;
    
    public float maxDamage;
    
    public float projectileForce;
    
    public GameObject gameManager;

    public GameObject player;

    public override void UseItem()
    {
        player = GameObject.Find("Player");
        PutPicture("Right arm", sword);
        player.GetComponent<CloseDamage>().Attack -= player.GetComponent<CloseDamage>().GetDamage;
        player.GetComponent<CloseDamage>().Attack += NewAttack;
        gameManager = GameObject.Find("Game Manager");
    }

    public override void UnUseItem()
    {
        TakeOffPicture("Right arm");
        player.GetComponent<CloseDamage>().Attack -= NewAttack;
        player.GetComponent<CloseDamage>().Attack += player.GetComponent<CloseDamage>().GetDamage;
    }

    private void NewAttack()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = player.GetComponent<Transform>().transform.position;

        var hitTest = transform.eulerAngles;

        hitTest.z = Mathf.Atan((mousePos.y - myPos.y) / (mousePos.x - myPos.x)) * Mathf.Rad2Deg;

        Vector3 direction = (mousePos - myPos).normalized;

        var Particle = Instantiate(particle, player.GetComponent<Transform>().transform.position + direction * projectileForce, Quaternion.identity);
        var Hit = Instantiate(projectile, player.GetComponent<Transform>().transform.position + direction * projectileForce, Quaternion.identity);
        Hit.GetComponent<ExplosionDealDamage>().damage = UnityEngine.Random.Range(minDamage, maxDamage) * gameManager.GetComponent<PlayerSetts>().GetDamageMultiplier();
    }
}
