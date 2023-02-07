using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente a camera.
public class Camera : MonoBehaviour, ICamera
{
    private IPlayer player;

    private bool tipoDeCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        tipoDeCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        AcompanharMovPlayer();
    }

    public void AlternarModoDeCamera()
    {
        // tipoDeCamera = !tipoDeCamera;
        // Debug.Log(tipoDeCamera);
    }

    // Vincula a camera ao player
    private void AcompanharMovPlayer()
    {
        if (tipoDeCamera == false)
        {
            cameraNormal();
        }
        else
        {
            cameraFunda();
        }
    }

    private void cameraNormal()
    {
        gameObject.transform.position = new Vector3(player.GetPosicao().x, player.GetPosicao().y + 2.7f, -10);
    }

    private void cameraFunda()
    {
        gameObject.transform.position = new Vector3(player.GetPosicao().x, player.GetPosicao().y + 1.0f, -10);
    }
}
