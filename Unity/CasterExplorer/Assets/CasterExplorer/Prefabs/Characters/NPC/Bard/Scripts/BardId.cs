using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BardId : MonoBehaviour, IDataPersistence
{
    private BardSR bardSr;
    //private GameObject pickedUpFruit;
    // private bool collected = false;
    //private SpriteRenderer visual;
    public bool bardDead = false;


    [SerializeField] private GameObject Bardid;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        bardSr = GetComponent<BardSR>();
    }



    public void LoadData(GameData data)
    {
        data.deadBards.TryGetValue(id, out bardSr.bardDead);
        if (bardSr.bardDead)
        {
            Bardid.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.deadBards.ContainsKey(id))
        {
            data.deadBards.Remove(id);
        }
        data.deadBards.Add(id, bardSr.bardDead);
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}