using UnityEngine;

// 寻找对局
public class SearchBattlePanel : UIPanelBase
{
    public float ShakeThreshold = 1f;
    private float oldY = 0;

    private bool IsSearching = false;

    // 震动检测
    void Update ()
    {
        if (IsSearching)
            return;
        float newY = Input.acceleration.y;
        float dy = newY - oldY;
        oldY = newY;
        if (dy > ShakeThreshold)
        {
            // 触发震动
        }
    }
    
    public void SearchBattle()
    {
        
    }
    
    public override void OnShow()
    {
    }

    public override void OnHide()
    {
    }
}