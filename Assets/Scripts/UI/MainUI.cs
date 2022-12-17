using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : UIPanelBase
{
    public UIPokemonCard pokemon;

    public float ShakeThreshold = 1f;
    private float oldY = 0f;
    public Text moneyText;

    public override void OnShow()
    {
        pokemon.SetData(GameManager.Instance.currentPokemon);
        moneyText.text = $"游戏币：{GameManager.Instance.Coin}";
    }

    private void Update()
    {
        float newY = Input.acceleration.y;
        float dy = newY - oldY;
        oldY = newY;
        if (dy > ShakeThreshold)
        {
            // 触发震动
        }
    }

    private GameObject loading;
    public void Battle()
    {
        if (GameManager.Instance.currentPokemon == null)
        {
            // 当前没有pokemon可用，请先抽卡
            return;
        }
        loading = Utility.ShowLoading();
        RequestBattle();
    }

    private void HideLoading()
    {
        if (loading != null)
            Destroy(loading);
    }
    
    private void RequestBattle()
    {
        // 请求对局
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/request_battle?pokemon_id=5&user_id=" + GameManager.Instance.userId, s =>
        {
            if (s == "success")
            {
                RequestBattleInfo();
            }
            else
            {
                RequestBattle();
            }
        }, exception => { RequestBattle(); });
    }

    private void RequestBattleInfo()
    {
        // 请求对局
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/check_battle_info?user_id=0", s =>
            {
                var battleData = JsonUtility.FromJson<PokemonBattleData>(s);
                GameManager.Instance.BattleData = battleData;
                // 已找到对局
                SceneManager.LoadScene(1);
            },
            exception =>
            {
                // 未找到对局
                HideLoading();
            });
    }

    public override void OnHide()
    {
    }

    public void OnClickChargeButton()
    {
        UIManager.Instance.ShowPanel(typeof(ChargePanel));
    }

    public void OnClickDrawCardButton()
    {
        UIManager.Instance.ShowPanel(typeof(DrawCardPanel));
    }

    public void OnClickBagButton()
    {
        UIManager.Instance.ShowPanel(typeof(BagPanel));
    }

    public void OnClickMallButton()
    {
        UIManager.Instance.ShowPanel(typeof(MallPanel));
    }
}