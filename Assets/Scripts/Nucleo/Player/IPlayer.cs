using UnityEngine;

namespace Assets.Scripts.Player
{
    // Interface referente ao Player.
    public interface IPlayer
    { 
        // Recebe a direção que o player está se movendo.
        Vector3 direcaoMovimento { get; }
        
        // Recebe e insere se o player está no chão ou não.
        bool estaNoChao { get; set; }

        // Recebe e insere se o player pode ou não se mover.
        bool movimentoHabilitado { get; set; }

        // Mata o player.
        void Morte();

        // Recebe a posição do player.
        Vector3 GetPosicao();

    }
}