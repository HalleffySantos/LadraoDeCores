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

    internal ICaixaDeDialogo caixaDeDialogo;

    internal INodeParser nodeParser;

    internal SpriteRenderer[] caixaExplicativaSprite;

    internal TextMeshPro[] caixaExplicativaTexto;

    // Start is called before the first frame update
    public virtual void Start()
    {
        nodeParser = GameObject.FindGameObjectWithTag(GameObjectsTags.NodeParserTag.Value).GetComponent<INodeParser>();
        caixaDeDialogo = GameObject.FindGameObjectWithTag(GameObjectsTags.CaixaDeDialogoTag.Value).GetComponent<ICaixaDeDialogo>();

        caixaExplicativaSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        caixaExplicativaTexto = gameObject.GetComponentsInChildren<TextMeshPro>();
        DesativaCaixaExplicativa();
    }

    // Metodo para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public virtual void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            AtivaCaixaExplicativa();
        }
    }

    // Metodo para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public virtual void AcaoSaida(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            DesativaCaixaExplicativa();
        }
    }

    // Metodo para realização da ação chamada pelo OnTriggerStay ou OnCollisionStay.
    public virtual void AcaoStay(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null && player.movimentoHabilitado && Input.GetKeyDown(KeyCode.E))
        {
            Dialogo(player);
        }
    }

    // Ativa balão de interação entre o player e o npc.
    internal void AtivaCaixaExplicativa()
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

    // Desativa balão de interação entre o player e o npc.
    internal void DesativaCaixaExplicativa()
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

    // Executa o diálogo entre o player e o npc.
    internal void Dialogo(IPlayer player)
    {
        player.movimentoHabilitado = false;
        Debug.Log(caixaDeDialogo.ToString());
        caixaDeDialogo.AparecerComACaixaDeDialogo();
        nodeParser.ExecuteDialogo(dialogueGraph);
    }
}
 