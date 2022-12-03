
using Assets.Scripts.Infraestrutura.Nodes;
using UnityEngine;
using XNode;

// Script do grafo de um diálogo.
namespace Assets.Scripts.Infraestrutura
{
	[CreateAssetMenu]
	public class DialogueGraph : NodeGraph 
	{ 
		public BaseNode current;

	}
}