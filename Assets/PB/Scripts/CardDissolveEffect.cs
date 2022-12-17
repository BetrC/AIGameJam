using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CardDissolveEffect : MonoBehaviour
{
    private Material effectMat;

    public float duration = 1f;


    //public ParticleSystem spawnPar;


    public GameObject testObj;

    private void Awake()
    {
        effectMat = (Material)Resources.Load("CardDissolve");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetDeadEffect(testObj);
        }
    }

    public void SetDeadEffect(GameObject targetObj)
    {
        Material mat = new Material(effectMat.shader);
        mat.CopyPropertiesFromMaterial(effectMat);
        Image[] images = targetObj.GetComponentsInChildren<Image>();
        Text[] texts = targetObj.GetComponentsInChildren<Text>();

        for (int i = 0; i < images.Length; i++)
        {
            images[i].material = mat;

        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].material = mat;
        }
    
        mat.SetFloat("_FadeAmount", 0.08f);
        mat.DOFloat(0.9f, "_FadeAmount", duration).SetEase(Ease.Linear);

        mat.SetFloat("_GreyscaleBlend", 0f);
        mat.DOFloat(1, "_GreyscaleBlend", duration / 4).SetEase(Ease.Linear);
        mat.SetFloat("_ShakeUvSpeed", 20f);
        DOVirtual.DelayedCall(0.5f, () => {

            mat.SetFloat("_ShakeUvSpeed", 0f);
        });


        GameObject impactGo = Instantiate((GameObject)Resources.Load("VFX/VFX_DeadEffect"),targetObj.transform);
        impactGo.transform.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y, targetObj.transform.position.z );
    }



}
