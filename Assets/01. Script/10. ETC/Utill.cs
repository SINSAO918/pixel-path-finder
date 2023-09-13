using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Utill
{
    public static class Timer
    {
        private static Dictionary<string, float> timerDictionart = new Dictionary<string, float>(); 
        
        public static void StartTimer(string timerName)
        {
            if (timerDictionart.ContainsKey(timerName))
                timerDictionart.Remove(timerName);

            timerDictionart.Add(timerName, Time.realtimeSinceStartup);
        }
        public static float GetTimer(string timerName)
        {
            return Time.realtimeSinceStartup - timerDictionart[timerName];
        }
    }

    public static List<TReturn> Extraction<TSource, TReturn>(this IEnumerable<TSource> source, Func<TSource, TReturn> func)
    {
        List<TReturn> returns = new();
        foreach (var item in source)
        {
            returns.Add(func(item));
        }
        return returns;
    }
    public static bool RadomBool => UnityEngine.Random.Range(0, 2) == 0;
}