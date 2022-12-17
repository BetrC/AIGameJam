using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public RectTransform UIRect;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            string fileName =  "123.png";
            //系统不识别标点符号，但支持中文
            IEnumerator coroutine = CaptureByUI(UIRect, fileName);
            StartCoroutine(coroutine);
        }

    }
    public IEnumerator CaptureByUI(RectTransform UIRect, string mFileName)
    {
        //等待帧画面渲染结束
        yield return new WaitForEndOfFrame();

        int width = (int)(UIRect.rect.width);
        int height = (int)(UIRect.rect.height);

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        //左下角为原点（0, 0）
        float leftBtmX = UIRect.transform.position.x + UIRect.rect.xMin;
        float leftBtmY = UIRect.transform.position.y + UIRect.rect.yMin;

        //从屏幕读取像素, leftBtmX/leftBtnY 是读取的初始位置,width、height是读取像素的宽度和高度
        tex.ReadPixels(new Rect(leftBtmX, leftBtmY, width, height), 0, 0);
        //执行读取操作
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        //保存
        System.IO.File.WriteAllBytes(mFileName, bytes);

    }
}
