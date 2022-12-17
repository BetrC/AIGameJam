using UnityEngine;
using UnityEngine.UI;

public class UIPokemonCard : MonoBehaviour
{
    public Text nameText;
    public Text raceText;
    public Text descriptionText;
    public Text attackText;
    public Text defenseText;
    public Text rarityText;
    public Text healthText;

    public Image icon;

    public void SetData(PokemonData data)
    {
        nameText.text = data.Name;
        raceText.text = data.Race;
        descriptionText.text = data.Description;
        attackText.text = data.Attack.ToString();
        defenseText.text = data.Defence.ToString();
        rarityText.text = data.Level.ToString();
        healthText.text = data.HP.ToString();

        WebDownloader.Instance.DownloadImage("http://43.153.64.79:4567/get_pokemon_img?pokemon_id=" + data.id,
            data.id.ToString(),
            (sprite) =>
            {
                icon.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), Vector2.zero);
            });
    }
}