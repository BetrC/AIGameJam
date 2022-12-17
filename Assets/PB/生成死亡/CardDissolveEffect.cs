using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CardDissolveEffect : MonoBehaviour
{
    public Material effectMat;

    public float duration = 0.2f;


    public ParticleSystem spawnPar;


    public Image dissolveCarUIRect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetMatEffect();
        }
    }

    public void SetMatEffect()
    {
        StartCoroutine(CaptureByUI(dissolveCarUIRect.rectTransform));

  
    }

    public IEnumerator CaptureByUI(RectTransform UIRect)
    {
        //等待帧画面渲染结束
        yield return new WaitForEndOfFrame();
       
        int width = (int)(UIRect.rect.width);
        int height = (int)(UIRect.rect.height);

        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);

        //左下角为原点（0, 0）
        float leftBtmX = UIRect.transform.position.x + UIRect.rect.xMin;
        float leftBtmY = UIRect.transform.position.y + UIRect.rect.yMin;

        //从屏幕读取像素, leftBtmX/leftBtnY 是读取的初始位置,width、height是读取像素的宽度和高度
        tex.ReadPixels(new Rect(leftBtmX, leftBtmY, width, height), 0, 0);
        //执行读取操作
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        //保存
        System.IO.File.WriteAllBytes("1.png", bytes);

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        UIRect.GetComponent<Image>().sprite = sprite;
        //byte[] bytes = tex.EncodeToPNG();
        ////保存
        //System.IO.File.WriteAllBytes(mFileName, bytes);
        spawnPar.Play();
        effectMat.SetFloat("_FadeAmount", 0.08f);
        effectMat.DOFloat(0.9f, "_FadeAmount", duration).SetEase(Ease.Linear);

        effectMat.SetFloat("_GreyscaleBlend", 0f);
        effectMat.DOFloat(1, "_GreyscaleBlend", duration / 4).SetEase(Ease.Linear);
        effectMat.SetFloat("_ShakeUvSpeed", 20f);
        DOVirtual.DelayedCall(0.5f, () => {
            spawnPar.Stop();
            effectMat.SetFloat("_ShakeUvSpeed", 0f);
        });

    }

}
