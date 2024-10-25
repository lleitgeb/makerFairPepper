using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite[] taskSprites;
    public Color32[] taskColors = new Color32[]
    {
        new Color32(253, 196, 4, 255), //fdch04 gelb bear
        new Color32(90, 160, 216, 255), //5aa0d8 blau cat
        new Color32(0, 167, 102, 255), //00a766 grün owl
        new Color32(217, 75, 50, 255), //d94b32 rot one
        new Color32(186, 198, 52, 255), //bac634 lime two
        new Color32(147, 114, 177, 255), //fdch04 lila three
        new Color32(0, 0, 0, 255), //000000 black None
    };

    public string[] feedback = new string[]
    {
        "Maximal unterschiedlich!",
        "Ziemlich unterschiedlich!",
        "Ähnlich, aber erkennbar unterschiedlich!",
        "Ähnlich, fast nicht zu unterscheiden!",
        "Die Farben sind komplett identisch!"
    };
}
