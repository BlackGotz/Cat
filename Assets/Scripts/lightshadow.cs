using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightshadow : MonoBehaviour
{
    Collider closest, cl1, cl2;
    public LayerMask itemsLayer;
    bool LightsOnTrigger;
    public SpriteRenderer _sprite, _sprite2, _sprite3;
    private Vector3 scale; // Переменная для текущего размера
    private Vector3 newscale;
    private Vector3 position, diff;
    private float distance, k, scaleparent, scalelight, y, intensity;
    private Light sunlight;
    public float angle, radius, ang, h, l;


    void Start()
    {
        scaleparent = transform.localScale.z * l * Mathf.Sin(20 * Mathf.PI / 180); // Проверяем текущий размер самого объекта
        y = transform.localScale.y;
        GameObject timeday = GameObject.Find("Sun");
        sunlight = timeday.GetComponent<Light>();
    }
    void Update()
    {
        intensity = sunlight.intensity;
        Collider[] findedItems = Physics.OverlapSphere(transform.position, radius, itemsLayer);
        k = findedItems.Length;
        if (findedItems.Length > 0)
        {
            position = transform.position;
            closest = findedItems[0];
            if (k > 1)
            {
                cl1 = findedItems[1];
                if (k > 2)
                { cl2 = findedItems[2]; }
                else { cl2 = null; }
            }
            else
            {
                cl1 = null;
                cl2 = null;
            }
            LightsOnTrigger = true;
        }
        else
        {
            LightsOnTrigger = false;
        }
    }

    void Shadow(Collider cl, float f, SpriteRenderer _sprite)
    {
        _sprite.gameObject.SetActive(true);
        scale = new Vector3(_sprite.transform.localScale.x, _sprite.transform.localScale.y, _sprite.transform.localScale.z);
        newscale = scale;

        var color = _sprite.color;
        position = _sprite.transform.position;
        diff = cl.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(position.x - diff.x, 2) + Mathf.Pow(position.y - diff.y, 2));
        scalelight = Mathf.Abs(diff.z);
        Debug.Log(scaleparent);
        if (scalelight > scaleparent)        // Изменение размера тени в зависимости где находится источник света
        {
            newscale = new Vector3(scale.x, scalelight * distance / Mathf.Abs(scalelight - scaleparent) / 5, 0f);
        }
        else
        {
            newscale = new Vector3(scale.x, y * h, 0f);
        }

        ang = (position.x - diff.x) / (position.y - diff.y);
        if (position.y - diff.y > 0)
        {
            angle = -Mathf.Atan(ang) * 180 / Mathf.PI;
        }
        else
        {
            angle = 180 - Mathf.Atan(ang) * 180 / Mathf.PI;
        }
        _sprite.transform.localScale = newscale;
        _sprite.transform.localEulerAngles = new Vector3(20, 0, angle);
        color.a = (radius - distance) * 0.4f * f*(1.2f-intensity);
        color.a = Mathf.Clamp(color.a, 0, 0.4f);
        _sprite.color = color;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (LightsOnTrigger)
        {
            if (k == 1)
            {
                Shadow(closest, 1, _sprite);
                _sprite2.gameObject.SetActive(false);
                _sprite3.gameObject.SetActive(false);

            }
            else if (k == 2)
            {
                Shadow(closest, 0.9f, _sprite);
                Shadow(cl1, 0.9f, _sprite2);
                _sprite3.gameObject.SetActive(false);

            }
            else if (k >= 3)
            {
                Shadow(closest, 0.8f, _sprite);
                Shadow(cl1, 0.8f, _sprite2);
                Shadow(cl2, 0.8f, _sprite3);
            }
        }
        else
        {
            _sprite.gameObject.SetActive(false);
            _sprite2.gameObject.SetActive(false);
            _sprite3.gameObject.SetActive(false);
        }
    }

}
