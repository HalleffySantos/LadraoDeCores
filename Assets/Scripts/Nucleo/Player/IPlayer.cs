using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IPlayer
    { 
        Vector3 direcaoMovimento { get; }
        
        bool estaNoChao { get; set; }

        bool movimentoHabilitado { get; set; }

        void Morte();

        Vector3 GetPosicao();

    }
}