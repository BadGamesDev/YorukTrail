using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoadUI : MonoBehaviour
{
    public GameState gameState;
    public CarData carData;

    public TMP_Text distanceIndicator;
    public TMP_Text fuelIndicator;
    public TMP_Text foodIndicator;

    public void UpdateUI()
    {
        distanceIndicator.text = gameState.distanceLeft.ToString();
        fuelIndicator.text = carData.fuelCur.ToString();
    }
}
