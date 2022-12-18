using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public UIPokemonCard self;
    public UIPokemonCard other;

    private PokemonData SelfData;
    private PokemonData OtherData;

    private List<BattleStep> _battleSteps;

    public List<BaseAttackAnim> AttackAnims;

    private void Awake()
    {
        SelfData = GameManager.Instance.BattleData.player;
        OtherData = GameManager.Instance.BattleData.enemy;
        _battleSteps = GameManager.Instance.BattleData.battle_steps;

        AttackAnims = transform.GetComponents<BaseAttackAnim>().ToList();

        self.SetData(SelfData);
        other.SetData(OtherData);
    }

    private void Start()
    {
        AudioManager.Instance.ChangeBgMusic("fightBG");
        
        // 模拟战斗
        var sequence = DOTween.Sequence();
        sequence = sequence.AppendCallback(ShowStartView).AppendInterval(1.8f).AppendCallback((() =>
        {
            StartCoroutine(DoBattleStep());
        }));
    }

    private IEnumerator DoBattleStep()
    {
        var sequence = DOTween.Sequence();
        int lastIndex = -1;
        foreach (var step in _battleSteps)
        {
            var index = Random.Range(0, AttackAnims.Count);
            while (index == lastIndex)
            {
                index = Random.Range(0, AttackAnims.Count);
            }

            lastIndex = index;
            var anim = AttackAnims[index];
            var from = step.atk_from == 0 ? self : other;
            var to = step.atk_from == 0 ? other : self;
            float time = anim.SetAttackAnim(from.transform as RectTransform, to.transform as RectTransform);
            yield return new WaitForSeconds(time);
            ApplyFightHealth(step);
            yield return null;
        }
        DissolveLoser();
        yield return new WaitForSeconds(1f);
        ShowFinishView();
    }

    private void ApplyFightHealth(BattleStep step)
    {
        SelfData.HP = step.health_list[0];
        self.UpdateHealth(SelfData.HP);
        OtherData.HP = step.health_list[1];
        other.UpdateHealth(OtherData.HP);
    }

    private void DissolveLoser()
    {
        var finalStep = _battleSteps[_battleSteps.Count - 1];
        if (finalStep.health_list[0] <= 0)
        {
            self.gameObject.AddComponent<CardDissolveEffect>();
        }
        else
        {
            other.gameObject.AddComponent<CardDissolveEffect>();
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
        int addCoin = 328;
        var prefab = Utility.GetPrefab("GameFinishView");
        var panel = Instantiate(prefab, Utility.UIRoot).GetComponent<GameFinishView>();
        panel.Show(GetLoser() == 1, addCoin);
    }
}