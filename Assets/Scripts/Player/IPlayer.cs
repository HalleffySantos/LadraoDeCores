using UnityEngine;

namespace Assets.Scripts.Interacao
{
    public interface IPlayer
    { 
        bool estaNoChao { get; set; }

        void Morte();

        Vector3 GetPosicao();
    }
}