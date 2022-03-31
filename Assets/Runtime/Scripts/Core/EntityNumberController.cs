using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityNumberController : MonoBehaviour
{
    private static Dictionary<FoodParameters, int> foodCountDict = new Dictionary<FoodParameters, int>();
    private static Dictionary<FishParameters, int> fishCountDict = new Dictionary<FishParameters, int>();

    public static Dictionary<FoodParameters, int> FoodCountDict =>  foodCountDict;
    public static Dictionary<FishParameters, int> FishCountDict => fishCountDict;

    public static void AddFoodCount(FoodParameters name)
    {
        if (!foodCountDict.ContainsKey(name))
        {
            foodCountDict.Add(name, 1);
        }
        else
        {
            foodCountDict[name]++;
        }
        foreach (var item in foodCountDict)
            Debug.Log(item.Key);
        Debug.Log(foodCountDict[name]);
    }
    public static void RemoveFoodCount(FoodParameters name)
    {
        if (foodCountDict.ContainsKey(name) && foodCountDict[name] > 0)
        {
            foodCountDict[name]--;
        }
        else
        {
            foodCountDict.Remove(name);
        }
        foreach (var item in foodCountDict)
            Debug.Log(item.Key);
        Debug.Log(foodCountDict[name]);
    }
    public static void AddFishCount(FishParameters name)
    {
        if (!fishCountDict.ContainsKey(name))
        {
            fishCountDict.Add(name, 1);
        }
        else
        {
            fishCountDict[name]++;
        }
        foreach (var item in fishCountDict)
            Debug.Log(item.Key);
        Debug.Log(fishCountDict[name]);
    }
    public static void RemoveFishCount(FishParameters name)
    {
        if (fishCountDict.ContainsKey(name) && fishCountDict[name] > 0)
        {
            fishCountDict[name]--;
        }
        else
        {
            fishCountDict.Remove(name);
        }
        foreach (var item in fishCountDict)
            Debug.Log(item.Key);
        Debug.Log(fishCountDict[name]);
    }


    private static void AddCount(ScriptableObject name, Dictionary<ScriptableObject, int> dictionary)
    {
        if (!dictionary.ContainsKey(name))
        {
            dictionary.Add(name, 1);
        }
        else
        {
            dictionary[name]++;
        }
        foreach (var item in dictionary)
            Debug.Log(item.Key);
        Debug.Log(dictionary[name]);
    }
    private static void RemoveCount(ScriptableObject name, Dictionary<ScriptableObject, int> dictionary)
    {
        if (dictionary.ContainsKey(name) && dictionary[name] > 0)
        {
            dictionary[name]--;
        }
        else
        {
            dictionary.Remove(name);
        }
        foreach (var item in dictionary)
            Debug.Log(item.Key);
        Debug.Log(dictionary[name]);
    }
    

}
