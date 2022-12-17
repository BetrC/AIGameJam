using System;
using UnityEngine;
using UnityEngine.UI;

public enum CardQuality
{
    N,
    R,
    SR,
    SSR,
}

public class UIPokemonCard : MonoBehaviour
{
    [Header("卡牌品质底图")] public Sprite NSprite;
    public Sprite RSprite;
    public Sprite SRSprite;
    public Sprite SSRSprite;

    [Header("UI控件")] public Text nameText;

    // public Text raceText;
    public Text descriptionText;
    public Text attackText;
    public Text defenseText;
    public Text levelText;
    public Text healthText;

    public Image qualityImage;
    public Image icon;

    public void SetData(PokemonData data)
    {
        gameObject.SetActive(data != null);
        if (data == null)
            return;
        nameText.text = data.Monster;
        // raceText.text = data.Rarity;
        descriptionText.text = data.Description;
        attackText.text = data.Attack.ToString();
        defenseText.text = data.Defense.ToString();
        levelText.text = data.Level.ToString();
        healthText.text = data.HP.ToString();
        var quality = Utility.GetCardQuality(data.Rarity);
        SetQualitySprite(quality);

        WebDownloader.Instance.DownloadImage("http://123.207.251.146:4567/get_pokemon_img?pokemon_id=" + data.ID,
            data.ID.ToString(),
            (sprite) =>
            {
                icon.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), Vector2.zero);
            });
    }

    public void SetQualitySprite(CardQuality quality)
    {
        switch (quality)
        {
            case CardQuality.N:
                qualityImage.sprite = NSprite;
                break;
            case CardQuality.R:
                qualityImage.sprite = RSprite;
                break;
            case CardQuality.SR:
                qualityImage.sprite = SRSprite;
                break;
            case CardQuality.SSR:
                qualityImage.sprite = SSRSprite;
                break;
        }
    }
}