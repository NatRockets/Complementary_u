using UnityEngine;

public class ShadeCalculator : MonoBehaviour
{
    public void GenerateColorOptions(CalculationRule rule, out Color sample, out Color var1, out Color var2)
    {
        sample = default;
        var1 = default;
        var2 = default;
        
        switch (rule)
        {
            case CalculationRule.BrightnessNegative: Brightness(false, out var1, out var2); break;
            case CalculationRule.BrightnessPositive: Brightness(true, out var1, out var2); break;
            case CalculationRule.ContrastNegative: Contrast(false, out sample, out var1, out var2); break;
            case CalculationRule.ContrastPositive: Contrast(true, out sample, out var1, out var2); break;
            case CalculationRule.Complementary: Complementary(out sample, out var1, out var2); break;
            case CalculationRule.WarmnessNegative: Warmness(false, out var1, out var2); break;
            case CalculationRule.WarmnessPositive: Warmness(true, out var1, out var2); break;
            case CalculationRule.SaturationNegative: Saturation(false, out var1, out var2); break;
            case CalculationRule.SaturationPositive: Saturation(true, out var1, out var2); break;
        }
    }

    private void Complementary(out Color sample, out Color var1, out Color var2)
    {
        float h = Random.Range(0f, 1f);
        float s = Random.Range(0.5f, 1f);
        float v = Random.Range(0.5f, 1f);
        sample = Color.HSVToRGB(h, s, v);
        
        var1 = Color.HSVToRGB((h + 0.5f) % 1, s, v);
        var2 = Color.HSVToRGB((h + 0.5f + Random.Range(0.1f, 0.3f)) % 1, h, v);
    }

    private void Warmness(bool pos, out Color var1, out Color var2)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        Color hue1 = Color.HSVToRGB(val1, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        Color hue2 = Color.HSVToRGB(val2, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        
        float diff1 = Mathf.Abs(0.5f - val1);
        float diff2 = Mathf.Abs(0.5f - val2);

        if (diff2 > diff1)
        {
            var1 = pos ? hue2 : hue1;
            var2 = pos ? hue1 : hue2;
        }
        else
        {
            var1 = pos ? hue1 : hue2;
            var2 = pos ? hue2 : hue1;
        }
    }

    private void Saturation(bool pos, out Color var1, out Color var2)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        Color sat1 = Color.HSVToRGB(Random.Range(0f, 1f), val1, Random.Range(0.5f, 1f));
        Color sat2 = Color.HSVToRGB(Random.Range(0f, 1f), val2, Random.Range(0.5f, 1f));

        if (val1 > val2)
        {
            var1 = pos ? sat1 : sat2;
            var2 = pos ? sat2 : sat1;
        }
        else
        {
            var1 = pos ? sat2 : sat1;
            var2 = pos ? sat1 : sat2;
        }
    }

    private void Contrast(bool pos, out Color sample, out Color var1, out Color var2)
    {
        float h = Random.Range(0f, 1f);
        float s = Random.Range(0.5f, 1f);
        float v = Random.Range(0.3f, 1f);
        sample = Color.HSVToRGB(h, s, v);
        
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        Color bri1 = Color.HSVToRGB(Random.Range(0f, 1f), s, val1);
        Color bri2 = Color.HSVToRGB(Random.Range(0f, 1f), s, val2);

        float diff1 = Mathf.Abs(v - val1);
        float diff2 = Mathf.Abs(v - val2);
        
        if (diff1 > diff2)
        {
            var1 = pos ? bri1 : bri2;
            var2 = pos ? bri2 : bri1;
        }
        else
        {
            var1 = pos ? bri2 : bri1;
            var2 = pos ? bri1 : bri2;
        }
    }
    
    private void Brightness(bool pos, out Color var1, out Color var2)
    {
        float val1 = Random.Range(0.3f, 1f);
        float val2 = Random.Range(0.3f, 1f);
        Color bri1 = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.5f, 1f), val1);
        Color bri2 = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.5f, 1f), val2);

        if (val1 > val2)
        {
            var1 = pos ? bri1 : bri2;
            var2 = pos ? bri2 : bri1;
        }
        else
        {
            var1 = pos ? bri2 : bri1;
            var2 = pos ? bri1 : bri2;
        }
    }
}
