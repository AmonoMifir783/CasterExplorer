using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
public class AI_Fire : MonoBehaviour 
{ 
    public Transform target; // Ссылка на объект, который будет перемещаться 
    public float speed = 0.2f; 
    public float targetHeight = 3f; 
    float maxHeight = 3f;
    private Vector3 initialPosition; 
    private float startTime; 
    public int currentTemp = 0; 
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
            if (currentTemp >= minTemp && currentTemp <= maxTemp ) 
            {    
                if (F1 < minTemp) 
                { 
                    maxHeight = targetHeight;
                    StartCoroutine(MoveTargetDown()); 
                    Debug.Log("Down"); 
                } 
                else if (F1 > maxTemp) 
                { 
                    maxHeight = targetHeight;
                    StartCoroutine(MoveTargetUp()); 
                    Debug.Log("Up"); 
                } 
            } 
            else if (currentTemp > maxTemp) 
            { 
                if (F1 < maxTemp && F1 > minTemp) 
                { 
                    maxHeight = targetHeight;
                    StartCoroutine(MoveTargetDown()); 
                    Debug.Log("Down"); 
                } 
                else if (F1 < minTemp) 
                { 
                    maxHeight = 2 * targetHeight;
                    StartCoroutine(MoveTargetDown()); 
                    Debug.Log("Down_Down"); 
                } 
            } 
             else if (currentTemp < minTemp) 
            { 
                if (F1 < maxTemp && F1 > minTemp) 
                { 
                    maxHeight = targetHeight;
                    StartCoroutine(MoveTargetUp()); 
                    Debug.Log("Up"); 
                } 
                else if (F1 > maxTemp) 
                { 
                    maxHeight = 2 * targetHeight;
                    StartCoroutine(MoveTargetUp()); 
                    Debug.Log("Up_Up"); 
                } 
            } 
            currentTemp = F1; 
        } 
    } 
    IEnumerator MoveTargetUp() 
    { 
        float elapsedTime = 0f; 
        initialPosition = target.position;
        while (elapsedTime < maxHeight / speed) 
        { 
            float fractionOfJourney = elapsedTime / ( maxHeight / speed); 
            target.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * maxHeight, fractionOfJourney); 
            elapsedTime += Time.deltaTime; 
            yield return null; 
        } 
    } 
    IEnumerator MoveTargetDown() 
    { 
        float elapsedTime = 0f;
        initialPosition = target.position;
        while (elapsedTime < maxHeight / speed) 
        { 
            float fractionOfJourney = elapsedTime / (maxHeight / speed); 
            target.position = Vector3.Lerp(initialPosition, initialPosition - Vector3.up * maxHeight, fractionOfJourney); 
            elapsedTime += Time.deltaTime; 
            yield return null; 
        } 
    } 
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Fire : MonoBehaviour
{
    public Transform target; // Ссылка на объект, который будет перемещаться
    public float speed = 0.2f;
    public float targetHeight = 3f;
    private Vector3 initialPosition;
    private float startTime;
    private bool isMoving = false; // Флаг для отслеживания состояния движения
    private Coroutine moveCoroutine; // Ссылка на текущую корутину перемещения
    public int minTemp = -5;
    public int maxTemp = 15;
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = target.position;
        startTime = Time.timeSinceLevelLoad;
        Link_SpellReaction = GetComponent<SpellReaction>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(Link_SpellReaction.Temperature);
        int F1 = Link_SpellReaction.Temperature;
        if (collision.gameObject.CompareTag("Spell"))
        {
            if (F1 >= maxTemp && !isMoving && transform.parent.GetComponent<AI_Cisterna>().isMoveUp)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine); // Остановка предыдущей корутины, если она была запущена
                    moveCoroutine = StartCoroutine(MoveTargetUp());
                    Debug.Log("Цепь замкнулась. Объект поднялся");
            }
            else if (F1 <= minTemp && !isMoving && transform.parent.GetComponent<AI_Cisterna>().isMoveDown)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine); // Остановка предыдущей корутины, если она была запущена
                    moveCoroutine = StartCoroutine(MoveTargetDown());
                    Debug.Log("Цепь замкнулась. Объект опустился");
            }
        }
    }

    IEnumerator MoveTargetUp()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = target.position; // Запоминаем текущую позицию перед началом движения
        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            target.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * targetHeight, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
        transform.parent.GetComponent<AI_Cisterna>().isMoveDown = true;
        transform.parent.GetComponent<AI_Cisterna>().isMoveUp = false;
    }

    IEnumerator MoveTargetDown()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = target.position; // Запоминаем текущую позицию перед началом движения
        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            target.position = Vector3.Lerp(startPosition, startPosition - Vector3.up * targetHeight, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
        transform.parent.GetComponent<AI_Cisterna>().isMoveDown = false;
        transform.parent.GetComponent<AI_Cisterna>().isMoveUp = true;
    }
}*/
