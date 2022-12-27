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

        // Recebe o id do ultimo objeto que o player esteve em contato.
        int ultimoObjetoEmContato { get; }

        // Mata o player.
        void Morte();

        // Recebe a posição do player.
        Vector3 GetPosicao();

        // Inicia a animação de ataque do player.
        void ComecaAnimacaoDeAtaque();

        // Termina a animação de ataque do player.
        void TerminaAnimacaoDeAtaque();

        // Configurações para o player se prender a uma parede.
        void ComecaEscalarParede();

        // Configurações para o player parar de se prender a uma parede.
        void TerminaEscalarParede();

        // Recuo vertical infringido ao player após acertar um inimigo/objeto com a arma.
        void RecuoVertical();

        // Instance id do terreno que o player está em contato.
        int TerrenoEmContato();

        // A cor atual do player.
        Color CorDoPlayer();
    }
}