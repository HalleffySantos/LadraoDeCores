namespace Assets.Scripts.Enumeradores
{
    public class GameObjectsTags
    {
        public string Value { get; private set; }

        private GameObjectsTags(string value) { Value = value; }

        public static GameObjectsTags PlayerTag { get { return new GameObjectsTags("Player"); } }
        public static GameObjectsTags TerrenoTag { get { return new GameObjectsTags("Terreno"); } }
        public static GameObjectsTags InimigoTag { get { return new GameObjectsTags("Inimigo"); } }
    }
}