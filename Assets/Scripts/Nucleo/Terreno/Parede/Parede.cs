using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null && gameObject.GetInstanceID() != player.ultimoObjetoEmContato)
        {
            if (player.CorDoPlayer() != Color.white && player.CorDoPlayer() != spriteRenderer.color)
            {
                player.Morte();
            }

            player.estaNoChao = true;
            player.ComecaEscalarParede();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            player.estaNoChao = false;
            player.TerminaEscalarParede();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
        
    }
}
