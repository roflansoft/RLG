using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawners = new ();
    
    public List<GameObject> enemies = new ();
    
    public List<GameObject> typeOfEnemies = new ();
    
    private List<GameObject> _doors = new ();
    
    public List<GameObject> levels = new ();
    
    public List<GameObject> passedLevels = new ();
    
    public GameObject chest;
    
    private bool _spawned;
    
    public GameObject image;

    [SerializeField] private bool openDoors;

    private void Start()
    {
        levels.Add(gameObject);
        Invoke("SetDoors", 3f);
        _spawned = false;
        openDoors = true;
        var spawnerPoints = transform.Find("SpawnPoints").gameObject;
        foreach (Transform child in spawnerPoints.transform)
        {
            spawners.Add(child.gameObject);
        }
    }

    private void SetDoors()
    {
        _doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
    }

    private void CloseAllDoors()
    {
        openDoors = false;
        foreach (var door in _doors)
        {
            door.gameObject.SetActive(true);
        }

    }

    private void OpenAllDoors()
    {
        openDoors = true;
        foreach (var door in _doors)
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _spawned) return;
        _spawned = true;
        CloseAllDoors();
        const int countSpawner = 3;
        Debug.Log("Count Spawners");
        Debug.Log(spawners.Count);
        Debug.Log("Count spawned enemies/chest");
        Debug.Log(countSpawner);
        while (spawners.Count != countSpawner)
        {
            var delIndex = Random.Range(0, spawners.Count);
            for (var i = delIndex; i < spawners.Count - 1; ++i)
            {
                spawners[i] = spawners[i + 1];
            }

            spawners.RemoveAt(spawners.Count - 1);
        }

        foreach (var spawner in spawners)
        {
            var rand = Random.Range(0, 11);
            switch (rand)
            {
                case < 10:
                {
                    var enemyType = typeOfEnemies[Random.Range(0, typeOfEnemies.Count)];
                    var enemy = Instantiate(enemyType, spawner.transform.position, Quaternion.identity);
                    enemy.transform.SetParent(gameObject.transform);
                    enemies.Add(enemy);
                    break;
                }
                case 10:
                {
                    var newChest = Instantiate(this.chest, spawner.transform.position, Quaternion.identity);
                    newChest.transform.SetParent(gameObject.transform);
                    break;
                }
            }
        }

        StartCoroutine(CheckEnemies());
    }

    private IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        levels.Remove(gameObject);
        passedLevels.Add(gameObject);
        if (levels.Count == 0)
        {
            image.gameObject.SetActive(true);
        }

        OpenAllDoors();
    }
}

