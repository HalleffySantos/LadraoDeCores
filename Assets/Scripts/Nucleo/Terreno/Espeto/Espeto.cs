using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Espeto : Terreno
{
    private IPlayer player;

    // Start is called before the first frame update
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
        if (tObject.GetComponent<IPlayer>() != null)
        {
            tObject.GetComponent<IPlayer>().Morte();
            return;
        }

        if(tObject.GetComponent<IArma>() != null)
        {
            player.RecuoVertical();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {

    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
        if(tObject.GetComponent<IArma>() != null)
        {
            player.RecuoVertical();
        }
    }
}
