using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DrawCardPanel : UIPanelBase
{
    public bool isDrawing = false;

    public UIPokemonCard card;
    public Button buttonReturn;

    public override void OnShow()
    {
        AudioManager.Instance.ChangeBgMusic("gainBG");
        
        buttonReturn.gameObject.SetActive(false);
        isDrawing = true;
        var prefab = Utility.GetPrefab("loadingHint");
        var loading = GameObject.Instantiate(prefab, Utility.UIRoot);
        AudioManager.Instance.PlaySound("gainStart");
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/request_pokemon", s =>
        {
            Destroy(loading);
            var pokemon = JsonUtility.FromJson<PokemonData>(s);
            card.SetData(pokemon);
            buttonReturn.gameObject.SetActive(true);
            GameManager.Instance.currentPokemon = pokemon;
            GameManager.Instance.AddPokemon(pokemon);
        }, (exception =>
        {
            Debug.Log(exception.Message);
            Destroy(loading);
            buttonReturn.gameObject.SetActive(true);
        }));
    }

    public override void OnHide()
    {
        isDrawing = false;
    }
}