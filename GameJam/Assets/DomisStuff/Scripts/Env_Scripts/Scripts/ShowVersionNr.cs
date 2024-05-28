using UnityEngine;

public class ShowVersionNr : MonoBehaviour
{

    //This is the version number of the game, and will be displayed in the top left corner of the screen.
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "v" + Application.version);
    }
}
