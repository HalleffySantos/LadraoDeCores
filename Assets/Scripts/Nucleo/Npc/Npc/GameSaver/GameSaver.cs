using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

public class GameSaver : NpcAutomatico
{
    private IPlayer player;
    private IGameManager gameManager;

    public override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
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
            gameManager.SaveGame();
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