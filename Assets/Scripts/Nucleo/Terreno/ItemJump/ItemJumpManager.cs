using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Nucleo.Terreno.ItemJump
{
    // Script responsável por gerenciar a criação dos itensJump de uma cena.
    public class ItemJumpManager : MonoBehaviour, IItemJumpManager
    {
        // Prefab do item jump.
        public GameObject itemJumpPrefab;
        
        // Posicoes referentes aos itens prefabs da cena.
        // Deve ter a mesma quantidade de elementos que PosicaoYPrefabs.
        // O indice de um elemento nessa lista se refere ao mesmo elemento
        public List<float> PosicaoXPrefabs;

        public List<float> PosicaoYPrefabs;

        private IList<ItemJumpConfig> ItemJumpListaGeral;

        private IList<ItemJumpConfig> itemJumpListaInstanciados;

        // Start is called before the first frame update
        void Start()
        {
            ItemJumpListaGeral = new List<ItemJumpConfig>();
            itemJumpListaInstanciados = new List<ItemJumpConfig>();

            for (int i = 0; i < PosicaoXPrefabs.Count(); i++)
            {
                ConvertaPosicaoParaItemJumpConfig(PosicaoXPrefabs[i], PosicaoYPrefabs[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {
            InstanciaItensJump();
        }

        public void ItemDestruido(float posX, float posY)
        {
            itemJumpListaInstanciados.Remove(itemJumpListaInstanciados.Where(x => x.PosicaoX == posX && x.PosicaoY == posY).FirstOrDefault());
            ItemJumpListaGeral.FirstOrDefault(x => x.PosicaoX == posX && x.PosicaoY == posY).CouldownTime = 4.0f;
        }

        private void ConvertaPosicaoParaItemJumpConfig(float posX, float posY)
        {
            ItemJumpListaGeral.Add(new ItemJumpConfig(0, posX, posY));
        }

        private void InstanciaItensJump()
        {
            foreach (var item in ItemJumpListaGeral)
            {
                if (!itemJumpListaInstanciados.Where(x => x.PosicaoX == item.PosicaoX && x.PosicaoY == item.PosicaoY).Any())
                {
                    if (item.CouldownTime <= 0)
                    {
                        Instantiate(itemJumpPrefab, new Vector3(item.PosicaoX, item.PosicaoY, 0), Quaternion.identity);
                        itemJumpListaInstanciados.Add(item);
                    }
                    
                    else
                    {
                        item.CouldownTime -= Time.deltaTime;
                    }
                }
            }
        }
    }

}