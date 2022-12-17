using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource bgMusic;

    public void ChangeBgMusic(string music)
    {
        var clip = Resources.Load<AudioClip>($"Sound/{music}");
        bgMusic.clip = clip;
        bgMusic.Play();
    }
    
    public void PlaySound(string sound)
    {
        var prefab = Utility.GetPrefab("SoundSource");
        var player = Instantiate(prefab).GetComponent<SoundPlayer>();
        player.PlaySound(sound);
    }
}