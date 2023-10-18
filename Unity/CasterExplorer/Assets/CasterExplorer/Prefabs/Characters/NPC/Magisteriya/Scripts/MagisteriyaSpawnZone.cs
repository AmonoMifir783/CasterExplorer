using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // ������ ������������ �������
    public float spawnTime = 3f; // ����� ����� ��������
    private bool canSpawn = true; // ����, �����������, ����� �� �������� �������
    private bool isCollected = false; // ����, �����������, ��� �� ������ ��������
    private GameObject spawnedObject; // ������ �� ������������ ������

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