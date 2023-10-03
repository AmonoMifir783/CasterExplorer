using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagisteriyaCollected : MonoBehaviour, IDataPersistence
{
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    private GameObject pickedUpFruit;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        //pickedUpFruit = playerObject.GetComponent<MagisteriyaFruitPickUp>();
        //data.pickedUpFruit.TryGetValue(id, out pickedUpFruit);
    }

    public void SaveData(ref GameData data)
    {

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
