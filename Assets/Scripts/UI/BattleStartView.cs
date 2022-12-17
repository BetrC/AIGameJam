using System;
using DG.Tweening;
using UnityEngine;

public class BattleStartView : MonoBehaviour
{
    public Transform imageBack;
    public void Start()
    {
        imageBack.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        sequence.Append(imageBack.DOScale(Vector3.one, 1f)).AppendInterval(1f)
            .Append(imageBack.DOScale(Vector3.zero, 1f)).AppendInterval(.8f).AppendCallback(Close);
    }

    void Close()
    {
        Destroy(gameObject);
    }
}