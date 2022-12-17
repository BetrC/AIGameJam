using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    
    public static string SUCCESS_MSG = "success";
    public static string ERROR_MSG_ARGS_WRONG = "wrong arg!";
    public static string ERROR_MSG_PATH_WRONG = "wrong path!";
    public static string ERROR_MSG_CANNOT_FIND_IMG = "cannot find image!";
    public static string ERROR_MSG_BATTLE_EXIST = "battle already registered!";
    public static string ERROR_MSG_BATTLE_NOT_START = "battle not start yet!";

    public static GameObject GetPrefab(string name)
    {
        return Resources.Load<GameObject>("Prefab/" + name);
    }

    public static Transform UIRoot => GameObject.Find("Canvas").transform;

    public static GameObject ShowLoading()
    {
        var prefab = Utility.GetPrefab("loadingHint");
        var loading = GameObject.Instantiate(prefab, UIRoot);
        return loading;
    }

    public static Dictionary<string, CardQuality> CardQualityDictionary = new Dictionary<string, CardQuality>()
    {
        {"Epic", CardQuality.N},
        {"Uncommon", CardQuality.R},
        {"Very Rare", CardQuality.SR},
        {"Mythic", CardQuality.SSR},
    };
    
    public static CardQuality GetCardQuality(string quality)
    {
        if (CardQualityDictionary.ContainsKey(quality))
            return CardQualityDictionary[quality];
        return CardQuality.N;
    }

    public static void ShowHint(string text)
    {
        var prefab = GetPrefab("hint");
        var hint = GameObject.Instantiate(prefab, UIRoot).GetComponent<Hint>();
        hint.Begin(text);
    }
}