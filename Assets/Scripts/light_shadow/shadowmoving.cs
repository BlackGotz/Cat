using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowmoving : MonoBehaviour
{
    public GameObject go;
    public SpriteRenderer _sprite;
    private Vector3 scale; // Переменная для текущего размера
    private Vector3 newscale;
    private Sun timesun;
    public float time, maxa,speed,angle;
    float intensity,y,maxy,x,minx;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z); // Проверяем текущий размер
        newscale = scale;
        GameObject timeday = GameObject.Find("Sun");
        timesun = timeday.GetComponent<Sun>();
        speed = timesun.speed;
        y = scale.y;
        maxy = y * 2;
        x = scale.x;
        minx = x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        time = timesun.time;
        if (time <= 24)               //Счетчик времени
            time += speed * Time.deltaTime;
        else
            time = 0;


        if ((time <= 4) || (time >= 22.4f))  //Ночное время
        {
            intensity = 0;
        }
        else if ((time >= 8.4f) && (time <= 18))   //День
        {
            intensity = 1;
        }
        else if ((time > 4) && (time < 8.4f))    //Рассвет
        {
            intensity = (time - 4) / 4.4f;
        }
        else if ((time > 18) && (time < 22.4))   //Закат
        {
            intensity = (22.4f - time) / 4.4f;
        }
        intensity = Mathf.Clamp(intensity, 0, 1);

        var color = _sprite.color;
        color.a = intensity * maxa;
        color.a = Mathf.Clamp(color.a, 0, maxa);
        _sprite.color = color;

    }

    void FixedUpdate()
    {
        if (((time <= 1.2f)&&(time>=0))||(time>22.4f))
        {
            float timer=time;
            if (time>22.4f)
            {
                timer = time-22.4f;
            }
            else if (time<=1.2f)
            {
                timer = 1.6f + time;
            }
            newscale = new Vector3(x - minx* (2.8f - timer) / 2.8f, y+y*(2.8f-timer)/2.8f, 0);
            angle = 0+90*(2.8f-timer)/2.8f;
        }
        else if ((time <= 22.4f)&&(time>13.2f))
        {
            newscale = new Vector3(x - minx * (time - 13.2f) / 9.2f, y+y*(time-13.2f)/9.2f, 0f);
            angle = 180-90* (time - 13.2f) / 9.2f;
        }
        else if ((time <= 13.2f)&& (time > 4))
        {
            newscale = new Vector3(minx + minx * (time - 4) / 9.2f, maxy-y*(time-4)/9.2f, 0f);
            angle = 270 - 90 * (time - 4) / 9.2f;
        }
        else if ((time <= 4) && (time > 1.2f))
        {
            newscale = new Vector3(x - minx * (time - 1.2f) / 2.8f, y+y*(time-1.2f)/2.8f, 0f);
            angle = 360 - 90 * (time - 1.2f) / 2.8f;
        }
        go.transform.localScale = newscale;
        go.transform.localEulerAngles = new Vector3(20, 0, angle);
    }
}
