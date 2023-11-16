using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SalamandraId : MonoBehaviour, IDataPersistence
{
    private SalamandraSR salamandraSr;
    private GameObject pickedUpFruit;
    // private bool collected = false;
    //private SpriteRenderer visual;
    public bool salamandraDead = false;


    [SerializeField] private GameObject salamandraModelId;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        salamandraSr = GetComponent<SalamandraSR>();
    }



    public void LoadData(GameData data)
    {
        data.deadSalamandras.TryGetValue(id, out salamandraSr.salamandraDead);
        if (salamandraSr.salamandraDead)
        {
            salamandraModelId.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.deadSalamandras.ContainsKey(id))
        {
            data.deadSalamandras.Remove(id);
        }
        data.deadSalamandras.Add(id, salamandraSr.salamandraDead);
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}