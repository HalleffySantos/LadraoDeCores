using UnityEngine;

namespace Assets.Scripts.Infraestrutura.Nodes
{
	// Nó conditiocional para um diálogo.
	public class ConditionalNode : BaseNode {

		[Input] public int entry;

		[Output] public int exit1;

        [Output] public int exit2;

        [Output] public int exit3;

        [Output] public int exit4;

        [Output] public int exit5;
		
        public int escolhaSaida;

		public override string GetString()
		{
			return "ConditionalNode/" + escolhaSaida;
		}
	}
}