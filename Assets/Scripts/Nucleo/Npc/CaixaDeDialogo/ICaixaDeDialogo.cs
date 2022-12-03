namespace Assets.Scripts.Npc.CaixaDeDialogo
{
    // Responsável pela configuração da caixa de diálogo do canvas.
    public interface ICaixaDeDialogo
    {
        //Desabilita a caixa de dialogo para o player.
        public void DesaparecerComACaixaDeDialogo();

        //Habilita a caixa de dialogo para o player.
        public void AparecerComACaixaDeDialogo();
    
    }
}