using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public delegate void OnTick();
    public static event OnTick HourTicked;
    public static event OnTick DayTicked;
    public static event OnTick WeekTicked;
    public static event OnTick MonthTicked;
    public static event OnTick YearTicked;

    [SerializeField] private int hour;
    [SerializeField] private int day;
    [SerializeField] private int weekDay;
    [SerializeField] private int month;
    [SerializeField] private int year;

    private readonly int[] daysPerMonth = new[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private readonly string[] timesOfDay = new[] { "Dawn", "Morning", "Noon", "Afternoon", "Dusk", "Night" };
    private readonly string[] dayNames = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    private readonly string[] monthNames = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    public float timeMultiplier = 1f;
    private float timeSinceTick;
    private bool isPaused = false;

    #region PauseUnpause
    private void OnEnable()
    {
        GameState.OnPause += PauseTime;
        GameState.OnResume += ResumeTime;
    }

    private void OnDisable()
    {
        GameState.OnPause -= PauseTime;
        GameState.OnResume -= ResumeTime;
    }

    private void PauseTime()
    {
        isPaused = true;
    }

    private void ResumeTime()
    {
        isPaused = false;
    }
    #endregion

    #region Safety
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Update()
    {
        if (isPaused) //doing the pause check like this avoids unnecessary references and dependencies at the cost of an extremely negligible hit to performance
        {
            return;
        }

        float scaledDeltaTime = Time.deltaTime * timeMultiplier;
        timeSinceTick += scaledDeltaTime;

        while (timeSinceTick >= 1f)
        {
            timeSinceTick -= 1f;
            HourTick();
        }
    }

    private void HourTick()
    {
        if (hour == 23)
        {
            DayTick();
        }
        else
        {
            hour += 1;
        }

        HourTicked?.Invoke();
    }

    private void DayTick()
    {
        hour = 0;

        if (weekDay < 7)
        {
            weekDay += 1;
        }
        else
        {
            WeekTick();
        }

        int maxDays = daysPerMonth[month - 1]; //can cause erros if the month 0 (like it is not set to anything at start etc.)
        if (day < maxDays)
        {
            day += 1;
        }
        else
        {
            MonthTick();
        }

        DayTicked?.Invoke();
    }

    private void WeekTick()
    {
        weekDay = 1;
        WeekTicked?.Invoke();
    }

    private void MonthTick()
    {
        if (month < 12)
        {
            month += 1;
        }
        else
        {
            YearTick();
        }

        day = 1;
        MonthTicked?.Invoke();
    }

    private void YearTick()
    {
        month = 1;
        year += 1;
        UpdateLeapYear();
        YearTicked?.Invoke();
    }

    private void UpdateLeapYear()
    {
        if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
        {
            daysPerMonth[1] = 29;
        }
        else
        {
            daysPerMonth[1] = 28;
        }
    }

    public int GetHour() => hour;
    public int GetDay() => day;
    public int GetWeekDay() => weekDay;
    public int GetMonth() => month;
    public int GetYear() => year;
    public string GetTimeOfDay() => timesOfDay[Mathf.FloorToInt(hour / 4)];
    public string GetDayName() => dayNames[weekDay - 1];
    public string GetMonthName() => monthNames[month - 1];
}