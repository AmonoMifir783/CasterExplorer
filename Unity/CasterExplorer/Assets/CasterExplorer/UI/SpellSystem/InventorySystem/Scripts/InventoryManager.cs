using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
public GameObject FastSlot1;
public GameObject FastSlot2;
public GameObject FastSlot3;
public GameObject FastSlot4;
//FastSlot1.GetComponent<InventorySlots>().Item;
//public CreateNewSlots createNewSlots;
public Transform InventoryPanel;
public List<InventorySlots> Slots = new List<InventorySlots>();
public Dictionary<string, ItemScriptableObject> spellDictionary = new Dictionary<string, ItemScriptableObject>();
public int i = 0;

public bool isLoading = false;

public GameObject prefab; // ,
//public Transform contentPanel;

public Sprite SpellArt;
public GameObject CreateSlot;

void Start()
{
//createNewSlots = CreateSlot.GetComponent<CreateNewSlots>();

Inicialization();

}

public void Inicialization()
{
while (i < InventoryPanel.childCount)
{
if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
{
InventorySlots slot = InventoryPanel.GetChild(i).GetComponent<InventorySlots>();
Slots.Add(slot);
if (slot.Item != null && !spellDictionary.ContainsKey(slot.Item.name))
{
spellDictionary.Add(slot.Item.name, slot.Item);
}
i++;
}
}
}


int Temperature;
int Force;
int Amperage;
int Gravity;
int Light;

public void LoadData(GameData data)
{

int j = 0;

foreach (string spellName in data.spellNames)
{
Debug.Log("Размер - " + data.spellAtributs.Count);

Temperature = data.spellAtributs[j];
j++;

Force = data.spellAtributs[j];
j++;

Amperage = data.spellAtributs[j];
j++;

Gravity = data.spellAtributs[j];
j++;

Light = data.spellAtributs[j];
j++;

Debug.Log("Всё ок " + spellName);


CreateSpell(Temperature, Force, Amperage, Gravity, Light);

}
}

public void SaveData(GameData data)
{
foreach (InventorySlots slot in Slots)
{
if (slot.Item != null && !data.spellNames.Contains(slot.Item.name) && slot.Item.name != "1" && slot.Item.name != "2")
{
data.spellNames.Add(slot.Item.name);

Debug.Log("Я родился! - " + slot.Item.name);

data.spellAtributs.Add(slot.Item.Temperature);
data.spellAtributs.Add(slot.Item.Force);
data.spellAtributs.Add(slot.Item.Amperage);
data.spellAtributs.Add(slot.Item.Gravity);
data.spellAtributs.Add(slot.Item.Light);
}
}
}


public int CountSpell = 0;

public void CreateSpell(int Temperature, int Force, int Amperage, int Gravity, int light)
{
SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();

// Настройка свойств SpellItem
spellItem.Temperature = Temperature;
spellItem.Force = Force;
spellItem.Amperage = Amperage;
spellItem.Gravity = Gravity;
spellItem.Light = light;

CountSpell++;

spellItem.Icon = SpellArt;
spellItem.ItemName = CountSpell;


//string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/" + CountSpell + ".asset";

//#if UNITY_EDITOR
// AssetDatabase.CreateAsset(spellItem, targetPath);
// AssetDatabase.SaveAssets();
//#endif

Inicialization();

Instantiate(prefab, InventoryPanel); // Создаем префаб как дочерний элемент панели Content
AddItem(spellItem);

}

public void AddItem(ItemScriptableObject _item)
{
//Instantiate(prefab, InventoryPanel);
Inicialization();

foreach (InventorySlots slot in Slots)
{
if (slot.isEmpty)
{
slot.Item = _item;
slot.isEmpty = false;
slot.isHighlighting = false;

slot.SetIcon(_item.Icon);
slot.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
break;
}
}
}

}
