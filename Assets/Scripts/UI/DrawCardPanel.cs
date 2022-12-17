using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DrawCardPanel : UIPanelBase
{
    public bool isDrawing = false;

    public override void OnShow()
    {
        isDrawing = true;
        var prefab = Utility.GetPrefab("loadingHint");
        var loading = GameObject.Instantiate(prefab, Utility.UIRoot);
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/request_pokemon", s =>
        {
            Destroy(loading);
            var pokemon = JsonUtility.FromJson<PokemonData>(s);
            // 展示一下获得的卡牌
            var prefab = Utility.GetPrefab("pokemonGet");
            var view = GameObject.Instantiate(prefab, Utility.UIRoot).GetComponent<PokemonGetView>();
            view.Init(pokemon);
        }, (exception =>
        {
            Debug.Log(exception.Message);
            Destroy(loading);
        }));
    }

    public override void OnHide()
    {
        isDrawing = false;
    }
}