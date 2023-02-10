using System.Collections;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Infraestrutura.Nodes;
using Assets.Scripts.Npc.CaixaDeDialogo;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Assets.Scripts.Infraestrutura
{
    // Responsável por passar pelos nós de um diálogo em tempo de execução.
    public class NodeParser : MonoBehaviour, INodeParser
    {
        public TextMeshProUGUI speaker;

        public TextMeshProUGUI dialogue;

        private Coroutine _parser;

        private ICaixaDeDialogo caixaDeDialogo;

        private IPlayer player;

        private DialogueGraph graph;

        private bool isTypeSentenceInUse;

        // Start is called before the first frame update 
        void Start()
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
            caixaDeDialogo = GameObject.FindGameObjectWithTag(GameObjectsTags.CaixaDeDialogoTag.Value).GetComponent<ICaixaDeDialogo>();
            isTypeSentenceInUse = false;
        }

        // Dialogo, se existir, comeca a ser executado.
        public void ExecuteDialogo(DialogueGraph dialogueGraph)
        {
            if (dialogueGraph == null)
            {
                return;
            }

            graph = dialogueGraph;

            foreach(BaseNode b in graph.nodes)
            {
                graph.current = b;

                if (b.GetString() == "Start")
                {
                    graph.current = b;
                    break;
                }
            }

            _parser = StartCoroutine(ParseNode());
        }

        // Chamada do proximo nó pela porta de saida (exit).
        public void NextNode(string fieldName)
        {
            //Find the port with this name
            if (_parser != null)
            {
                StopCoroutine(_parser);
                _parser = null;
            }

            //Check if this port is the one we're looking for
            foreach(NodePort p in graph.current.Ports)
            {
                if (p.fieldName == fieldName)
                {
                    graph.current = p.Connection.node as BaseNode;
                    break;
                }
            }

            _parser = StartCoroutine(ParseNode());
        }

        private IEnumerator ParseNode()
        {
            BaseNode b = graph.current;
            string data = b.GetString();

            string[] dataParts = data.Split('/');

            switch (dataParts[0])
            {
                case "Start":
                    NextNode("exit");
                    break;
                
                case "DialogueNode":
                    speaker.text = dataParts[1];
                    StartCoroutine(TypeSentence(dataParts[2]));

                    yield return new WaitUntil(() => !isTypeSentenceInUse);

                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
                    
                    NextNode("exit");
                    break;
                
                case "ConditionalNode":
                    int saida = int.Parse(dataParts[1]);
                
                    switch (saida)
                    {
                        case 2:
                            NextNode("exit2");
                            break;

                        case 3:
                            NextNode("exit3");
                            break;

                        case 4:
                            NextNode("exit4");
                            break;

                        case 5:
                            NextNode("exit5");
                            break;

                        default:
                            NextNode("exit1");
                            break;
                    }
                    break;
                
                case "End":
                    TerminarDialogo();
                    break;

                
                default:
                    break;
            }
            
        }

        private void TerminarDialogo()
        {
            if (caixaDeDialogo != null && player != null)
            {
                caixaDeDialogo.DesaparecerComACaixaDeDialogo();
                player.movimentoHabilitado = true;
            }
        }

        private IEnumerator TypeSentence(string sentence)
        {
            isTypeSentenceInUse = true;
            dialogue.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                dialogue.text += letter;
                yield return new WaitForSeconds(.015f);
            }

            isTypeSentenceInUse = false;
        }
    }
}