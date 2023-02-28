using Assets.Scripts.Player;
using UnityEngine;

// Script referente ao ItemJump.
public class ItemJump : Terreno
{
    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            if (player.CorDoPlayer().a != corTerreno.a || player.CorDoPlayer().r != corTerreno.r || player.CorDoPlayer().g != corTerreno.g || player.CorDoPlayer().b != corTerreno.b)
            {
                tObject.GetComponent<IPlayer>().Morte();
            }

            player.estaNoChao = true;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {                
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
        
    }
}
