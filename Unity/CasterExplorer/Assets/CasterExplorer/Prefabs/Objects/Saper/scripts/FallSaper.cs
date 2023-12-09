using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSaper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blockToFall; // ������ �� ����, ������� ������ ������
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("upal");
            isPlayerInside = true;
            StartCoroutine(FallDown());
        }
    }

    private IEnumerator FallDown()  
    {
        float speed = 100f; // �������� �������
        float distance = 100f; // ���������� �������
        float time = distance / speed; // ����� �������

        Vector3 targetPosition = blockToFall.transform.position + new Vector3(0f, -distance, 0f); // ������� �������

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            blockToFall.transform.position = Vector3.Lerp(blockToFall.transform.position, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blockToFall.transform.position = targetPosition;
    }
}
