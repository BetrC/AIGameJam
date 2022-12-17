using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public Text content;

    public void Begin(string text)
    {
        content.text = text;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(transform.position + new Vector3(0, 3, 0), 0.5f)).AppendInterval(1f)
            .AppendCallback(Release);
    }

    private void Release()
    {
        Destroy(gameObject);
    }
}