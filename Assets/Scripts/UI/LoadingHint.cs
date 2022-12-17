using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHint : MonoBehaviour
{
    public Image image;

    private void Start()
    {
        image.transform.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental);
    }
}