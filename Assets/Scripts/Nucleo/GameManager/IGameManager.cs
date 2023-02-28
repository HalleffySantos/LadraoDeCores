using UnityEngine;

public interface IGameManager
{
    public bool AmareloEncontrado { get; set; }

    public Color Amarelo { get; }

    public Color Cinza { get; }

    public void SaveGame();
}