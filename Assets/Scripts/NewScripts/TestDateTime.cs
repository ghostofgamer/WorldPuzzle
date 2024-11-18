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
        // Инициализация массива времени последнего нажатия
        lastPressTimes = new DateTime[maxStreak];

        // Проверяем, есть ли сохраненное состояние стрика и времени последнего нажатия
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

        // Проверяем, какие кнопки доступны
        CheckButtonAvailability();

        // Подписываемся на событие нажатия кнопок
        for (int i = 0; i < maxStreak; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void Update()
    {
        string timing = PlayerPrefs.GetString("LastPressTime_");

        // Проверка, что строка не пустая и может быть преобразована в DateTime
        if (!string.IsNullOrEmpty(timing))
        {
            DateTime tim;
            if (DateTime.TryParse(timing, out tim))
            {
                // Проверка, что время корректно
                if (DateTime.Now - tim >= TimeSpan.FromSeconds(3))
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
        for (int i = 0; i < maxStreak; i++)
        {
            // if (i == currentStreak && DateTime.Now - lastPressTimes[i] >= TimeSpan.FromHours(24))
            if (i == currentStreak && DateTime.Now - lastPressTimes[i] >= TimeSpan.FromSeconds(3))
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        lastPressTimes[buttonIndex] = DateTime.Now;
        // PlayerPrefs.SetString("LastPressTime_" + buttonIndex, lastPressTimes[buttonIndex].ToString());
        PlayerPrefs.SetString("LastPressTime_", DateTime.Now.ToString());
        PlayerPrefs.Save();

        // Увеличиваем текущий стрик и сохраняем его в PlayerPrefs
        currentStreak++;
        if (currentStreak >= maxStreak)
        {
            currentStreak = 0;
        }

        PlayerPrefs.SetInt("CurrentStreak", currentStreak);
        PlayerPrefs.Save();

        // Обновляем доступность кнопок
        buttons[buttonIndex].interactable = false;

        // CheckButtonAvailability();
    }


    /*public Button[] buttons;
  private int currentStreak = 0;
  private int maxStreak = 4;
  private DateTime[] lastPressTimes;
  private bool isTimerRunning = false;

  void Start()
  {
      // Инициализация массива времени последнего нажатия
      lastPressTimes = new DateTime[maxStreak];

      // Проверяем, есть ли сохраненное состояние стрика и времени последнего нажатия
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

      // Проверяем, какие кнопки доступны
      CheckButtonAvailability();

      // Подписываемся на событие нажатия кнопок
      for (int i = 0; i < maxStreak; i++)
      {
          int buttonIndex = i;
          buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
      }
  }

  void CheckButtonAvailability()
  {
      for (int i = 0; i < maxStreak; i++)
      {
          if (i == currentStreak && DateTime.Now - lastPressTimes[i] >= TimeSpan.FromSeconds(5))
          {
              buttons[i].interactable = true;
          }
          else
          {
              buttons[i].interactable = false;
          }
      }

      if (!isTimerRunning)
      {
          StartCoroutine(StartTimer());
      }
  }

  void OnButtonClick(int buttonIndex)
  {
      lastPressTimes[buttonIndex] = DateTime.Now;
      PlayerPrefs.SetString("LastPressTime_" + buttonIndex, lastPressTimes[buttonIndex].ToString());
      PlayerPrefs.Save();

      // Обновляем доступность кнопок
      CheckButtonAvailability();

      // Увеличиваем текущий стрик и сохраняем его в PlayerPrefs через 5 секунд
      StartCoroutine(IncreaseStreakAfterDelay(buttonIndex));
  }

  IEnumerator IncreaseStreakAfterDelay(int buttonIndex)
  {
      yield return new WaitForSeconds(5);

      currentStreak++;
      if (currentStreak >= maxStreak)
      {
          currentStreak = 0;
      }
      PlayerPrefs.SetInt("CurrentStreak", currentStreak);
      PlayerPrefs.Save();

      // Обновляем доступность кнопок
      CheckButtonAvailability();
  }

  IEnumerator StartTimer()
  {
      isTimerRunning = true;
      while (true)
      {
          for (int i = 0; i < maxStreak; i++)
          {
              TimeSpan remainingTime = TimeSpan.FromSeconds(5) - (DateTime.Now - lastPressTimes[i]);
              if (remainingTime > TimeSpan.Zero)
              {
                  Debug.Log("Button " + i + " remaining time: " + remainingTime);
              }
          }
          yield return new WaitForSeconds(1);
      }
  }*/


    /*public Button[] buttons;
    private int currentStreak = 0;
    private int maxStreak = 4;
    private DateTime[] lastPressTimes;
    private bool isTimerRunning = false;

    void Start()
    {
        // Инициализация массива времени последнего нажатия
        lastPressTimes = new DateTime[maxStreak];

        // Проверяем, есть ли сохраненное состояние стрика и времени последнего нажатия
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

        // Проверяем, какие кнопки доступны
        CheckButtonAvailability();

        // Подписываемся на событие нажатия кнопок
        for (int i = 0; i < maxStreak; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void CheckButtonAvailability()
    {
        for (int i = 0; i < maxStreak; i++)
        {
            if (i == currentStreak && DateTime.Now - lastPressTimes[i] >= TimeSpan.FromHours(24))
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }

        if (!isTimerRunning)
        {
            StartCoroutine(StartTimer());
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        lastPressTimes[buttonIndex] = DateTime.Now;
        PlayerPrefs.SetString("LastPressTime_" + buttonIndex, lastPressTimes[buttonIndex].ToString());
        PlayerPrefs.Save();

        // Обновляем доступность кнопок
        CheckButtonAvailability();
    }

    IEnumerator StartTimer()
    {
        isTimerRunning = true;
        while (true)
        {
            for (int i = 0; i < maxStreak; i++)
            {
                TimeSpan remainingTime = TimeSpan.FromHours(24) - (DateTime.Now - lastPressTimes[i]);
                if (remainingTime > TimeSpan.Zero)
                {
                    Debug.Log("Button " + i + " remaining time: " + remainingTime);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }*/


    /*public Button button;
    private DateTime lastPressTime;
    private bool isTimerRunning = false;


    void Start()
    {
        // Проверяем, есть ли сохраненное время последнего нажатия
        if (PlayerPrefs.HasKey("LastPressTime"))
        {
            string lastPressTimeString = PlayerPrefs.GetString("LastPressTime");
            lastPressTime = DateTime.Parse(lastPressTimeString);
        }
        else
        {
            lastPressTime = DateTime.MinValue;
        }

        // Проверяем, можно ли нажать кнопку
        CheckButtonAvailability();

        // Подписываемся на событие нажатия кнопки
        button.onClick.AddListener(OnButtonClick);
    }

    void CheckButtonAvailability()
    {
        Debug.Log("ЕРУ ИЛИ ФОЛС " + (DateTime.Now - lastPressTime >= TimeSpan.FromHours(24)));

        if (DateTime.Now - lastPressTime >= TimeSpan.FromHours(24))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            if (!isTimerRunning)
            {
                StartCoroutine(StartTimer());
            }
        }
    }

    void OnButtonClick()
    {
        lastPressTime = DateTime.Now;
        PlayerPrefs.SetString("LastPressTime", lastPressTime.ToString());
        PlayerPrefs.Save();

        button.interactable = false;
        if (!isTimerRunning)
        {
            StartCoroutine(StartTimer());
        }
    }

    IEnumerator StartTimer()
    {
        isTimerRunning = true;
        TimeSpan remainingTime = TimeSpan.FromHours(24) - (DateTime.Now - lastPressTime);

        while (remainingTime > TimeSpan.Zero)
        {
            remainingTime = TimeSpan.FromHours(24) - (DateTime.Now - lastPressTime);
            Debug.Log("Remaining time: " + remainingTime);
            yield return new WaitForSeconds(1);
        }

        button.interactable = true;
        isTimerRunning = false;
    }*/
}