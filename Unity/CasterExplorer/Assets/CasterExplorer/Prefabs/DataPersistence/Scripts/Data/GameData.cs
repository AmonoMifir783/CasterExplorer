using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int currentHealth;
    public int inventoryCount;
    public PlayerHealth playerHealthScript;
    public PlayerStamina playerStaminaScript;
    public float playerHealthFillAmount;
    public float playerStaminaFillAmount;
    public string MagisteriyaCount;
    public Vector3 playerPosition;
    public MagisteriyaFruitPickUp MagisterutaFruitPickUp;
    public DateTime currentDateTime;
    public int maxHealth;
    public List<int> SlotsSave;
    public int scrollCount;
    public string ScrollCount;
    public int currentStamina;
    public int maxStamina;

    public List<string> spellNames;


    public SerializableDictionary<string, bool> magisteriyaCollected;
    public SerializableDictionary<string, bool> deadSalamandras;
    public SerializableDictionary<string, bool> deadBards;
    public SerializableDictionary<string, bool> pickedChests;
    public SerializableDictionary<string, bool> pickedFruits;
    public SerializableDictionary<string, bool> saperNumbers;

    public GameData()
    {
        this.maxStamina = 100;
        this.maxHealth = 100;
        playerPosition = Vector3.one;
        this.inventoryCount = 0;
        this.MagisteriyaCount = inventoryCount.ToString();
        this.scrollCount = 5;
        this.ScrollCount = scrollCount.ToString();
        this.playerHealthFillAmount = 0f;
        this.playerStaminaFillAmount = 0f;
        this.currentHealth = 100;
        this.currentStamina = 100;
        magisteriyaCollected = new SerializableDictionary<string, bool>();
        deadSalamandras = new SerializableDictionary<string, bool>();
        deadBards = new SerializableDictionary<string, bool>();
        pickedChests = new SerializableDictionary<string, bool>();
        pickedFruits = new SerializableDictionary<string, bool>();
        saperNumbers = new SerializableDictionary<string, bool>();
        SlotsSave = new List<int>();
        spellNames = new List<string>();
    }

    public int GetLevelInfo() // информаци€ котора€ будет находитьс€ в слоте сохранени€
    {

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SceneNumber", sceneNumber);
        return sceneNumber;
    }
    public DateTime GetTimeInfo() // информаци€, котора€ будет находитьс€ в слоте сохранени€
    {
        DateTime currentDateTime = DateTime.Now;
        return currentDateTime;
    }

    public void SaveSpellNames(List<string> names)
    {
        spellNames.Clear();
        spellNames.AddRange(names);
    }

    // ћетод дл€ загрузки имен заклинаний
    public List<string> LoadSpellNames()
    {
        return spellNames;
    }

}