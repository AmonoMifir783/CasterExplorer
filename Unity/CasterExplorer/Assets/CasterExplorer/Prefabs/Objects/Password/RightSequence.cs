using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSequence : MonoBehaviour
{   
    List<int> sequence = new List<int> ();
    public int[] check = new int[3];
    bool areEqual = false;

    public void SequencwAnalitics(int number)
    {
        sequence.Add(number); 
        //Debug.Log("Поступаемое значение = " + number);


        if(sequence.Count == check.Length)
        {  
            
            for (int i = 0; i < check.Length; i++)
            {
                if (sequence[i] == check[i])
                { 
                    areEqual = true;
         
                }
                else 
                {
                    areEqual = false;
                    sequence.Clear();
                    break;
                }
            }

            if (areEqual == true)
            {
                Debug.Log("Hello");
            }   

        }
        

    }
          
} 



























