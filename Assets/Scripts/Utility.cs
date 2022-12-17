using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
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
}