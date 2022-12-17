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
}