using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftWall;

    public GameObject upWall;

    public GameObject rightWall;

    public GameObject downWall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall") return;
        
        var angle = transform.localEulerAngles.z;
        switch (angle)
        {
            case 0:
                Instantiate(leftWall, transform.position, Quaternion.identity);
                break;
            case 270:
                Instantiate(upWall, transform.position, Quaternion.identity);
                break;
            case 180:
                Instantiate(rightWall, transform.position, Quaternion.identity);
                break;
            case 90:
                Instantiate(downWall, transform.position, Quaternion.identity);
                break;
        }

        Destroy(gameObject);
    }
}
