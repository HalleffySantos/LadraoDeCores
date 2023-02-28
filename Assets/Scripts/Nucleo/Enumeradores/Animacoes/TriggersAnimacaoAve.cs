namespace Assets.Scripts.Enumeradores.Animacoes
{
    //Enumerador referente as variávies para controle das animações de um player.
    public class TriggersAnimacaoAve
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

         private TriggersAnimacaoAve(string value) { Value = value; }

        //Enumerador referente ao movimento de ataque.
        public static TriggersAnimacaoAve MovDir { get { return new TriggersAnimacaoAve("MovDir"); } }

    }
}