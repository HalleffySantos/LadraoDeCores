namespace Assets.Scripts.Enumeradores.Animacoes
{
    //Enumerador referente as variávies para controle das animações da cutscene de início.
    public class TriggersAnimacaoCutsceneInicio
    {
        //É o valor de uma tag.
        public string Value { get; private set; }

         private TriggersAnimacaoCutsceneInicio(string value) { Value = value; }

        //Enumerador referente ao movimento de ataque.
        public static TriggersAnimacaoCutsceneInicio FinalCutscene { get { return new TriggersAnimacaoCutsceneInicio("FinalCutscene"); } }

    }
}