using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NormalAttackAnim : BaseAttackAnim
{
    public Sequence attackSequece;

    public RectTransform a;
    public RectTransform b;



    public override void OnAttackEnd()
    {
        //    throw new System.NotImplementedException();
    }

    public override void OnAttackStart()
    {
        //     throw new System.NotImplementedException();
    }

 

    public override float SetAttackAnim(RectTransform attackObjRect, RectTransform targetObjRect)
    {
        attackObjRect.transform.SetAsLastSibling();
       //需要设置一下层级
        attackSequece = DOTween.Sequence();
        isAttack = true;
        Vector3 basePos = attackObjRect.anchoredPosition3D;
        attackSequece.Append(attackObjRect.DOAnchorPos3DZ(-130, 0.3f));
        //attackSequece.Append(attackObjRect.DOAnchorPos3D(new Vector3(targetObjRect.anchoredPosition.x, targetObjRect.anchoredPosition.y), 0.2f).SetEase(Ease.InBack) .OnComplete(()=> {
        //    SetTargetHit(targetObjRect.gameObject);

        //    GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_NormalAttackImpact"));
        //    impactGo.transform.position = new Vector3(targetObjRect.transform.position.x, targetObjRect.transform.position.y, targetObjRect.transform.position.z-10);

        //}).OnStart(()=> {

        //    float rotateNum = 30;
        //    if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
        //    {
        //        rotateNum = -30;
        //    }
        //    attackObjRect.DORotate(new Vector3(rotateNum, 0, 0), 0.05f);
        //}));//-targetObjRect.rect.width/2
        attackSequece.Append(attackObjRect.DOMove(new Vector3(targetObjRect.position.x, targetObjRect.position.y, targetObjRect.position.z), 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SetTargetHit(targetObjRect.gameObject);

            GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_NormalAttackImpact"));
            impactGo.transform.position = new Vector3(targetObjRect.transform.position.x, targetObjRect.transform.position.y, targetObjRect.transform.position.z - 10);

        }).OnStart(() =>
        {

            float rotateNum = -30;
            if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
            {
                rotateNum = 30;
            }
            attackObjRect.DORotate(new Vector3(rotateNum, 0, 0), 0.05f);
        }));//-targetObjRect.rect.width/2


        attackSequece.Append(attackObjRect.DOAnchorPos3D(basePos, 0.4f).OnStart(() => {
            attackObjRect.DORotate(new Vector3(0, 0, 0), 0.2f);
        }));
        attackSequece.Play();
        attackSequece.OnComplete(() => {

            isAttack = false;
        });
        return 1;
    }
}
