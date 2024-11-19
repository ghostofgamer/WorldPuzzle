using System;
using UnityEngine;
using UnityEngine.UI;

public class TestDateTime : MonoBehaviour
{
    public Button[] buttons;
    private int currentStreak = 0;
    private int maxStreak = 4;
    private DateTime[] lastPressTimes;

    void Start()
    {
        lastPressTimes = new DateTime[maxStreak];

        if (PlayerPrefs.HasKey("CurrentStreak"))
        {
            currentStreak = PlayerPrefs.GetInt("CurrentStreak");
            for (int i = 0; i < maxStreak; i++)
            {
                string key = "LastPressTime_" + i;
                if (PlayerPrefs.HasKey(key))
                {
                    string lastPressTimeString = PlayerPrefs.GetString(key);
                    lastPressTimes[i] = DateTime.Parse(lastPressTimeString);
                }
                else
                {
                    lastPressTimes[i] = DateTime.MinValue;
                }
            }
        }
        else
        {
            currentStreak = 0;
            for (int i = 0; i < maxStreak; i++)
            {
                lastPressTimes[i] = DateTime.MinValue;
            }
        }

        CheckButtonAvailability();

        for (int i = 0; i < maxStreak; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void Update()
    {
        string timing = PlayerPrefs.GetString("LastPressTime_");

        if (!string.IsNullOrEmpty(timing))
        {
            DateTime tim;
            if (DateTime.TryParse(timing, out tim))
            {
                // Проверка, что время корректно
                if (DateTime.Now - tim >= TimeSpan.FromHours(24))
                {
                    CheckButtonAvailability();
                }

                Debug.Log(DateTime.Now - tim);
            }
            else
            {
                Debug.LogError("Не удалось преобразовать строку в DateTime: " + timing);
            }
        }


        /*// Проверяем, какие кнопки доступны
        // CheckButtonAvailability();

        string timing = PlayerPrefs.GetString("LastPressTime_");
        DateTime tim = DateTime.Parse(timing);

        // if (DateTime.Now - tim >= TimeSpan.FromHours(24))
        if (DateTime.Now - tim >= TimeSpan.FromSeconds(3))
            CheckButtonAvailability();

        Debug.Log(DateTime.Now - tim);*/
    }

    void CheckButtonAvailability()
    {
        string key = "LastPressTime_";

        if (PlayerPrefs.HasKey(key))
        {
            string timing = PlayerPrefs.GetString("LastPressTime_");
            DateTime tim;

            for (int i = 0; i < maxStreak; i++)
            {
                if (DateTime.TryParse(timing, out tim))
                {
                    if (i == currentStreak && DateTime.Now - tim >= TimeSpan.FromHours(24))
                    {
                        Debug.Log("CURRENTSTREAK " + currentStreak);
                        buttons[i].interactable = true;
                    }
                    else
                    {
                        Debug.Log("ELSE " + currentStreak);
                        buttons[i].interactable = false;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].interactable = false;

            buttons[0].interactable = true;
        }


        /*for (int i = 0; i < maxStreak; i++)
        {
            if (i == currentStreak && DateTime.Now - lastPressTimes[i] >= TimeSpan.FromHours(24))
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }*/
    }

    void OnButtonClick(int buttonIndex)
    {
        lastPressTimes[buttonIndex] = DateTime.Now;
        // PlayerPrefs.SetString("LastPressTime_" + buttonIndex, lastPressTimes[buttonIndex].ToString());
        PlayerPrefs.SetString("LastPressTime_", DateTime.Now.ToString());
        PlayerPrefs.Save();

        currentStreak++;
        if (currentStreak >= maxStreak)
        {
            currentStreak = 0;
        }

        PlayerPrefs.SetInt("CurrentStreak", currentStreak);
        PlayerPrefs.Save();
        buttons[buttonIndex].interactable = false;
    }
}