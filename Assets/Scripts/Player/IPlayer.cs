using UnityEngine;

namespace Assets.Scripts.Interacao
{
    public interface IPlayer
    { 
        public bool estaNoChao { get; set; }

        public void Morte();

        public Vector3 GetPosicao();
    }
}