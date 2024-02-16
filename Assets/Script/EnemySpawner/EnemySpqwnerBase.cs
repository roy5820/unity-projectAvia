using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpqwnerBase : MonoBehaviour
{
    public GameObject[] monsterPrefabs = null;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(delay);

            int randomSelect = Random.Range(0, monsterPrefabs.Length-1);
            Instantiate(monsterPrefabs[randomSelect], transform.position, transform.rotation);
        }
    }
}
