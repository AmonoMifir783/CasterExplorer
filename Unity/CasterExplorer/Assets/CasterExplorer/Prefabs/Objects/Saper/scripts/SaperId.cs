using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaperId : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    private ShowNumber showNumber;

    [SerializeField] private GameObject Saperid;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        showNumber = GetComponent<ShowNumber>();
    }
    public void LoadData(GameData data)
    {
        //data.saperNumbers.TryGetValue(id, out showNumber.numberShow);
        //if (showNumber.numberShow)
        //{
        //    Saperid.gameObject.SetActive(false);
        //}
    }

    public void SaveData(GameData data)
    {
        //if (data.saperNumbers.ContainsKey(id))
        //{
        //    data.saperNumbers.Remove(id);
        //}
        //data.saperNumbers.Add(id, showNumber.numberShow);
    }
}
