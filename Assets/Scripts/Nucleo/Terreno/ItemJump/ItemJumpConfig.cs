namespace Assets.Scripts.Nucleo.Terreno.ItemJump
{
    // Script referente as configurações para criação de um itemJump.
    public class ItemJumpConfig
    {
        public float CouldownTime { get; set; }

        public float PosicaoX { get; private set; }

        public float PosicaoY { get; private set; }

        public ItemJumpConfig(float couldown, float posX, float posY)
        {
            CouldownTime = couldown;
            PosicaoX = posX;
            PosicaoY = posY;
        }

    }
}
