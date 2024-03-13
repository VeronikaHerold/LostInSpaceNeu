using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class SaveMyGame : MonoBehaviour
{
    private int score = 1;
    // Start is called before the first frame update

    // Update is called once per frame
public void Save()
    {
        SaveGame.Save<int>("myFile", score);
    }
}
