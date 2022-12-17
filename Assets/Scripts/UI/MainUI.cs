using UI;
using UnityEngine;

public class MainUI : UIPanelBase
{
    public override void OnShow()
    {
    }

    public override void OnHide()
    {
    }

    public void OnClickChargeButton()
    {
        UIManager.Instance.ShowPanel(typeof(ChargePanel));
    }

    public void OnClickDrawCardButton()
    {
        UIManager.Instance.ShowPanel(typeof(DrawCardPanel));
    }

    public void OnClickBattleButton()
    {
        UIManager.Instance.ShowPanel(typeof(SearchBattlePanel));
    }

    public void OnClickBagButton()
    {
        UIManager.Instance.ShowPanel(typeof(BagPanel));
    }

    public void OnClickMallButton()
    {
        UIManager.Instance.ShowPanel(typeof(MallPanel));
    }
}