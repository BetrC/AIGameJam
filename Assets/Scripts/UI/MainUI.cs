using System;
using DG.Tweening;
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
            Utility.ShowHint("当前没有可用的卡牌，请先抽卡");
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

    private float requestBattleInfoStartTime = 0f;

    private void RequestBattle()
    {
        // 请求对局
        WebDownloader.Instance.GetText(
            $"http://123.207.251.146:4567/request_battle?pokemon_id={GameManager.Instance.currentPokemon.ID}&user_id={GameManager.Instance.userId}", s =>
            {
                if (s == "success" || s == "battle already registered!")
                {
                    requestBattleInfoStartTime = Time.fixedTime;
                    RequestBattleInfo();
                }
                else
                {
                    Utility.ShowHint(s);
                    HideLoading();
                    
                }
            }, exception =>
            {
                Utility.ShowHint(exception.Message); 
                HideLoading();
            });
    }

    private void RequestBattleInfo()
    {
        if (requestBattleInfoStartTime + 30 < Time.fixedTime)
        {
            HideLoading();
            Utility.ShowHint("未找到对局");
            return;
        }

        // 请求对局
        WebDownloader.Instance.GetText(
            $"http://123.207.251.146:4567/check_battle_info?user_id={GameManager.Instance.userId}", s =>
            {
                if (s == Utility.ERROR_MSG_BATTLE_NOT_START)
                {
                    DOVirtual.DelayedCall(0.5f, RequestBattleInfo);
                    return;
                }

                var battleData = JsonUtility.FromJson<PokemonBattleData>(s);
                Debug.Log(s);
                GameManager.Instance.BattleData = battleData;
                // 已找到对局
                SceneManager.LoadScene(1);
            },
            exception =>
            {
                Utility.ShowHint(exception.Message);
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