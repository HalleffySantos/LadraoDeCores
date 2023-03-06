namespace Assets.Scripts.Enumeradores
{
    //Enumerador referente ao dicionario de tags criadas na unit interface.
    public class GameObjectsTags
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

        private GameObjectsTags(string value) { Value = value; }

        //Enumerador referente a tag da interface (Main e Pause).
        public static GameObjectsTags InterfaceTag { get { return new GameObjectsTags("PauseInterface"); } }

        //Enumerador referente a tag do player.
        public static GameObjectsTags PlayerTag { get { return new GameObjectsTags("Player"); } }

        //Enumerador referente a tag do terreno.
        public static GameObjectsTags TerrenoTag { get { return new GameObjectsTags("Terreno"); } }

        //Enumerador referente a tag do inimigo.
        public static GameObjectsTags InimigoTag { get { return new GameObjectsTags("Inimigo"); } }

        //Enumerador referente a tag do nodeParser.
        public static GameObjectsTags NodeParserTag { get { return new GameObjectsTags("NodeParser"); } }

        //Enumerador referente a tag da caixaDeDialogo.
        public static GameObjectsTags CaixaDeDialogoTag { get { return new GameObjectsTags("CaixaDeDialogo"); } }

        //Enumerador referente a tag de Npc.
        public static GameObjectsTags NpcTag { get { return new GameObjectsTags("Npc"); } }

        //Enumerador referente a tag de ItemJumpManager.
        public static GameObjectsTags ItemJumpManagerTag { get { return new GameObjectsTags("ItemJumpManager"); } }

        //Enumerador referente a tag do GameManager.
        public static GameObjectsTags GameManagerTag { get { return new GameObjectsTags("GameManager"); } }

        //Enumerador referente a tag do BossAve.
        public static GameObjectsTags BossAveTag { get { return new GameObjectsTags("BossAve"); } }

        //Enumerador referente a tag da camera.
        public static GameObjectsTags CameraTag { get { return new GameObjectsTags("MainCamera"); } }

        //Enumerador referente a tag da cutscene.
        public static GameObjectsTags CutsceneTag { get { return new GameObjectsTags("Cutscene"); } }

        //Enumerador referente a tag dos botões.
        public static GameObjectsTags ButtonTag { get { return new GameObjectsTags("Button"); } }

        //Enumerador referente a tag da caixaDeDialogoPergaminho.
        public static GameObjectsTags CaixaPergaminhoTag { get { return new GameObjectsTags("CaixaDeDialogoPergaminho"); } }

        //Enumerador referente a tag do nodeParserPergaminho.
        public static GameObjectsTags NodeParserPergaminhoTag { get { return new GameObjectsTags("NodeParserPergaminho"); } }
    }
}