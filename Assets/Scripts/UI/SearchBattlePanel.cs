using UnityEngine;

// 寻找对局
public class SearchBattlePanel : UIPanelBase
{
    public float ShakeThreshold = 1f;
    private float oldY = 0;

    private bool IsSearching = false;

    // 震动检测
    void Update()
    {
        if (IsSearching)
            return;
        float newY = Input.acceleration.y;
        float dy = newY - oldY;
        oldY = newY;
        if (dy > ShakeThreshold)
        {
            // 触发震动
        }
    }

    private GameObject loading;

    private void HideLoading()
    {
        if (loading != null)
            Destroy(loading);
    }

    public void SearchBattle()
    {
        IsSearching = true;
        loading = Utility.ShowLoading();
    }


    private void RequestBattle()
    {
        // 请求对局
        WebDownloader.Instance.GetText("http://123.207.251.146:4567/request_battle?pokemon_id=5&user_id=0", s =>
        {
            if (s == "success")
            {
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
            },
            exception =>
            {
                // 未找到对局
            });
    }

    public override void OnShow()
    {
    }

    public override void OnHide()
    {
        IsSearching = false;
    }
}