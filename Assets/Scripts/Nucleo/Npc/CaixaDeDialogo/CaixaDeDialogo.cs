using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Npc.CaixaDeDialogo
{
    // Responsável pela configuração da caixa de diálogo do canvas.
    public class CaixaDeDialogo : MonoBehaviour, ICaixaDeDialogo
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
            animator.SetBool("IsOpen", false);
        }

        //Habilita a caixa de dialogo para o player.
        public void AparecerComACaixaDeDialogo()
        {
            animator.SetBool("IsOpen", true);
        }
    }
}