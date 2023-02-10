using Assets.Scripts.Player;
using UnityEngine;

public class TriggerInicioJogo : NpcAutomatico
{
    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            if (!PlayerPrefs.HasKey("primeiraAcaoJogo"))
            {
                Dialogo(tObject.GetComponent<IPlayer>());
                PlayerPrefs.SetInt("primeiraAcaoJogo", 1);
            }
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