using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CreateNewSlots : MonoBehaviour
{
    public GameObject prefab; // Ссылка на префаб, который нужно создать
    public Transform contentPanel; // Ссылка на панель Content, куда нужно добавить префабы
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;

    public GameObject FastSlot1;
    public GameObject FastSlot2;
    public GameObject FastSlot3;
    public GameObject FastSlot4;

    public InventorySlots FastSlot1Script;
    public InventorySlots FastSlot2Script;
    public InventorySlots FastSlot3Script;
    public InventorySlots FastSlot4Script;

    public int CountSpell = 2;

    public Transform InventoryPanel;
    public InventoryManager inventoryManager;

    public ChestPickUp chestPickUp;


    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        chestPickUp = FindObjectOfType<ChestPickUp>();

        //ImaheOnScenTest = GetComponent<Image>();

        // Находим кнопку на сцене и добавляем метод-обработчик нажатия на нее
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CreatePrefabOnButtonClick);

        FastSlot1 = GameObject.Find("FastSlot1");
        FastSlot2 = GameObject.Find("FastSlot2");
        FastSlot3 = GameObject.Find("FastSlot3");
        FastSlot4 = GameObject.Find("FastSlot4");

        FastSlot1Script = FastSlot1.GetComponent<InventorySlots>();
        FastSlot2Script = FastSlot2.GetComponent<InventorySlots>();
        FastSlot3Script = FastSlot3.GetComponent<InventorySlots>();
        FastSlot4Script = FastSlot4.GetComponent<InventorySlots>();
    }

    private void CreatePrefabOnButtonClick()
    {
        InventorySlots slot1Script = Slot1.GetComponent<InventorySlots>();
        InventorySlots slot2Script = Slot2.GetComponent<InventorySlots>();
        InventorySlots slot3Script = Slot3.GetComponent<InventorySlots>();



        bool isEmptySlot1 = slot1Script.isEmpty;
        bool isEmptySlot2 = slot2Script.isEmpty;
        bool isEmptySlot3 = slot3Script.isEmpty;

        ItemScriptableObject ItemSlot1 = slot1Script.Item;
        ItemScriptableObject ItemSlot2 = slot2Script.Item;
        ItemScriptableObject ItemSlot3 = slot3Script.Item;

        //Debug.Log("пупупу");

        if (!isEmptySlot1 && !isEmptySlot2 && !isEmptySlot3) // если все слоты заполнены
        {
            CreateSpell(ItemSlot1.Temperature, ItemSlot1.Force, ItemSlot1.Amperage, ItemSlot1.Gravity, ItemSlot1.Light,
                    ItemSlot2.Temperature, ItemSlot2.Force, ItemSlot2.Amperage, ItemSlot2.Gravity, ItemSlot2.Light,
                    ItemSlot3.Temperature, ItemSlot3.Force, ItemSlot3.Amperage, ItemSlot3.Gravity, ItemSlot3.Light);            
        }
        else if (!isEmptySlot1 && !isEmptySlot2 && isEmptySlot3) // если 1 и 2 слоты заполнены
        {
            CreateSpell(ItemSlot1.Temperature, ItemSlot1.Force, ItemSlot1.Amperage, ItemSlot1.Gravity, ItemSlot1.Light,
                        ItemSlot2.Temperature, ItemSlot2.Force, ItemSlot2.Amperage, ItemSlot2.Gravity, ItemSlot2.Light,
                        0, 0, 0, 0, 0);
        }
        else if (!isEmptySlot1 && isEmptySlot2 && !isEmptySlot3) // если 1 и 3 слоты заполнены
        {
            CreateSpell(ItemSlot1.Temperature, ItemSlot1.Force, ItemSlot1.Amperage, ItemSlot1.Gravity, ItemSlot1.Light,
                        0, 0, 0, 0, 0,
                        ItemSlot3.Temperature, ItemSlot3.Force, ItemSlot3.Amperage, ItemSlot3.Gravity, ItemSlot3.Light);
            
        }
        else if (isEmptySlot1 && !isEmptySlot2 && !isEmptySlot3) // если 2 и 3 слоты заполнены
        {
            CreateSpell(0, 0, 0, 0, 0,
                        ItemSlot2.Temperature, ItemSlot2.Force, ItemSlot2.Amperage, ItemSlot2.Gravity, ItemSlot2.Light,
                        ItemSlot3.Temperature, ItemSlot3.Force, ItemSlot3.Amperage, ItemSlot3.Gravity, ItemSlot3.Light);
            
        }
        else // в остальных случаях
        {
            // Действия, когда необходимо выполнить другую логику
        }
    }

    public void CreateSpell(int Temperature_Slot1, int Force_Slot1, int Amperage_Slot1, int Gravity_Slot1, int light_Slot1,
                            int Temperature_Slot2, int Force_Slot2, int Amperage_Slot2, int Gravity_Slot2, int light_Slot2,
                            int Temperature_Slot3, int Force_Slot3, int Amperage_Slot3, int Gravity_Slot3, int light_Slot3)
    {
        

        SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();

        // Настройка свойств SpellItem 
        spellItem.Temperature = Temperature_Slot1 + Temperature_Slot2 - Temperature_Slot3;
        spellItem.Force = Force_Slot1 + Force_Slot2 - Force_Slot3;
        spellItem.Amperage = Amperage_Slot1 + Amperage_Slot2 - Amperage_Slot3;
        spellItem.Gravity = Gravity_Slot1 + Gravity_Slot2 - Gravity_Slot3;
        spellItem.Light = light_Slot1 + light_Slot2 - light_Slot3;



        if(!CheckInventory(spellItem) && chestPickUp.scrollCount > 0)
        {
            CountSpell++;
            chestPickUp.scrollCount--;
            chestPickUp.UpdateScrollCountUI();

            spellItem.Icon = GenerateTexture();
            spellItem.ItemName = CountSpell;

            string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/" + CountSpell + ".asset";

            #if UNITY_EDITOR
                AssetDatabase.CreateAsset(spellItem, targetPath);
                AssetDatabase.SaveAssets();
            #endif

            inventoryManager.Inicialization();

            //Instantiate(prefab, contentPanel); // Создаем префаб как дочерний элемент панели Content
            AddItem(spellItem);
        }
        else
        {
            Debug.Log("I dont have enough scrolls!");
        }
    }      

    public bool CheckInventory(ItemScriptableObject _item)
    {
        foreach (InventorySlots slot in inventoryManager.Slots)
        {  
            if (!slot.isEmpty)
            {
                
                if (slot.Item.Temperature == _item.Temperature &&
                    slot.Item.Force == _item.Force &&
                    slot.Item.Amperage == _item.Amperage &&
                    slot.Item.Gravity == _item.Gravity &&
                    slot.Item.Light == _item.Light)
                {    
                    return true;
                    
                }
            }
        }


        if (!FastSlot1Script.isEmpty)
        {
            
            if (FastSlot1Script.Item.Temperature == _item.Temperature &&
                FastSlot1Script.Item.Force == _item.Force &&
                FastSlot1Script.Item.Amperage == _item.Amperage &&
                FastSlot1Script.Item.Gravity == _item.Gravity &&
                FastSlot1Script.Item.Light == _item.Light)
            {    
                return true;
            }
        }

        if (!FastSlot2Script.isEmpty)
        {
            
            if (FastSlot2Script.Item.Temperature == _item.Temperature &&
                FastSlot2Script.Item.Force == _item.Force &&
                FastSlot2Script.Item.Amperage == _item.Amperage &&
                FastSlot2Script.Item.Gravity == _item.Gravity &&
                FastSlot2Script.Item.Light == _item.Light)
            {    
                return true;
            }
        }

        if (!FastSlot3Script.isEmpty)
        {
            
            if (FastSlot3Script.Item.Temperature == _item.Temperature &&
                FastSlot3Script.Item.Force == _item.Force &&
                FastSlot3Script.Item.Amperage == _item.Amperage &&
                FastSlot3Script.Item.Gravity == _item.Gravity &&
                FastSlot3Script.Item.Light == _item.Light)
            {    
                return true;
            }
        }

        if (!FastSlot4Script.isEmpty)
        {
            
            if (FastSlot4Script.Item.Temperature == _item.Temperature &&
                FastSlot4Script.Item.Force == _item.Force &&
                FastSlot4Script.Item.Amperage == _item.Amperage &&
                FastSlot4Script.Item.Gravity == _item.Gravity &&
                FastSlot4Script.Item.Light == _item.Light)
            {    
                return true;
            }
        }

        return false;
    }

    public void AddItem(ItemScriptableObject _item)
    {
        Instantiate(prefab, contentPanel); // Создаем префаб как дочерний элемент панели Content
        foreach (InventorySlots slot in inventoryManager.Slots)
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

        return sprite;
    }
}