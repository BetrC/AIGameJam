using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        var clip = Resources.Load<AudioClip>($"Sound/{sound}");
        source.PlayOneShot(clip, Random.Range(0.9f, 1f));
        var len = clip.length;
        DOVirtual.DelayedCall(len, (() =>
        {
            Destroy(gameObject);
        }));
    }
}