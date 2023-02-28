using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terreno : MonoBehaviour, IInteracao, ITerreno
{
    internal SpriteRenderer spriteRenderer;

    private IList<IPlayer> playerEmContato;

    private IGameManager gameManager;

    private Color cinza;
    private Color amarelo;

    private bool amareloPrimeiroEncontro;

    internal Color corTerreno;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();

        playerEmContato = new List<IPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        cinza = gameManager.Cinza;
        amarelo = gameManager.Amarelo;

        amareloPrimeiroEncontro = false;

        if (gameObject.GetComponent<Tilemap>() != null)
        {
            corTerreno = gameObject.GetComponent<Tilemap>().color;
        }

        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            corTerreno = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        EstaPlayerEmContatoAoTerreno();
        TrocaCorTerreno();
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public virtual void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            if (player.CorDoPlayer() != corTerreno)
            {
                tObject.GetComponent<IPlayer>().Morte();
            }

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
            player.estaNoChao = true;
        }
    }

    private void TrocaCorTerreno()
    {
        if (gameManager != null && gameManager.AmareloEncontrado && !amareloPrimeiroEncontro && gameObject.tag.CompareTo(GameObjectsTags.TerrenoTag.Value) == 0)
        {
            amareloPrimeiroEncontro = true;

            if (gameObject.GetComponent<Tilemap>() != null)
            {
                if (Random.Range(0, 10000)%2 == 0)
                {
                    gameObject.GetComponent<Tilemap>().color = amarelo;
                }
                else
                {
                    gameObject.GetComponent<Tilemap>().color = cinza;
                }
                corTerreno = gameObject.GetComponent<Tilemap>().color;
            }

            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                if (Random.Range(0, 10000)%2 == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = amarelo;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = cinza;
                }
                corTerreno = gameObject.GetComponent<SpriteRenderer>().color;
            }
        }       
    }
    
}
