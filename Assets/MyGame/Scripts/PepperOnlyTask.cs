using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TaskToDo
{
    Bear = 0,
    Cat = 1,
    Owl = 2,
    One = 3,
    Two = 4,
    Three = 5,
    None = 6
}

public class PepperOnlyTask : MonoBehaviour
{
    private Color32[] taskColors = new Color32[]
    {
        new Color32(253, 196, 4, 255), //fdch04 gelb bear
        new Color32(90, 160, 216, 255), //5aa0d8 blau cat
        new Color32(0, 167, 102, 255), //00a766 grün owl
        new Color32(217, 75, 50, 255), //d94b32 rot one
        new Color32(186, 198, 52, 255), //bac634 lime two
        new Color32(147, 114, 177, 255), //fdch04 lila three
        new Color32(0, 0, 0, 255), //000000 black None
    };

    private string[] feedback = new string[]
    {
        "Maximal unterschiedlich!",
        "Ziemlich unterschiedlich!",
        "Ähnlich, aber erkennbar unterschiedlich!",
        "Ähnlich, fast nicht zu unterscheiden!",
        "Die Farben sind komplett identisch!"
    };

    public TaskToDo TargetTask { get; set; }
    public Color PlayerColor { get; set; }
    private float euclidDistance = -1;
    public int Points { get; set; }
    private string resultFeedback = "";

    private void Start()
    {
        Points = -1;
    }

    private int CalculatePoints(float euclidDistance)
    {
        int points = -1;

        //Die Farben sind komplett identisch.
        if (euclidDistance >= 0 && euclidDistance <= 10)
        {
            points = 4;
        }
        //Ähnlich, fast nicht zu unterscheiden.
        else if (euclidDistance >= 11 && euclidDistance <= 40)
        {
            points = 3;
        }
        //Ähnlich, aber erkennbar unterschiedlich
        else if (euclidDistance >= 41 && euclidDistance <= 100)
        {
            points = 2;
        }
        //Ziemlich unterschiedlich
        else if (euclidDistance >= 101 && euclidDistance <= 200)
        {
            points = 1;
        }
        //Maximal unterschiedlich, maximale Distanz von etwa 441.67
        else if (euclidDistance >= 201)
        {
            points = 0;
        }

        return points;
    }

    private void SetFeedback(int points)
    {
        resultFeedback = feedback[points];
    }

    public int EvaluatePoints(TaskToDo cTask, Color32 mixedColor)
    {
        int distance = -1;

        switch (cTask)
        {
            case TaskToDo.Bear:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Bear), mixedColor);
                break;
            case TaskToDo.Cat:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Cat), mixedColor);
                break;
            case TaskToDo.Owl:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Owl), mixedColor);
                break;
            case TaskToDo.One:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.One), mixedColor);
                break;
            case TaskToDo.Two:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Two), mixedColor);
                break;
            case TaskToDo.Three:
                distance = (int)CalculateColorDistance(GetTargetColor(TaskToDo.Three), mixedColor);
                break;
        }

        int points = CalculatePoints(distance);
        return points;
    }

    public float CalculateColorDistance(Color32 color1, Color32 color2)
    {
        // Differenzen der Farbkanäle
        int rDifference = color2.r - color1.r;
        int gDifference = color2.g - color1.g;
        int bDifference = color2.b - color1.b;

        // Euklidische Distanz berechnen
        float euclidDistance = Mathf.Sqrt(rDifference * rDifference + gDifference * gDifference + bDifference * bDifference);

        return euclidDistance;
    }

    public Color32 GetTargetColor(TaskToDo task)
    {
        switch(task)
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
}
