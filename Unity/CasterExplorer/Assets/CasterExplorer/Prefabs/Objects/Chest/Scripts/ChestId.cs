using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChestId : MonoBehaviour, IDataPersistence
{

    private PickChest pickChest;
    public bool chestPicked = false;


    [SerializeField] private GameObject Chest;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        pickChest = GetComponent<PickChest>();
    }



    public void LoadData(GameData data)
    {
        data.pickedChests.TryGetValue(id, out pickChest.chestPicked);
        if (pickChest.chestPicked)
        {
            Chest.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.pickedChests.ContainsKey(id))
        {
            data.pickedChests.Remove(id);
        }
        data.pickedChests.Add(id, pickChest.chestPicked);
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}