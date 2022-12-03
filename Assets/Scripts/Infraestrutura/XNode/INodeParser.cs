namespace Assets.Scripts.Infraestrutura
{
    // Responsável por passar pelos nós de um diálogo em tempo de execução.
    public interface INodeParser
    {
        // Dialogo, se existir, comeca a ser executado.
        public void ExecuteDialogo(DialogueGraph dialogueGraph);
    }
}