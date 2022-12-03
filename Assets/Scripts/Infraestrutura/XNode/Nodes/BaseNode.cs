using UnityEngine;
using XNode;

namespace Assets.Scripts.Infraestrutura.Nodes
{
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