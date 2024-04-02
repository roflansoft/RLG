using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;

    public Vector2 offset = new(0f, 0f);

    public bool isLeft;

    private Transform _player;

    private int _lastX;

    private void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y - 1f);
        FindPlayer(isLeft);
    }

    private void FindPlayer(bool playerIsLeft)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _lastX = Mathf.RoundToInt(_player.position.x);
        transform.position = playerIsLeft
            ? new Vector3(_player.position.x - offset.x, _player.position.y - offset.y, transform.position.z)
            : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (!_player) return;
        
        var currentX = Mathf.RoundToInt(_player.position.x);
        if (currentX > _lastX) isLeft = false;
        else if (currentX < _lastX) isLeft = true;
        _lastX = Mathf.RoundToInt(_player.position.x);

        Vector3 target;
        target = isLeft
            ? new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z)
            : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);

        var currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
        transform.position = currentPosition;
    }
}
