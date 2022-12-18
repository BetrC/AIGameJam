using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DrawCardPanel : UIPanelBase
{

    public GameObject cardEffectN;
    public GameObject cardEffectR;
    public GameObject cardEffectSR;
    public GameObject cardEffectSSR;
    
    
    public bool isDrawing = false;

    public UIPokemonCard card;
    public Button buttonReturn;
    
    private float startShowTime;
    public override void OnShow()
    {
        startShowTime = Time.fixedTime;
        card.SetMaskVisible(true);
        AudioManager.Instance.ChangeBgMusic("gainBG");
        buttonReturn.gameObject.SetActive(false);
        isDrawing = true;
        var prefab = Utility.GetPrefab("loadingHint");
        AudioManager.Instance.PlaySound("gainStart");
        
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/request_pokemon", s =>
        {
            var currentTime = Time.fixedTime;
            float timePass = currentTime - startShowTime;
            ShowCard(s, 6f - timePass);
        }, (exception =>
        {
            Debug.Log(exception.Message);
            buttonReturn.gameObject.SetActive(true);
        }));
    }

    private void ShowCard(string s, float time)
    {
        time = Mathf.Max(time, 1);
        var pokemon = JsonUtility.FromJson<PokemonData>(s);
        card.SetData(pokemon);
        var eff = GetCardEffect(Utility.GetCardQuality(pokemon.Rarity));
        var control = GameObject.Instantiate(eff, card.transform).GetComponent<CardSpawnEffectControl>();
        control.duration = time;
        card.SetMaskVisible(false);
        GameManager.Instance.currentPokemon = pokemon;
        GameManager.Instance.AddPokemon(pokemon);
        DOVirtual.DelayedCall(time, (() => buttonReturn.gameObject.SetActive(true)));
    }

    public GameObject GetCardEffect(CardQuality quality)
    {
        switch (quality)
        {
            case CardQuality.N:
                return cardEffectN;
            case CardQuality.R:
                return cardEffectR;
            case CardQuality.SR:
                return cardEffectSR;
            case CardQuality.SSR:
                return cardEffectSSR;
            default:
                return cardEffectN;
        }
    }
    
    public override void OnHide()
    {
        isDrawing = false;
    }
}