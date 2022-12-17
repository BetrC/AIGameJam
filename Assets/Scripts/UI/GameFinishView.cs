using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishView : MonoBehaviour
{
    public Text finishText;
    public Text scoreText;

    public void Show(bool win, int score)
    {
        finishText.text = win ? "你获得了胜利!" : "你被击败了...";
        scoreText.text = win ? $"获得{score}游戏币" : "";
        if (win)
            GameManager.Instance.Coin += score;
        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(0);
    }
}