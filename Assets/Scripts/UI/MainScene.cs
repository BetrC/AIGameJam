using System;
using UnityEngine;
public class MainScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowMainUI();
    }
}