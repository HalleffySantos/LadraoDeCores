using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

public class Parede : Terreno
{
    private IPlayer player;

    void Start()
    {

        
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoEntrada(GameObject tObject)
    {
        var playerRec = tObject.GetComponent<IPlayer>();
        if (playerRec != null && gameObject.GetInstanceID() != playerRec.ultimoObjetoEmContato)
        {
            if (playerRec.CorDoPlayer() != Color.white && playerRec.CorDoPlayer() != spriteRenderer.color)
            {
                playerRec.Morte();
            }

            playerRec.estaNoChao = true;
            playerRec.ComecaEscalarParede();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {
        var playerRec = tObject.GetComponent<IPlayer>();
        if (playerRec != null)
        {
            playerRec.estaNoChao = false;
            playerRec.TerminaEscalarParede();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
    }
}
