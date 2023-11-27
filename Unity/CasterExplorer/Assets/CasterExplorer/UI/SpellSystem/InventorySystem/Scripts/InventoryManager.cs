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
    public CreateNewSlots createNewSlots;
    public Transform InventoryPanel;
    public List<InventorySlots> Slots = new List<InventorySlots>();
    public Dictionary<string, ItemScriptableObject> spellDictionary = new Dictionary<string, ItemScriptableObject>();
    public int i = 0;

    public GameObject prefab; // Ссылка на префаб, который нужно создать
    //public Transform contentPanel;

    void Start()
    {
        createNewSlots = FindObjectOfType<CreateNewSlots>();
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

    public void LoadData(GameData data)
    {
        foreach (string spellName in data.spellNames)
        {
            string spellPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items/" + spellName + ".asset";
            //ItemScriptableObject spell = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemScriptableObject>(spellPath);
            //if (spell != null)
            //{
            //    spell.Icon = GenerateTexture();
            //    AddItem(spell);
            //}
        }
    }

    public void SaveData(GameData data)
    {
        foreach (InventorySlots slot in Slots)
        {
            if (slot.Item != null && !data.spellNames.Contains(slot.Item.name) && slot.Item.name != "1" && slot.Item.name != "2")
            {
                data.spellNames.Add(slot.Item.name);
            }
        }
    }
    public void AddItem(ItemScriptableObject _item)
    {
        Instantiate(prefab, InventoryPanel); // Создаем префаб как дочерний элемент панели Content
        Inicialization();
        //Instantiate(prefab, contentPanel); // Создаем префаб как дочерний элемент панели Content
        foreach (InventorySlots slot in Slots)
        {
            if (slot.isEmpty)
            {
                slot.Item = _item;
                slot.isEmpty = false;
                slot.isHighlighting = false;
                slot.SetIcon(_item.Icon);
                break;
            }
        }
    }
    public int width = 256;
    public int height = 256;
    public Texture2D texture;
    public Sprite GenerateTexture()
    {
        // Создаем новую текстуру с заданными размерами
        texture = new Texture2D(width, height);

        // Заполняем все пиксели текстуры выбранным цветом
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        Color32[] pixels = new Color32[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        // Заполняем текстуру пикселями
        texture.SetPixels32(pixels);

        // Применяем изменения к текстуре
        texture.Apply();

        // Создаем спрайт из текстуры
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        sprite.name = "name";
        //string targetPathh = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Images" + "/" + sprite.name + ".png";
        //byte[] bytes = texture.EncodeToPNG();
        //System.IO.File.WriteAllBytes(targetPathh, bytes);

        //#if UNITY_EDITOR
        //        AssetDatabase.CreateAsset(sprite, targetPathh);
        //            AssetDatabase.SaveAssets();
        //        #endif
        Debug.Log("ИДИ НАХУЙ");
        return sprite;
    }
}