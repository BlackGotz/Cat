using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public string sceneName;
    public GameObject obj,sun,ui;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                LoadData();
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void LoadData()
    {
        Data.currentMana=obj.GetComponent<MCcontroller>().currentMana;
        Data.currentSpeed = obj.GetComponent<MCcontroller>().currentSpeed;
        Data.currentHealth = obj.GetComponent<MCcontroller>().currentHealth;
        Data.time = sun.GetComponent<Sun>().time;
        Data.money = UIHandler.instance.money;
        Data.levelCount = UIHandler.instance.level;
        Data.currentLevel = UIHandler.instance.currentlevel;
        Data.first = false;
        Data.items = GameObject.Find("Main Camera").GetComponent<Inventory>().items;
    }
}

