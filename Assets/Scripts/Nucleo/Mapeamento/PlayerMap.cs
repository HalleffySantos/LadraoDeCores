namespace Assets.Scripts.Mapeamento
{
    //Enumerador para mapear as variáveis, com o nome delas usadas no save.
    public class PlayerMap
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

        private PlayerMap(string value) { Value = value; }

        //Posição x do player ao salvar o jogo.
        public static PlayerMap posXPlayer { get { return new PlayerMap("posXPlayer"); } }

        //Posição y do player ao salvar o jogo.
        public static PlayerMap posYPlayer { get { return new PlayerMap("posYPlayer"); } }

        //Cor atual do player ao salvar o jogo.
        public static PlayerMap corPlayer { get { return new PlayerMap("corAtualPlayer"); } }
    }
}