using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnArea = new Vector3(5,5,5);
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float timeToSpawn = 1f;
    private void Start()
    {
        StartCoroutine(SpawnFood());
    }
    private IEnumerator SpawnFood()
    {
        while (true)
        {
            Instantiate(foodPrefab, GetRandomSpawnPos(spawnArea), Quaternion.identity, transform);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    private Vector3 GetRandomSpawnPos(Vector3 area)
    {
        float posX = Random.Range(-area.x /2, area.x /2);
        float posY = Random.Range(-area.y /2, area.y /2);
        return new Vector3(posX, posY, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
