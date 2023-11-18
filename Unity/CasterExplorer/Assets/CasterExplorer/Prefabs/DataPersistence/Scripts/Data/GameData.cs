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


    public SerializableDictionary<string, bool> magisteriyaCollected;
    public SerializableDictionary<string, bool> deadSalamandras;
    public SerializableDictionary<string, bool> pickedChests;
    public SerializableDictionary<string, bool> pickedFruits;

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
        pickedChests = new SerializableDictionary<string, bool>();
        pickedFruits = new SerializableDictionary<string, bool>();
        SlotsSave = new List<int>();

    }

    public int GetLevelInfo() // информация которая будет находиться в слоте сохранения
    {

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SceneNumber", sceneNumber);
        return sceneNumber;
    }
    public DateTime GetTimeInfo() // информация, которая будет находиться в слоте сохранения
    {
        DateTime currentDateTime = DateTime.Now;
        return currentDateTime;
    }

}