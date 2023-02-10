using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

public class GameSaver : NpcAutomatico
{
    private IPlayer player;

    public override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
    }

    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            if (!PlayerPrefs.HasKey("primeiroDialogoPlayerSave"))
            {
                Dialogo(tObject.GetComponent<IPlayer>());
                PlayerPrefs.SetInt("primeiroDialogoPlayerSave", 1);
            }

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