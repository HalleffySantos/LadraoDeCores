namespace Assets.Scripts.Infraestrutura.Nodes
{
	public class StartNode : BaseNode 
	{
		[Output] public int exit;

		public override string GetString()
		{
			return "Start";
		}

	}
}