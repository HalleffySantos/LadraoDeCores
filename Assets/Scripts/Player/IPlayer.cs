using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IPlayer
    { 
        Vector3 direcaoMovimento { get; }
        
        bool estaNoChao { get; set; }

        void Morte();

        Vector3 GetPosicao();

    }
}