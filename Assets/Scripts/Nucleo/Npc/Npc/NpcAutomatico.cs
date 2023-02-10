using Assets.Scripts.Player;
using UnityEngine;

public class NpcAutomatico : Npc
{
    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            Dialogo(tObject.GetComponent<IPlayer>());
        }
    }

    public override void AcaoSaida(GameObject tObject)
    {
        return;
    }

    public override void AcaoStay(GameObject tObject)
    {
        return;
    }
}