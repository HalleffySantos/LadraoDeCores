using Assets.Scripts.Enumeradores;
using Assets.Scripts.Infraestrutura;
using Assets.Scripts.Interacao;
using Assets.Scripts.Npc.CaixaDeDialogo;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour, IInteracao
{
    public DialogueGraph dialogueGraph;

    private INodeParser nodeParser;

    private ICaixaDeDialogo caixaDeDialogo;

    private SpriteRenderer[] caixaExplicativaSprite;

    private TextMeshPro[] caixaExplicativaTexto;

    // Start is called before the first frame update
    void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag(GameObjectsTags.NodeParserTag.Value).GetComponent<INodeParser>();
        caixaDeDialogo = GameObject.FindGameObjectWithTag(GameObjectsTags.CaixaDeDialogoTag.Value).GetComponent<ICaixaDeDialogo>();

        caixaExplicativaSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        caixaExplicativaTexto = gameObject.GetComponentsInChildren<TextMeshPro>();
        DesativaCaixaExplicativa();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            AtivaCaixaExplicativa();
        }
    }

    public void AcaoSaida(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            DesativaCaixaExplicativa();
        }
    }

    public void AcaoStay(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null && player.movimentoHabilitado && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Trigou Stay");
            player.movimentoHabilitado = false;
            caixaDeDialogo.AparecerComACaixaDeDialogo();
            nodeParser.ExecuteDialogo(dialogueGraph);
        }
    }

    private void AtivaCaixaExplicativa()
    {
        foreach (var sprite in caixaExplicativaSprite)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }

        foreach (var texto in caixaExplicativaTexto)
        {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);
        }
    }

    private void DesativaCaixaExplicativa()
    {
        foreach (var sprite in caixaExplicativaSprite)
        {
            if (sprite.CompareTag(GameObjectsTags.NpcTag.Value))
            {
                continue;
            }

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }

        foreach (var texto in caixaExplicativaTexto)
        {
            if (texto.CompareTag(GameObjectsTags.NpcTag.Value))
            {
                continue;
            }

            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
        }
    }
}
 