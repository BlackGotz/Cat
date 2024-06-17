using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTransparency : MonoBehaviour
{
    public SpriteRenderer _sprite;
    bool ObjectsOnTrigger;
    public float speed;

    void FixedUpdate()
    {
        var color = _sprite.color;
        var mat = GetComponent<SpriteRenderer>().material;
        var Mat = mat.color;

        if (ObjectsOnTrigger)
        {
            color.a -= speed * Time.deltaTime;
            Mat.a -= speed * Time.deltaTime;
        }
        else
        {
            color.a += speed * Time.deltaTime;
            Mat.a += speed * Time.deltaTime;
        }
        color.a = Mathf.Clamp(color.a, 0.05f, 1);
        Mat.a = Mathf.Clamp(Mat.a, 0.05f, 1);

        _sprite.color = color;
        mat.color = color;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer==8)
        {

            ObjectsOnTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            ObjectsOnTrigger = false;
        }
    }
}
