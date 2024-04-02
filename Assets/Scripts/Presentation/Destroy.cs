using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject spell;
    
    private float _fireTime;

    private void Start()
    {
        _fireTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - _fireTime > 0.3)
            Destroy(spell);
    }
}
