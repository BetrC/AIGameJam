
using System;
using DG.Tweening;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public UIPokemonCard me;
    public UIPokemonCard other;

    public AnimationCurve curve;


    public Vector3 meInitPos;
    public Vector3 otherInitPos;
    public void Awake()
    {
        meInitPos = me.transform.position;
        otherInitPos = other.transform.position;
    }

    private void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(me.transform.DOMove(other.transform.position, 2f).SetEase(curve));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("move"))
        {
            Move();
        }

        if (GUILayout.Button("rotate"))
        {
            
        }
    }

    void Move()
    {
        me.transform.position = meInitPos;
        other.transform.position = otherInitPos;
        var sequence = DOTween.Sequence();
        sequence.Append(me.transform.DOMove(other.transform.position, 2f).SetEase(curve));
    }

    // void Rotate()
    // {
    //     
    // }
    
    
}