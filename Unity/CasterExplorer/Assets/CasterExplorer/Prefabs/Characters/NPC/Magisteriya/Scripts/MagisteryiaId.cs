using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagisteryiaId : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    private PickMagisteriya pickFruit;
    public bool fruitPicked = false;


    [SerializeField] private GameObject Fruit;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        pickFruit = GetComponent<PickMagisteriya>();
    }



    public void LoadData(GameData data)
    {
        data.pickedFruits.TryGetValue(id, out pickFruit.fruitPicked);
        if (pickFruit.fruitPicked)
        {
            Fruit.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.pickedFruits.ContainsKey(id))
        {
            data.pickedFruits.Remove(id);
        }
        data.pickedFruits.Add(id, pickFruit.fruitPicked);
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
