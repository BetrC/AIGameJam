using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAttackAnim : BaseAttackAnim
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
            attackSequece.Append(attackObjRect.DORotate(new Vector3(0, 0, 50), 0.3f).SetEase(Ease.OutBack));
            //   attackSequece.AppendInterval(0.5f);
            attackSequece.Append(attackObjRect.DORotate(new Vector3(0, 0, -7200), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnStart(()=> {
                float offset = -20;
                GameObject loopGo = Instantiate((GameObject)Resources.Load("VFX/VFX_RotateAttackLoop"), attackObjRect);
                GameObject trailGo = Instantiate((GameObject)Resources.Load("VFX/VFX_RotateAttackTrail"), attackObjRect);
                Destroy(trailGo, 2);
                if (attackObjRect.anchoredPosition.y > targetObjRect.anchoredPosition.y)
                {
                    offset = 20;
                }
            attackObjRect.DOMove(new Vector3(targetObjRect.position.x, targetObjRect.position.y- offset, targetObjRect.position.z), 0.5f).SetEase(Ease.Linear).OnComplete(()=> {
                StartCoroutine(SetEnemyHit(targetObjRect.gameObject));
            });
            }));
            //    attackSequece.Append();
            //    attackSequece.AppendInterval(0.5f);
            attackSequece.Append(attackObjRect.transform.DOShakeRotation(0.5f, new Vector3(0, 0, 15), 20).OnStart(()=> {
                attackSequece.Append(attackObjRect.DOAnchorPos3D(basePos, 0.4f).OnStart(() =>
                {

                }));

            }));


            //  attackObjRect.DORotate(new Vector3(0, 0, 0), 0.2f);
            attackSequece.Play();
            attackSequece.OnComplete(() =>
            {
                attackObjRect.DORotate(new Vector3(0, 0, 0), 0.2f);
                isAttack = false;
            });
            return 4;
    }


    IEnumerator SetEnemyHit(GameObject targetObj)
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_RotateAttackImpact"),targetObj.transform);
            SetTargetHit(targetObj);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
