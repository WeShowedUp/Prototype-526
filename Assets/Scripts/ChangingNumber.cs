using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingNumber : MonoBehaviour
{
    public Text currency;
    private float animationTime = 1.5f;
    private float currNumber, goalNumber, initial;

    public void AddToNumber(int value)
    {
        initial = currNumber;
        goalNumber += value;
    }

    public void SubToNumber(int value)
    {
        initial = currNumber;
        goalNumber -= value;
    }

    public void SetToZero()
    {
        initial = currNumber;
        goalNumber = 0;
    }

    public void Update()
    {
        if (currNumber != goalNumber)
        {
            float diff;
            if (initial < goalNumber)
            {
                diff = goalNumber - initial;
                currNumber += (animationTime * Time.deltaTime) * diff;
                if (currNumber >= goalNumber)
                    currNumber = goalNumber;
            }
            else
            {
                diff = initial - goalNumber;
                currNumber -= (animationTime * Time.deltaTime) * diff;
                if (currNumber <= goalNumber)
                    currNumber = goalNumber;
            }
            currency.text = currNumber.ToString("0");
        }
    }
}
