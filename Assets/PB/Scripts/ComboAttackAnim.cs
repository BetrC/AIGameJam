using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackAnim : BaseAttackAnim
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
        attackObjRect.transform.SetAsLastSibling();
        //需要设置一下层级
        attackSequece = DOTween.Sequence();
        isAttack = true;
        Vector3 basePos = attackObjRect.anchoredPosition3D;

        attackSequece.Append(attackObjRect.DOScale(new Vector3(1.1f,0.9f,1.1f), 0.3f).OnStart(()=> {

            attackObjRect.transform.DOShakePosition(0.3f, new Vector3(10, 10, 10), 45);
        }));

        //float zMoveNum = -200;
        //if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
        //{
        //    zMoveNum = 200;
        //}


        attackSequece.Append(attackObjRect.DOAnchorPos3DZ(-200, 0.1f).SetEase(Ease.InOutQuint).OnStart(()=> {

            float moveNum = -2400;
            if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
            {
                moveNum = 2500; 
            }

            attackObjRect.DOAnchorPos3DY(moveNum, 0.1f).SetEase(Ease.InOutQuint).OnComplete(()=> {
                StartCoroutine(SetEnemyHit(targetObjRect));
            });
            attackObjRect.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.3f).SetEase(Ease.OutQuart).OnComplete(()=> {
                attackObjRect.DOScale(Vector3.zero, 0f);
          
            });
        }));

        attackSequece.AppendInterval(1.3f);
        attackSequece.Append(attackObjRect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnStart(()=> {

            attackObjRect.DOAnchorPos3D(basePos, 0.2f);
        }).OnComplete(()=> {

            attackObjRect.transform.DOShakePosition(0.5f, new Vector3(10, 10, 10), 15);
        }));
        //attackSequece.Append(attackObjRect.DOAnchorPos3D(basePos, 0.4f).OnStart(() => {
        //    attackObjRect.DORotate(new Vector3(0, 0, 0), 0.2f);
        //}));
        attackSequece.Play();
        attackSequece.OnComplete(() => {

            isAttack = false;
        });
        return 3;
    }

    IEnumerator SetEnemyHit(RectTransform targetObjRect)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_ComboAttackImpact"), targetObjRect);
            impactGo.transform.localPosition = new Vector3(Random.Range(-targetObjRect.rect.width / 3, targetObjRect.rect.width / 3), Random.Range(-targetObjRect.rect.height / 3, targetObjRect.rect.height / 3), 0);

            SetTargetHit(targetObjRect.gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
