using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente a camera.
public class Camera : MonoBehaviour, ICamera
{
    private float topYCameraPlayer;
    private float centerYCameraPlayer;
    private float botYCameraPlayer;
    private float velocidadeVariacao;

    private float offSetAnterior;
    private float offSetEmVariacao;
    private bool emVariacao;

    private bool playerMorreu;
    private bool playerNotFound;

    private Vector3 velocidade;

    // Start is called before the first frame update
    void Start()
    {
        velocidadeVariacao = 0.1f;
        topYCameraPlayer = 2.0f;
        centerYCameraPlayer = 1.0f;
        botYCameraPlayer = -2.0f;

        velocidade = Vector3.zero;

        offSetAnterior = centerYCameraPlayer;
        emVariacao = false;

        playerMorreu = false;
    }

    // Update is called once per frame
    void Update()
    {
        AcompanharMovPlayer();
    }

    public void PlayerMorreu()
    {
        playerMorreu = true;
    }

    // Vincula a camera ao player
    private void AcompanharMovPlayer()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) == null)
        {
            playerNotFound = true;
        }

        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            if (playerMorreu && playerNotFound)
            {
                var playerR = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
                playerMorreu = false;
                playerNotFound = false;
                transform.position = new Vector3(playerR.GetPosicao().x, playerR.GetPosicao().y + centerYCameraPlayer, transform.position.z);
                return;
            }

            playerNotFound = false;

            var player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
            
            var offSet = player.direcaoMovimento.y < 0 ? botYCameraPlayer : player.direcaoMovimento.y == 0 ? centerYCameraPlayer : topYCameraPlayer;
            if (offSet != offSetAnterior && !emVariacao)
            {
                emVariacao = true;
                offSetEmVariacao = offSet;
            }

            if (emVariacao)
            {
                //Vector3 posDesejada = new Vector3(0, player.GetPosicao().y + offSetEmVariacao, 0);
                //Vector3 posFluida = Vector3.Lerp(new Vector3(0, transform.position.y, 0), posDesejada, velocidadeVariacao);
                //transform.position = new Vector3(player.GetPosicao().x, posFluida.y, transform.position.z);
                Vector3 posDesejada = new Vector3(0, player.GetPosicao().y + offSetEmVariacao, 0);
                Vector3 posFluida = Vector3.SmoothDamp(new Vector3(0, transform.position.y, 0), posDesejada, ref velocidade, velocidadeVariacao);
                transform.position = new Vector3(player.GetPosicao().x, posFluida.y, transform.position.z);

                int a = (int)(posDesejada.y*100);
                int b = (int)(transform.position.y*100);

                if (a == b)
                {
                    offSetAnterior = offSetEmVariacao;
                    emVariacao = false;
                }
            }

            else
            {
                transform.position = new Vector3(player.GetPosicao().x, player.GetPosicao().y+offSet, transform.position.z);
            }
            
        }

    }
}
