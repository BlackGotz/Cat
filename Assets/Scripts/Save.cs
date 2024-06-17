using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Save : MonoBehaviour
{
    public static int currentHealth, currentMana, currentLevel, levelCount, money;
    public static float currentSpeed, time;
    public static bool first = true;
    public static List<ItemInventory> items;
    public static int length = 30;
    public GameObject obj, sun, ui;

    private void Start()
    {
        LoadGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveGame();
            Application.Quit();
        }
    }

    public void getData()
    {
        currentMana = obj.GetComponent<MCcontroller>().currentMana;
        currentSpeed = obj.GetComponent<MCcontroller>().currentSpeed;
        currentHealth = obj.GetComponent<MCcontroller>().currentHealth;
        time = sun.GetComponent<Sun>().time;
        money = UIHandler.instance.money;
        levelCount = UIHandler.instance.level;
        currentLevel = UIHandler.instance.currentlevel;
        items = GameObject.Find("Main Camera").GetComponent<Inventory>().items;
    }
    public void SaveGame()
    {
        ResetData();
        getData();
        PlayerPrefs.SetInt("Health", currentHealth);
        PlayerPrefs.SetInt("Mana", currentMana);
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetInt("LevelCount", levelCount);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetFloat("CurrentSpeed", currentSpeed);
        PlayerPrefs.SetFloat("Time", time);
        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetInt("items_" + i +"id", items[i].id);
            PlayerPrefs.SetInt("items_" + i + "count", items[i].count);
        }
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete");
    }
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            obj.GetComponent<MCcontroller>().currentHealth = PlayerPrefs.GetInt("Health");
            obj.GetComponent<MCcontroller>().currentMana =PlayerPrefs.GetInt("Mana");
            UIHandler.instance.currentlevel = PlayerPrefs.GetInt("Level");
            UIHandler.instance.level = PlayerPrefs.GetInt("LevelCount");
            UIHandler.instance.money = PlayerPrefs.GetInt("Money");
            obj.GetComponent<MCcontroller>().currentSpeed= PlayerPrefs.GetFloat("CurrentSpeed");
            sun.GetComponent<Sun>().time = PlayerPrefs.GetFloat("Time");
            for (int i = 0; i < 30; i++)
            {
                items[i].id=PlayerPrefs.GetInt("items_" + i + "id");
                items[i].count = PlayerPrefs.GetInt("items_" + i + "count");
                Data.items[i].id = PlayerPrefs.GetInt("items_" + i + "id");
                Data.items[i].count = PlayerPrefs.GetInt("items_" + i + "count");
                Item item = obj.GetComponent<DataBase>().items[i];
                obj.GetComponent<Inventory>().SearchForSameItem(item, items[i].count);
            }
            GameObject.Find("Main Camera").GetComponent<Inventory>().items=items;
            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
