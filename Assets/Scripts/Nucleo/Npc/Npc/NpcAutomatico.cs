using Assets.Scripts.Player;
using UnityEngine;

public class NpcAutomatico : Npc
{
    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter. 
    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            Dialogo(tObject.GetComponent<IPlayer>());
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {
        return;
    }

    // Método para realização da ação chamada pelo OnTriggerStay ou OnCollisionStay.
    public override void AcaoStay(GameObject tObject)
    {
        return;
    }
}