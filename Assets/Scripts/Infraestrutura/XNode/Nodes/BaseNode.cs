using UnityEngine;
using XNode;

namespace Assets.Scripts.Infraestrutura.Nodes
{
	// Nó base para outros nós..
	public abstract class BaseNode : Node {

		public virtual string GetString()
		{
			return null;
		}

		public virtual Sprite GetSprite()
		{
			return null;
		}
	}
}