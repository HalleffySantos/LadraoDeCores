namespace Assets.Scripts.Infraestrutura.Nodes
{
	// Nó que representa o início de um dialogo.
	public class StartNode : BaseNode 
	{
		[Output] public int exit;

		public override string GetString()
		{
			return "Start";
		}

	}
}