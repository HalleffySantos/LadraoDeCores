namespace Assets.Scripts.Enumeradores.Animacoes
{
    //Enumerador referente as variávies para controle das animações de um player.
    public class TriggersAnimacaoPlayer
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

         private TriggersAnimacaoPlayer(string value) { Value = value; }

        //Enumerador referente ao movimento de andar.
        public static TriggersAnimacaoPlayer Andar { get { return new TriggersAnimacaoPlayer("EstaAndando"); } }

        //Enumerador referente ao movimento de pular.
        public static TriggersAnimacaoPlayer Pular { get { return new TriggersAnimacaoPlayer("EstaPulando"); } } 

        //Enumerador referente ao movimento de pular para cima.
        public static TriggersAnimacaoPlayer PuloUp { get { return new TriggersAnimacaoPlayer("EstaPulandoCima"); } } 

        //Enumerador referente ao movimento de pular para baixo.
        public static TriggersAnimacaoPlayer PuloDown { get { return new TriggersAnimacaoPlayer("EstaPulandoBaixo"); } } 

        //Enumerador referente a morte do player.
        public static TriggersAnimacaoPlayer Morte { get { return new TriggersAnimacaoPlayer("Morte"); } } 

        //Enumerador referente ao movimento de ataque.
        public static TriggersAnimacaoPlayer Ataque { get { return new TriggersAnimacaoPlayer("Ataque"); } }

        //Enumerador referente ao movimento de escalar.
        public static TriggersAnimacaoPlayer Escalar { get { return new TriggersAnimacaoPlayer("Escalar"); } }
    }
}