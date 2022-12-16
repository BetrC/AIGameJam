using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public List<UIPanelBase> panels;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPanel(Type panelType)
    {
        foreach (var panel in panels)
        {
            if (panel.GetType() == panelType)
            {
                panel.Show();
            }
            else if (panel.IsShowing)
            {
                panel.Hide();
            }
        }
    }

    public void ShowMainUI()
    {
        ShowPanel(typeof(MainUI));
    }
}