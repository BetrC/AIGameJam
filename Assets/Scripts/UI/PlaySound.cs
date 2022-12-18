using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.PlaySound("buy");
    }
}