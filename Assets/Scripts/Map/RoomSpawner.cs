using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    
    private const float WaitTime = 100f;

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants _variants;
    
    private int _rand;
    
    private static readonly HashSet<Vector3> SpawnPointsSet = new HashSet<Vector3>() { new Vector3(0f, 0f, 0f) };
    
    private GameObject _level;

    private void Start()
    {
        var pos = transform.position;
        if (SpawnPointsSet.Contains(pos))
        {
            Destroy(gameObject);
        }
        else
        {
            SpawnPointsSet.Add(pos);
            _variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
            Destroy(gameObject, WaitTime);
            Invoke("Spawn", 0.05f);
        }
    }

    public void Spawn()
    {
        switch (direction)
        {
            case Direction.Top:
                _rand = Random.Range(0, _variants.topRooms.Length);
                _level = Instantiate(_variants.topRooms[_rand], transform.position, _variants.topRooms[_rand].transform.rotation);
                break;
            case Direction.Bottom:
                _rand = Random.Range(0, _variants.bottomRooms.Length);
                _level = Instantiate(_variants.bottomRooms[_rand], transform.position, _variants.bottomRooms[_rand].transform.rotation);
                break;
            case Direction.Left:
                _rand = Random.Range(0, _variants.leftRooms.Length);
                _level = Instantiate(_variants.leftRooms[_rand], transform.position, _variants.leftRooms[_rand].transform.rotation);
                break;
            case Direction.Right:
                _rand = Random.Range(0, _variants.rightRooms.Length);
                _level = Instantiate(_variants.rightRooms[_rand], transform.position, _variants.rightRooms[_rand].transform.rotation);
                break;
        }
    } 

}
