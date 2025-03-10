using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFunctions : MonoBehaviour
{
    public GameState gameState;
    public CarData data;

    private void Start()
    {
        ChangeSpeedLevel(2);
    }

    public void ChangeSpeedLevel(int level)
    {
        data.speedLevel = level;
        CalculateSpeed();
    }
    
    public void CalculateSpeed()
    {
        data.speedCur = data.speed * data.speedLevel;
    }
    
    public void Travel()
    {
        if (data.speedLevel != 0)
        {
            int fuelUsage = data.speedLevel * data.fuelEfficiency;

            if (fuelUsage < data.fuelCur)
            {
                gameState.distanceLeft -= data.speedCur;
                data.fuelCur -= fuelUsage;
            }

            else
            {
                gameState.distanceLeft -= Mathf.FloorToInt(((float)data.fuelCur / fuelUsage) * data.speedCur);
                data.fuelCur = 0;
                RunOutOfGas();
            }
        }
    }

    public void RunOutOfGas()
    {
        Debug.Log("You are out of gas retard");
        data.speedLevel = 0;
    }
}
