namespace Assets.Scripts.Infraestrutura.Nodes
{
	public class EndNode : BaseNode 
	{
		[Input] public int entry;
        
        [Output] public int exit;

		public override string GetString()
		{
			return "End";
		}

	}
}