namespace Assets.Scripts.Enumeradores.Animacoes
{
    //Enumerador referente as variávies para controle das animações de um player.
    public class TriggersAnimacaoArma
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

         private TriggersAnimacaoArma(string value) { Value = value; }

        //Enumerador referente ao movimento de ataque.
        public static TriggersAnimacaoArma Ataque { get { return new TriggersAnimacaoArma("Ataque"); } }

    }
}