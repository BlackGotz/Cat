using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    private VisualElement m_Healthbar;
    private VisualElement m_Manabar;
    private VisualElement m_Speedbar;
    private VisualElement m_Levelbar;
    private Label m_Count;
    private Label m_LevelCount;
    private Label m_LevelCurrent;
    public static UIHandler instance { get; private set; }
    public int money=0;
    public int level = 0;
    public int currentlevel = 0;
    public int maxlevel = 20;


    // Awake is called when the script instance is being loaded (in this situation, when the game scene loads)
    private void Awake()
    {
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        
        if (Data.first == false)
        {
            currentlevel = Data.currentLevel;
            level = Data.levelCount;
            maxlevel = currentlevel * 5 + 20;
            money = Data.money;
        }
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        m_Manabar = uiDocument.rootVisualElement.Q<VisualElement>("ManaBar");
        m_Speedbar = uiDocument.rootVisualElement.Q<VisualElement>("SpeedBar");
        m_Levelbar = uiDocument.rootVisualElement.Q<VisualElement>("LevelBar");
        m_Count = uiDocument.rootVisualElement.Q<Label>("CountMoney");
        m_LevelCount = uiDocument.rootVisualElement.Q<Label>("Level");
        m_LevelCurrent = uiDocument.rootVisualElement.Q<Label>("CurrentLevel");

        SetHealthValue(1.0f);
        SetManaValue(1.0f);
        SetSpeedValue(1.0f);
        SetLevelValue(1.0f);
        SetCountValue(0);
        SetCountLevelValue(0);
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
    public void SetManaValue(float percentage)
    {
        m_Manabar.style.width = Length.Percent(100 * percentage);
    }
    public void SetSpeedValue(float percentage)
    {
        m_Speedbar.style.width = Length.Percent(100 * percentage);
    }
    public void SetLevelValue(float percentage)
    {
        m_Levelbar.style.width = Length.Percent(100 * percentage);
    }
    public void SetCountValue(int count)
    {
        money += count;
        m_Count.text="x " + money.ToString();
    }
    public void SetCountLevelValue(int count)
    {
        level += count;
        if(level>=maxlevel)
        {
            level= level-maxlevel;
            currentlevel += 1;
            maxlevel = currentlevel*5+20;
        }
        m_LevelCount.text = level.ToString()+"/"+maxlevel;
        m_LevelCurrent.text = "Lv. " + currentlevel.ToString();
        UIHandler.instance.SetLevelValue(level / (float)maxlevel);
    }
}
