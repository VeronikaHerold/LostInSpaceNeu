using UnityEngine;
using TMPro;

public class FragmentManager : MonoBehaviour
{
    private int fragment = 0;
    public TextMeshProUGUI ship;

    void Start()
    {
        // Initialisiere das Fragment auf 0
        fragment = 0;
        // Aktualisiere den Text zu Beginn
        UpdateFragmentText();
    }

    // Update is called once per frame
    void Update()
    {
        // Hier muss nichts aktualisiert werden, da die Anzahl der Fragmente nicht dynamisch aktualisiert wird
    }

    public void Addfragment()
    {
        // Erhöhe die Anzahl der Fragmente um 1
        fragment++;
        // Aktualisiere den Text, um die Änderung anzuzeigen
        UpdateFragmentText();
    }

    private void UpdateFragmentText()
    {
        // Aktualisiere den Text, um die aktuelle Anzahl der Fragmente anzuzeigen
        ship.text = fragment.ToString();
    }
}
