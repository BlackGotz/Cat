using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTransparency : MonoBehaviour
{
    public SpriteRenderer _sprite;
    bool ObjectsOnTrigger;
    public float speed;

    void Update()
    {
        var color = _sprite.color;
        if (ObjectsOnTrigger)
            color.a -= speed * Time.deltaTime;
        else
            color.a += speed * Time.deltaTime;
        color.a = Mathf.Clamp(color.a, 0.2f, 1);
        _sprite.color = color;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        ObjectsOnTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ObjectsOnTrigger = false;
    }
}
