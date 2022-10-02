
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RandomExtensions
{
    public static float Random(this AnimationCurve curve, int num = 100){
        float start = curve.keys[0].time;
        float end = curve.keys[curve.length - 1].time;
        float width = end - start;
        float step = width/num;
        float semiStep = step * .5f;

        float[] timeValues = new float[num];
        float[] unnormalizedProbabilities = new float[num];

        float t1 = start;
        float t2 = start + step;
        // float t = start + step*.5f;
        for (int i = 0; i < num; i++)
        {
            unnormalizedProbabilities[i] = (curve.Evaluate(t1) + curve.Evaluate(t2)) * .5f;
            timeValues[i] = t1 + semiStep;
                
            t1 = t2;
            t2 += step;
        }

        return timeValues.RandomUnnormalized(unnormalizedProbabilities);
        
        // float probabilitySum = unnormalizedProbabilities.Sum();
        // // float normalizationConstant = values.Sum() * step;

        // float[] probabilities = new float[num];
        // for (int i = 0; i < num; i++)
        //     probabilities[i] = unnormalizedProbabilities[i] / probabilitySum;

    }

    public static float Sum(this float[] values){
        float s = 0;
        foreach (float k in values) s += k;
        return s;
    }

    public static T RandomUnnormalized<T>(this T[] values, float[] unnormalizedProbabilities){
        int num = values.Length;
        float probabilitySum = unnormalizedProbabilities.Sum();

        float[] probabilities = new float[num];
        for (int i = 0; i < num; i++)
            probabilities[i] = unnormalizedProbabilities[i] / probabilitySum;

        return values.Random(probabilities);
    }

    public static T Random<T>(this T[] values, float[] probabilities){
        float r = UnityEngine.Random.value;
        
        int num = values.Length;

        float p = 0;
        for (int i = 0; i < num; i++)
        {
            p += probabilities[i];

            if (p >= r) return values[i];             
        }
        return values[num - 1];
    }

    public static T Random<T>(this T[] values){
        int n = values.Length;
        float[] probabilities = Enumerable.Repeat((float) 1/n, n).ToArray();
        return Random(values, probabilities);
    }
}