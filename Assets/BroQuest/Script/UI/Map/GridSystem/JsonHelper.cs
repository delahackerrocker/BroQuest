using System;
using UnityEngine;
using System.Collections.Generic;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    /// <summary>
    /// Generates new list of items based on a JSON string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">JSON string (NOT path).</param>
    /// <returns></returns>
    public static List<T> ListFromJson<T>(string json)
    {
        List<T> list = new List<T>();
        T[] thingsArray = FromJson<T>(json);
        foreach (T thing in thingsArray)
        {
            list.Add(thing);
        }

        return list;
    }

    /// <summary>
    /// Adds items to an existing list from a JSON string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">List to add items to.</param>
    /// <param name="json">JSON string (NOT path).</param>
    public static void AddToListFromJson<T>(List<T> list, string json)
    {
        T[] things = FromJson<T>(json);
        foreach (T thing in things)
        {
            list.Add(thing);
        }
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
