namespace Assets.Scripts.Infraestrutura.Nodes
{
	// Nó que reprensenta o final de um dialogo.
	public class EndNode : BaseNode 
	{
		[Input] public int entry;

		public override string GetString()
		{
			return "End";
		}

	}
}