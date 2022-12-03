using UnityEngine;

namespace Assets.Scripts.Infraestrutura.Nodes
{
	public class DialogueNode : BaseNode {

		[Input] public int entry;

		[Output] public int exit;
		
		public string speakerName;
		
		public string dailogueLine;
		
		public Sprite sprite;

		public override string GetString()
		{
			return "DialogueNode/" + speakerName + "/" + dailogueLine;
		}

		public override Sprite GetSprite()
		{
			return sprite;
		}
	}
}