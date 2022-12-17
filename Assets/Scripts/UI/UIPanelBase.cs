using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public abstract class UIPanelBase : MonoBehaviour
{
    public abstract void OnShow();
    public abstract void OnHide();

    [HideInInspector]
    public bool IsShowing = false;
    public void Show()
    {
        IsShowing = true;
        gameObject.SetActive(true);
        OnShow();
    }
    
    public void Hide()
    {
        IsShowing = false;
        gameObject.SetActive(false);
        OnHide();
    }

    public void Close()
    {
        AudioManager.Instance.PlaySound("clickButton");
        UIManager.Instance.ShowPanel(typeof(MainUI));
    }
}