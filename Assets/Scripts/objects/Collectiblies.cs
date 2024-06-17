using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectiblies : MonoBehaviour
{
    public bool Money;
    public bool Health;
    public bool Exp,Mana,Speed, normalResource;
    public int price;
    public int id;
    public GameObject obj;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (Money)
            {
                UIHandler.instance.SetCountValue(price);
            }
            if(Exp)
            {
                UIHandler.instance.SetCountLevelValue(price);
                
            }
            if (normalResource)
            {
                obj = GameObject.Find("Main Camera");
                Item item = obj.GetComponent<DataBase>().items[id];
                obj.GetComponent<Inventory>().SearchForSameItem(item, 1);
            }
            Destroy(gameObject);
        }
    }

}
