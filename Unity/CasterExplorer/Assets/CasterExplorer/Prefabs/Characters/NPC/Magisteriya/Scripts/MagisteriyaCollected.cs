using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagisteriyaCollected : MonoBehaviour, IDataPersistence
{
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    private GameObject pickedUpFruit;
   // private bool collected = false;
    private GameObject visual;
    public bool fruitPicked = false;


    [SerializeField] private GameObject Magisteriya;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Start()
    {
        magisteriyaPickUpScript = GetComponent<MagisteriyaFruitPickUp>();
        visual = Magisteriya;
    }



    public void LoadData(GameData data)
    {
        if (magisteriyaPickUpScript != null)
        {
            if (data.magisteriyaCollected.TryGetValue(id, out fruitPicked))
            {
                if (fruitPicked)
                {
                    Magisteriya.gameObject.SetActive(false);
                }
                else
                {
                    Magisteriya.gameObject.SetActive(true);
                }
            }
            else
            {
                if (!fruitPicked)
                {
                    Magisteriya.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (magisteriyaPickUpScript != null)
        {
            data.magisteriyaCollected[id] = fruitPicked;
        }
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
