using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagisteriyaCollected : MonoBehaviour, IDataPersistence
{
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    private GameObject pickedUpFruit;
    private bool collected;
    private GameObject visual;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        if (magisteriyaPickUpScript != null && magisteriyaPickUpScript.fruitPicked)
        {
            data.magisteriyaCollected.TryGetValue(id, out collected);
            if (collected)
            {
                visual.gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.magisteriyaCollected.ContainsKey(id))
        {
            data.magisteriyaCollected.Remove(id);
        }
        data.magisteriyaCollected.Add(id, collected);
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
