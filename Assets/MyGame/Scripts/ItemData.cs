using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite[] taskSprites;
    public Department[] departments;
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

    public Color32 GetTargetColor(TaskToDo task)
    {
        switch (task)
        {
            case TaskToDo.Bear:
                return taskColors[(int)TaskToDo.Bear];
            case TaskToDo.Cat:
                return taskColors[(int)TaskToDo.Cat];
            case TaskToDo.Owl:
                return taskColors[(int)TaskToDo.Owl];
            case TaskToDo.One:
                return taskColors[(int)TaskToDo.One];
            case TaskToDo.Two:
                return taskColors[(int)TaskToDo.Two];
            case TaskToDo.Three:
                return taskColors[(int)TaskToDo.Three];
            default:
                return Color.black;
        }
    }

    public Sprite GetTargetSprite(TaskToDo targetTask)
    {
        switch (targetTask)
        {
            case TaskToDo.Bear:
                return taskSprites[(int)TaskToDo.Bear];
            case TaskToDo.Cat:
                return taskSprites[(int)TaskToDo.Cat];
            case TaskToDo.Owl:
                return taskSprites[(int)TaskToDo.Owl];
            case TaskToDo.One:
                return taskSprites[(int)TaskToDo.One];
            case TaskToDo.Two:
                return taskSprites[(int)TaskToDo.Two];
            case TaskToDo.Three:
                return taskSprites[(int)TaskToDo.Three];
            default:
                return taskSprites[(int)TaskToDo.Three];
        }
    }
}
