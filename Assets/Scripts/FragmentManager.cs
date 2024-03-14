using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FragmentManager : MonoBehaviour
{
    public int fragment;
    public TextMeshProUGUI ship;
    // Start is called before the first frame update
    void Start()
    {
        fragment = PlayerPrefs.GetInt("fragment", 0);

    }

    // Update is called once per frame
    void Update()
    {
        ship.text = PlayerPrefs.GetInt("fragment", 0).ToString();

    }
    public void Addfragment()
    {
        fragment++;
        PlayerPrefs.SetInt("fragment", fragment);
    }
}
