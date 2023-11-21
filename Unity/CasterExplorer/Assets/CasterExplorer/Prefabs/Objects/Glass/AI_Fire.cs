using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Fire : MonoBehaviour
{
    public Transform target; // Ссылка на объект, который будет перемещаться
    public float speed = 0.2f;
    public float targetHeight = 3f;
    private Vector3 initialPosition;
    private float startTime;
    public int minTemp = -5;
    public int maxTemp = 15;
    //public bool isFire = false;
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = target.position;
        startTime = Time.timeSinceLevelLoad; // Исправлено использование времени
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиска компонента
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(Link_SpellReaction.Temperature);
        int F1 = Link_SpellReaction.Temperature;
        if (collision.gameObject.CompareTag("Spell"))
        {
            if (F1 <= minTemp)
            {
                StartCoroutine(MoveTargetUp());
                Debug.Log("Цепь замкнулась. Объект поднялся");
            }
            else if (F1 >= maxTemp)
            {
                StartCoroutine(MoveTargetDown());
                Debug.Log("Цепь замкнулась. Объект опустился");
            }
        }
    }

    IEnumerator MoveTargetUp()
    {
        float elapsedTime = 0f;
        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            target.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * targetHeight, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveTargetDown()
    {
        float elapsedTime = 0f;
        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            target.position = Vector3.Lerp(initialPosition, initialPosition - Vector3.up * targetHeight, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

