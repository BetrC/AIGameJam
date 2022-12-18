using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource bgMusic;

    public void ChangeBgMusic(string music)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(bgMusic.DOFade(0.4f, .5f)).AppendInterval(.5f).AppendCallback((() =>
        {
            var clip = Resources.Load<AudioClip>($"Sound/{music}");
            bgMusic.clip = clip;
            bgMusic.Play();
            bgMusic.DOFade(1f, .5f);
        }));
    }
    
    public void PlaySound(string sound)
    {
        var prefab = Utility.GetPrefab("SoundSource");
        var player = Instantiate(prefab).GetComponent<SoundPlayer>();
        player.PlaySound(sound);
    }
}