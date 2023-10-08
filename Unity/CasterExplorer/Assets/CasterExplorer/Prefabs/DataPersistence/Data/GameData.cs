using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int currentHealth;
    public int inventoryCount;
    public PlayerHealth playerHealthScript;
    public float playerHealthFillAmount;
    public string MagisteriyaCount;
    public Vector3 playerPosition;
    public MagisteriyaFruitPickUp MagisterutaFruitPickUp;

    public SerializableDictionary<string, bool> magisteriyaCollected;

    public GameData()
    {
        playerPosition = Vector3.zero;
        this.inventoryCount = 0;
        this.MagisteriyaCount = inventoryCount.ToString();
        this.playerHealthFillAmount = 1f;
        this.currentHealth = 100;
        magisteriyaCollected = new SerializableDictionary<string, bool>();
    }

    public int GetLevelInfo() // информация которая будет находиться в слоте сохранения
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene number: " + sceneNumber);
        return sceneNumber;
    }

}