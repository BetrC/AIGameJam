﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardSpawnEffectControl : MonoBehaviour
{

    public Material effectMat;

    public float duration = 0.2f;

    public float destroyTime = 1;

    public ParticleSystem spawnPar;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        SetMatEffect();
    //    }
    //}
    private void Start()
    {
        SetMatEffect();
        Destroy(gameObject, destroyTime);
    }

    public void SetMatEffect()
    {
        spawnPar.Play();
        effectMat.SetFloat("_FadeAmount", -0.1f);
        effectMat.DOFloat(1, "_FadeAmount", duration).SetEase(Ease.Linear);
    }
}
