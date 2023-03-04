namespace Assets.Scripts.Mapeamento
{
    //Enumerador para mapear as variáveis, com o nome delas usadas no save.
    public class GameManagerMap
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

        private GameManagerMap(string value) { Value = value; }

        // Verifica se a cor amarela foi encontrada.
        public static GameManagerMap AmareloEncontrado { get { return new GameManagerMap("AmareloEncontrado"); } }

        //Verifica se o jogo está salvo.
        public static GameManagerMap GameSaved { get { return new GameManagerMap("GameSaved"); } }
    }
}