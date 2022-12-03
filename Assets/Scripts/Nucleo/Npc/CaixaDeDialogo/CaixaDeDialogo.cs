using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Npc.CaixaDeDialogo
{
    // Responsável pela configuração da caixa de diálogo do canvas.
    public class CaixaDeDialogo : MonoBehaviour, ICaixaDeDialogo
    {
        private SpriteRenderer[] sprites;

        private Text[] textos;


        // Start is called before the first frame update
        void Start()
        {
            sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
            textos = gameObject.GetComponentsInChildren<Text>();

            DesaparecerComACaixaDeDialogo();
        }
        
        //Desabilita a caixa de dialogo para o player.
        public void DesaparecerComACaixaDeDialogo()
        {
            foreach(var sprite in sprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            }

            foreach(var texto in textos)
            {
                texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
            }
        }

        //Habilita a caixa de dialogo para o player.
        public void AparecerComACaixaDeDialogo()
        {
            foreach(var sprite in sprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
            }

            foreach(var texto in textos)
            {
                texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1.0f);
            }
        }
    }
}