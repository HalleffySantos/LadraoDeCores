using System.Collections.Generic;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Terreno : MonoBehaviour, IInteracao, ITerreno
{
    internal SpriteRenderer spriteRenderer;

    private IList<IPlayer> playerEmContato;

    // Start is called before the first frame update
    void Start()
    {
        playerEmContato = new List<IPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EstaPlayerEmContatoAoTerreno();
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public virtual void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            playerEmContato.Add(player);
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public virtual void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            playerEmContato.Remove(player);
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

    private void EstaPlayerEmContatoAoTerreno()
    {
        if (playerEmContato != null && playerEmContato.Count > 0)
        {
            var player = playerEmContato[0];
            if (player.CorDoPlayer() != Color.white && player.CorDoPlayer() != spriteRenderer.color)
            {
                player.Morte();
            }

            player.estaNoChao = true;
        }
    }
}
