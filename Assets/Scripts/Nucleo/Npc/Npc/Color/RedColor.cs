using Assets.Scripts.Enumeradores;
using Assets.Scripts.Infraestrutura;
using Assets.Scripts.Npc.CaixaDeDialogo;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

public class RedColor : Npc 
{
    private IGameManager gameManager;

    private IBossAve bossAve;

    // Start is called before the first frame update
    void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag(GameObjectsTags.NodeParserTag.Value).GetComponent<INodeParser>();
        caixaDeDialogo = GameObject.FindGameObjectWithTag(GameObjectsTags.CaixaDeDialogoTag.Value).GetComponent<ICaixaDeDialogo>();

        caixaExplicativaSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        caixaExplicativaTexto = gameObject.GetComponentsInChildren<TextMeshPro>();
        DesativaCaixaExplicativa();
        
        gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();
        bossAve = GameObject.FindGameObjectWithTag(GameObjectsTags.BossAveTag.Value).GetComponent<BossAve>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Metodo para realização da ação chamada pelo OnTriggerStay ou OnCollisionStay.
    public override void AcaoStay(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null && player.movimentoHabilitado && Input.GetKeyDown(KeyCode.E))
        {
            player.movimentoHabilitado = false;
            caixaDeDialogo.AparecerComACaixaDeDialogo();
            
            //gameManager.NovaCorColetada(Color.red);
            
            nodeParser.ExecuteDialogo(dialogueGraph);

            bossAve.ComecaAPerseguir();

            Destroy(gameObject);
        }
    }
}

