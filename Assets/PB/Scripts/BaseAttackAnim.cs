using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackAnim : MonoBehaviour
{
    public abstract void OnAttackStart();
    public abstract void OnAttackEnd();

    public abstract float SetAttackAnim(RectTransform attackObjRect, RectTransform targetObjRect);

    public bool isAttack = false;

}
