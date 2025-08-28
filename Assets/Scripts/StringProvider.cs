using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CalculationRule
{
    Complementary,
    BrightnessPositive,
    BrightnessNegative,
    ContrastPositive,
    ContrastNegative,
    SaturationPositive,
    SaturationNegative,
    WarmnessPositive,
    WarmnessNegative
}

[Serializable]
public class ColorQuestion
{
    public string question;
    public CalculationRule rule;
}

public class StringProvider : MonoBehaviour
{
    [SerializeField] private ColorQuestion[] colorQuestions;
    
    public List<ColorQuestion> GetQuestionSequence(int number)
    {
        List<ColorQuestion> assets = new List<ColorQuestion>();
        for (int i = 0; i < colorQuestions.Length; i++)
        {
            assets.Add(colorQuestions[i]);
        }
        
        int difference = number - colorQuestions.Length;
        for (int i = 0; i < difference; i++)
        {
            assets.Add(colorQuestions[Random.Range(0, colorQuestions.Length)]);
        }
        
        return assets;
    }
}
