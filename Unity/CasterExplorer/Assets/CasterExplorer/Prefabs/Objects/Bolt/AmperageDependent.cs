using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
public class AmperageDependent : MonoBehaviour  
{  
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)  
    public float CurrentEnergy = 0;  
    public GameObject battery;
    public float MaxEnergy = 255;

    public bool isUpdateTime = false;

     private Material material;

    
    void Start()  
    {  
        Link_SpellReaction = GetComponent<SpellReaction>(); 
        
        Renderer objectRenderer = battery.GetComponent<Renderer>();
        material = objectRenderer.material;
    } 

    private void OnCollisionEnter(Collision collision)  
    { 
         if (collision.gameObject.CompareTag("Spell")) 
        {  
            Debug.Log(Link_SpellReaction.Amperage); 
            float A1 = Link_SpellReaction.Amperage;  
           
            if (CurrentEnergy < MaxEnergy)
            {
                if (CurrentEnergy + A1 > MaxEnergy)
                {
                    CurrentEnergy = MaxEnergy;
                }
                else 
                {
                    CurrentEnergy += A1;
                }

            }
            
            if (!isUpdateTime)
            {
                isUpdateTime = true;
                InvokeRepeating("UpdateTime", 0f, 1f);
            }
            
        }  
    }  
   
   bool Move = false;
    void UpdateTime()
    {
        if (CurrentEnergy > 0)
        {
            CurrentEnergy -= 1;
            Debug.Log("Текущий заряд - " + CurrentEnergy);

            Color baseColor = material.GetColor("_BaseColor");
            baseColor.g = CurrentEnergy/10;
            // Присваиваем значение 200 в синий канал (диапазон от 0 до 1)
            material.SetColor("_BaseColor", baseColor);
            
            if (!Move)
            {
                Move = true;
                transform.parent.GetComponent<RoomInspector>().OpenTheDoor();   
            }
        }
        else
        {
            isUpdateTime = false;
            CancelInvoke("UpdateTime");
        }
    }
}