using DG.Tweening;
using UnityEngine.UI;

public class ChargePanel : UIPanelBase
{
    public Image faceImg;

    public override void OnShow()
    {
        var seq = DOTween.Sequence();
        seq.AppendInterval(1f).AppendCallback(() =>
            {
                faceImg.gameObject.SetActive(true);
            }).AppendInterval(2f)
            .AppendCallback((() =>
            {
                GameManager.Instance.Coin += 648;
                Utility.ShowHint("获得648游戏币");
                faceImg.gameObject.SetActive(false);
                Close();
            }));
    }

    public override void OnHide()
    {
    }
}