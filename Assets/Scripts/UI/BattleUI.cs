using System;
using System.Collections.Generic;
using System.Security.Permissions;
using DG.Tweening;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public UIPokemonCard self;
    public UIPokemonCard other;

    private PokemonData SelfData;
    private PokemonData OtherData;

    private List<BattleStep> _battleSteps;


    private void Awake()
    {
        SelfData = GameManager.Instance.BattleData.player;
        OtherData = GameManager.Instance.BattleData.enemy;
        _battleSteps = GameManager.Instance.BattleData.battle_steps;

        self.SetData(SelfData);
        other.SetData(OtherData);
    }

    private void Start()
    {
        // 模拟战斗
        var sequence = DOTween.Sequence();
        sequence = sequence.AppendCallback(ShowStartView).AppendInterval(1.8f);
        foreach (var step in _battleSteps)
        {
            sequence = sequence.AppendCallback(() => ShowBattleStep(step)).AppendInterval(1f);
        }

        sequence = sequence.AppendCallback(DestroyLoser);
        sequence.AppendCallback(ShowFinishView);
    }

    private void ShowBattleStep(BattleStep step)
    {
        SelfData.HP = step.health_list[0];
        self.UpdateHealth(SelfData.HP);
        OtherData.HP = step.health_list[1];
        other.UpdateHealth(OtherData.HP);
    }

    private void DestroyLoser()
    {
        var finalStep = _battleSteps[_battleSteps.Count - 1];
        if (finalStep.health_list[0] <= 0)
        {
            Destroy(self.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    private int GetLoser()
    {
        var finalStep = _battleSteps[_battleSteps.Count - 1];
        return finalStep.health_list[0] <= 0 ? 0 : 1;
    }

    void ShowStartView()
    {
        var prefab = Utility.GetPrefab("BattleStartView");
        Instantiate(prefab, Utility.UIRoot);
    }

    void ShowFinishView()
    {
        int addCoin = 114514;
        var prefab = Utility.GetPrefab("GameFinishView");
        var panel = Instantiate(prefab, Utility.UIRoot).GetComponent<GameFinishView>();
        panel.Show(GetLoser() == 1, addCoin);
    }
}