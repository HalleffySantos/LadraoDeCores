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
        public int ultimoObjetoEmContato { get; }

        // Mata o player.
        void Morte();

        // Recebe a posição do player.
        Vector3 GetPosicao();

        // Inicia a animação de ataque do player.
        void ComecaAnimacaoDeAtaque();

        // Termina a animação de ataque do player.
        public void TerminaAnimacaoDeAtaque();

        // Configurações para o player se prender a uma parede.
        public void ComecaEscalarParede();

        // Configurações para o player parar de se prender a uma parede.
        public void TerminaEscalarParede();

        // Recuo vertical infringido ao player após acertar um inimigo/objeto com a arma.
        public void RecuoVertical();
    }
}