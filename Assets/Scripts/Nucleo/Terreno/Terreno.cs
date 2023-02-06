using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Terreno : MonoBehaviour, IInteracao, ITerreno
{
    internal SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public virtual void AcaoEntrada(GameObject tObject)
    {
        Debug.Log("Esta no chao");
        var player = tObject.GetComponent<IPlayer>();
        if (player != null )
        {
            if (player.CorDoPlayer() != Color.white && player.CorDoPlayer() != spriteRenderer.color)
            {
                player.Morte();
            }

            player.estaNoChao = true;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public virtual void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            player.estaNoChao = false;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public virtual void AcaoStay(GameObject tObject)
    {

    }

    public void ChangeColor(Color cor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = cor;
    }
}
