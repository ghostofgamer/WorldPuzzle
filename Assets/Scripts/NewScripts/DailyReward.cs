using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private LevelSelectManager _levelSelectManager;

    public Button[] rewardButtons; // Массив кнопок для наград
    private const string LastRewardDateKey = "LastRewardDate";
    private const string RewardDaysKey = "RewardDays";

    private float _claimCoolDown = 24f;
    private float _claimDeadLine = 48f;
    private int _maxStreak = 4;
    private bool _canClaimReward;

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("LastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }

        set
        {
            if (value != null)
                PlayerPrefs.SetString("LastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("LastClaimedTime");
        }
    }

    private int _currentStreak
    {
        get => PlayerPrefs.GetInt("CurrentStreak", 0);
        set => PlayerPrefs.SetInt("CurrentStreak", value);
    }

    private void Start()
    {
        StartCoroutine(RewardStateUpdater());
    }

    private IEnumerator RewardStateUpdater()
    {
        while (true)
        {
            UpdateRewardState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardState()
    {
        // Debug.Log("Updating reward state");
        _canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours >= _claimDeadLine)
            {
                lastClaimTime = null;
                _currentStreak = 0;
            }
            else if (timeSpan.TotalHours < _claimCoolDown)
            {
                _canClaimReward = false;
            }
        }

        var nextClaimTime = lastClaimTime.Value.AddHours(_claimCoolDown);
        var currentClaimCoolDown = nextClaimTime - DateTime.UtcNow;
        string last = $"{currentClaimCoolDown.Hours:D2}:{currentClaimCoolDown.Minutes:D2}:{currentClaimCoolDown.Seconds:D2}";
        Debug.Log(last);
    }


    /*private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }*/

    /*
    void Start()
    {
        // Загрузить состояние наград
        LoadRewardState();

        // Обновить состояние кнопок
        UpdateRewardButtons();
    }
    */

    public void Close()
    {
        // gameObject.SetActive(false);
        ChangeValue(0, false);
    }
    public void Open()
    {
        // gameObject.SetActive(false);
        ChangeValue(1, true);
    }

    private void ChangeValue(float alpha, bool value)
    {
        _canvasGroup.alpha = alpha;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    public void AddCoins(int coin)
    {
        if (coin <= 0)
            return;

        GlobalVariables.globalVariables.AddCoins(coin);
        _levelSelectManager.RefreshStarsAndCoins();
    }


    void LoadRewardState()
    {
        // Загрузить дату последнего получения награды
        string lastRewardDateStr = PlayerPrefs.GetString(LastRewardDateKey, "");
        DateTime lastRewardDate;
        if (DateTime.TryParse(lastRewardDateStr, out lastRewardDate))
        {
            // Проверить, сколько дней прошло с последнего получения награды
            TimeSpan timeSpan = DateTime.Now - lastRewardDate;
            int daysPassed = timeSpan.Days;

            // Загрузить количество полученных наград
            int rewardDays = PlayerPrefs.GetInt(RewardDaysKey, 0);

            // Если прошло 4 дня или больше, сбросить систему
            if (daysPassed >= 4)
            {
                rewardDays = 0;
                PlayerPrefs.SetInt(RewardDaysKey, rewardDays);
            }

            // Обновить дату последнего получения награды
            PlayerPrefs.SetString(LastRewardDateKey, DateTime.Now.ToString());
        }
        else
        {
            // Если дата не найдена, установить текущую дату
            PlayerPrefs.SetString(LastRewardDateKey, DateTime.Now.ToString());
            PlayerPrefs.SetInt(RewardDaysKey, 0);
        }
    }

    void UpdateRewardButtons()
    {
        int rewardDays = PlayerPrefs.GetInt(RewardDaysKey, 0);
        

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            if (i < rewardDays)
            {
                rewardButtons[i].interactable = false;
            }
            else if (i == rewardDays)
            {
                rewardButtons[i].interactable = true;
            }
            else
            {
                rewardButtons[i].interactable = false;
            }
        }
    }

    public void ClaimReward(int day)
    {
        /*int rewardDays = PlayerPrefs.GetInt(RewardDaysKey, 0);

        if (day == rewardDays)
        {
            // Получить награду
            rewardDays++;
            PlayerPrefs.SetInt(RewardDaysKey, rewardDays);

            // Обновить состояние кнопок
            UpdateRewardButtons();

            // Сбросить систему, если собрали 4 награды
            if (rewardDays == 4)
            {
                PlayerPrefs.SetInt(RewardDaysKey, 0);
                PlayerPrefs.SetString(LastRewardDateKey, DateTime.Now.ToString());
            }

            // Блокировать текущую кнопку
            rewardButtons[day].interactable = false;
        }*/
    }
}