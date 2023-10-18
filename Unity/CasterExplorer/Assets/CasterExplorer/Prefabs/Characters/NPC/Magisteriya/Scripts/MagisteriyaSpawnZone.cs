using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // префаб спавнящегося объекта
    public float spawnTime = 3f; // время между спавнами
    private bool canSpawn = true; // флаг, указывающий, можно ли спавнить объекты
    private bool isCollected = false; // флаг, указывающий, был ли объект подобран
    private GameObject spawnedObject; // ссылка на заспавненный объект

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            if (canSpawn && !isCollected && spawnedObject == null)
            {
                Vector3 randomPosition = GetRandomSpawnPosition();
                Quaternion randomRotation = GetRandomSpawnRotation();
                if (objectPrefab != null)
                {
                    spawnedObject = Instantiate(objectPrefab, randomPosition, randomRotation);
                }
                canSpawn = false;
                yield return new WaitForSeconds(spawnTime);
                canSpawn = true;
            }
            yield return null;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Collider spawnCollider = GetComponent<Collider>();
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            Random.Range(spawnCollider.bounds.min.y, spawnCollider.bounds.max.y),
            Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z)
        );
        return randomPosition;
    }

    private Quaternion GetRandomSpawnRotation()
    {
        Quaternion randomRotation = Quaternion.Euler(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );
        return randomRotation;
    }

    public void CollectObject()
    {
        isCollected = true;
        StartCoroutine(ResetCollection());
    }

    private IEnumerator ResetCollection()
    {
        yield return new WaitForSeconds(spawnTime);
        isCollected = false;
        spawnedObject = null;
    }
}