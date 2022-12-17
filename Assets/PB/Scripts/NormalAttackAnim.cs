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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetAttackAnim(a, b);
        }
    }

    public override float SetAttackAnim(RectTransform attackObjRect, RectTransform targetObjRect)
    {
       //需要设置一下层级
        attackSequece = DOTween.Sequence();
        isAttack = false;
        Vector3 basePos = attackObjRect.anchoredPosition3D;
        attackSequece.Append(attackObjRect.DOAnchorPos3DZ(-130, 0.3f));
        attackSequece.Append(attackObjRect.DOAnchorPos3D(new Vector3(targetObjRect.anchoredPosition.x, targetObjRect.anchoredPosition.y), 0.2f).SetEase(Ease.InBack) .OnComplete(()=> {
            targetObjRect.transform.DOShakeRotation(0.2f,new Vector3(30,30,15),10);
            targetObjRect.transform.DOShakePosition(0.15f, new Vector3(15, 15, 0), 30);
            //   targetObjRect.transform.DOShakeScale(0.15f, new Vector3(-0.5f,-0.5f,-0.5f), 10);
            targetObjRect.transform.DOPunchScale(new Vector3(-0.5f, -0.5f, -0.5f), 0.3f,10,-0.5f);

        //    targetObjRect.GetComponent<Image>().DOColor(new Color32(255, 50, 50, 255), 0.3f).From();



            GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_NormalAttackImpact"));
            impactGo.transform.position = new Vector3(targetObjRect.transform.position.x, targetObjRect.transform.position.y, targetObjRect.transform.position.z-10);

        }).OnStart(()=> {

            float rotateNum = 30;
            if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
            {
                rotateNum = -30;
            }
            attackObjRect.DORotate(new Vector3(rotateNum, 0, 0), 0.05f);
        }));//-targetObjRect.rect.width/2
        attackSequece.Append(attackObjRect.DOAnchorPos3D(basePos, 0.4f).OnStart(() => {
            attackObjRect.DORotate(new Vector3(0, 0, 0), 0.2f);
        }));
        attackSequece.Play();
        attackSequece.OnComplete(() => {

            isAttack = true;
        });
        return 1;
    }
}
