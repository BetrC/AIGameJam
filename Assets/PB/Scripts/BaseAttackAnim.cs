using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract class BaseAttackAnim : MonoBehaviour
{
    public abstract void OnAttackStart();
    public abstract void OnAttackEnd();

    public abstract float SetAttackAnim(RectTransform attackObjRect, RectTransform targetObjRect);

    public bool isAttack = false;


    public void SetTargetHit(GameObject targetObj)
    {
        targetObj.transform.DOKill();
        targetObj.transform.localScale = Vector3.one;
        targetObj.transform.eulerAngles = Vector3.zero;
        targetObj.transform.DOShakeRotation(0.2f, new Vector3(30, 30, 15), 10);
    //    targetObj.transform.DOShakePosition(0.15f, new Vector3(15, 15, 0), 30);
        //   targetObjRect.transform.DOShakeScale(0.15f, new Vector3(-0.5f,-0.5f,-0.5f), 10);
        targetObj.transform.DOPunchScale(new Vector3(-0.5f, -0.5f, -0.5f), 0.3f, 10, -0.5f);

        //    targetObjRect.GetComponent<Image>().DOColor(new Color32(255, 50, 50, 255), 0.3f).From();
        Image[] images = targetObj.GetComponentsInChildren<Image>();

        while (images != null && images.Length > 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.white;
                images[i].DOColor(new Color32(255, 50, 50, 255), 0.3f).From();
            }
            break;
        }

 
    }

    public void InitTargetState(GameObject targetObj)
    {
        targetObj.transform.DOKill();
        targetObj.transform.localScale = Vector3.one;
        targetObj.transform.eulerAngles = Vector3.zero;
        Image[] images = targetObj.GetComponentsInChildren<Image>();
        while (images != null && images.Length > 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.white;
            }
            break;
        }

    }
}
