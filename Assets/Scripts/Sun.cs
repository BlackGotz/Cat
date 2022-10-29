using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float time;
    public Light sunlight;
    float intensity;

    public Color color1, color2;
    Color rescolor;
    // Start is called before the first frame update
    void Start()
    {
        sunlight = GetComponent<Light>();
        intensity = sunlight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if ((time <= 4) || (time >= 22.4f))
        {
            sunlight.intensity = 0;
            rescolor = color1;
        }
        else if ((time >= 8.4f) && (time <= 18))
        {
            sunlight.intensity = 1;
            rescolor = color2;
        }
        else if ((time > 4) && (time < 8.4f))
        {
            sunlight.intensity = (time - 4) / 4.4f;
            
            rescolor = new Color((color2.r - color1.r) * (time - 4) / 4.4f + color1.r, (color2.g - color1.g) * (time - 4) / 4.4f + color1.g, (color2.b - color1.b) * (time - 4) / 4.4f + color1.b);
        }
        else if ((time > 18) && (time < 22.4))
        {
            sunlight.intensity = (22.4f - time) / 4.4f;
            rescolor = new Color((color1.r - color2.r) * (time-18) / 4.4f + color2.r, (color1.g - color2.g) * (time-18) / 4.4f + color2.g, (color1.b - color2.b) * (time-18) / 4.4f + color2.b);
        }
        intensity = Mathf.Clamp(intensity, 0, 1);

        if (time <= 24)
            time += 0.002f * Time.deltaTime;
        else
            time = 0;
        sunlight.color = rescolor;
    }

}
