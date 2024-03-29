using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Npc.CaixaDeDialogo
{
    // Responsável pela configuração da caixa de diálogo do canvas.
    public class CaixaPergaminho : MonoBehaviour, ICaixaDeDialogo
    {
        private SpriteRenderer[] sprites;

        private TextMeshProUGUI[] textos;

        private Image[] imagens;

        private Animator animator;


        // Start is called before the first frame update
        void Start()
        {
            sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
            textos = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
            imagens = gameObject.GetComponentsInChildren<Image>();
            animator = gameObject.GetComponent<Animator>();

            DesaparecerComACaixaDeDialogo();
        }
        
        //Desabilita a caixa de dialogo para o player.
        public void DesaparecerComACaixaDeDialogo()
        {

            foreach (var sprite in sprites)
            {
                //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                sprite.enabled = false;
            }

            foreach (var texto in textos)
            {
                //texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
                texto.enabled = false;
            }

            foreach (var imagem in imagens)
            {
                //imagem.color = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 0);
                imagem.enabled = false;
            }
        }

        //Habilita a caixa de dialogo para o player.
        public void AparecerComACaixaDeDialogo()
        {
            foreach (var sprite in sprites)
            {
                //sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
                sprite.enabled = true;
            }

            foreach (var texto in textos)
            {
                //texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);
                texto.enabled = true;
            }

            foreach (var imagem in imagens)
            {
                //imagem.color = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 1);
                imagem.enabled = true;
            }
        }
    }
}