using UnityEngine;
using UnityEngine.Networking;

public class PokemonGetView : MonoBehaviour
{
    public UIPokemonCard card;

    public void Init(PokemonData data)
    {
        card.SetData(data);
    }
    
    public void Close()
    {
        Destroy(gameObject);
    }
}