using UnityEngine;

using static TimeManager;

public class UpdateManager : MonoBehaviour
{
    public GameState gameState;
    public CarFunctions carFunctions;

    //public EntityTracker entityTracker;

    private void Start()
    {
        HourTicked += OnHourTicked;
        DayTicked += OnDayTicked;
        WeekTicked += OnWeekTicked;
        MonthTicked += OnMonthTicked;
    }

    public void OnHourTicked()
    {
        carFunctions.Travel();
    }

    public void OnDayTicked()
    {

    }

    public void OnWeekTicked()
    {

    }

    public void OnMonthTicked()
    {

    }

    public void OnYearTicked()
    {

    }
}