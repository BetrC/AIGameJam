using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilBulletAttackAnim : BaseAttackAnim
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
        StartCoroutine(ShootBullet(attackObjRect, targetObjRect));
        //  bulletGo.transform.DOMove(new Vector3(targetObjRect.transform.position.x, targetObjRect.transform.position.y, targetObjRect.transform.position.z ), 0.5f);
        return 3f;
    }
    IEnumerator ShootBullet(RectTransform attackObjRect, RectTransform targetObjRect)
    {
        for (int i = 0; i < 20; i++)
        {
            attackObjRect.localScale = Vector3.one;
            attackSequece = DOTween.Sequence();
            isAttack = false;
            attackSequece.Append(attackObjRect.transform.DOScale(new Vector3(1.1f, 0.9f, 1), 0.2f));
            attackSequece.Append(attackObjRect.transform.DOScale(new Vector3(0.8f, 1.2f, 1), 0.1f).OnStart(() => {

                GameObject muzzleGo = Instantiate((GameObject)Resources.Load("VFX/VFX_LilBullet_Muzzle"), attackObjRect.transform);
                muzzleGo.transform.position = new Vector3(attackObjRect.transform.position.x, attackObjRect.transform.position.y, attackObjRect.transform.position.z);

                GameObject bulletGo = Instantiate((GameObject)Resources.Load("VFX/VFX_LilBullet"));
                Vector3[] path = new Vector3[3];

                float xOffset = Random.Range(-10, 10);

                bulletGo.transform.position = new Vector3(attackObjRect.transform.position.x+ xOffset, attackObjRect.transform.position.y, attackObjRect.transform.position.z - 30);

                //path[0] = bulletGo.transform.position;
                //path[1] = new Vector3(targetObjRect.transform.position.x , (targetObjRect.transform.position.y + attackObjRect.transform.position.y) / 2, attackObjRect.transform.position.z - 30);
                //path[2] = new Vector3(targetObjRect.transform.position.x + xOffset, targetObjRect.transform.position.y, targetObjRect.transform.position.z );

                bulletGo.transform.DOMove(new Vector3( targetObjRect.transform.position.x + xOffset, targetObjRect.transform.position.y, targetObjRect.transform.position.z), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_LilBullet_Impact"));
                    impactGo.transform.position = new Vector3(bulletGo.transform.position.x, bulletGo.transform.position.y, bulletGo.transform.position.z - 10);
                    SetTargetHit(targetObjRect.gameObject);
                    Destroy(bulletGo, 1);
                });
            }));
            attackSequece.Append(attackObjRect.transform.DOScale(new Vector3(1, 1, 1), 0.2f));
            attackSequece.Play();
            attackSequece.OnComplete(() => {


            });

            yield return new WaitForSeconds(0.1f);
        }
        //yield return new WaitForSeconds(0.3f);
        //InitTargetState(targetObjRect.gameObject);
    }
}
