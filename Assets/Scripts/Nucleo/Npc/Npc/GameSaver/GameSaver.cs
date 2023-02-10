using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

public class GameSaver : NpcAutomatico
{
    private IPlayer player;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
    }

    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            //Dialogo(tObject.GetComponent<IPlayer>());
            player.SaveGame();
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