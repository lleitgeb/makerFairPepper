using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public enum PlayMode
{
    Koopterative,
    VROnly,
    PepperOnly,
    None
}

public class Session : MonoBehaviour
{
    public string PlayerName { set; get; }
    public string zeitstempel;
    public PlayMode mode;

    [SerializeField] private PepperOnlyTask[] tasks;
    private int maxRounds = 4;
    private int currentRound = -1;

    public PepperOnlyTask GetRoundTask(int round)
    {
        Debug.Log("Round: " + round + " " +tasks[round].name + ", "+ tasks[round].Points);
        return tasks[round];
    }

    // Nur get, kein set -> Wert kann nur gelesen werden
    public int MaxRounds
    {
        get { return maxRounds; }
    }

    public void SetInfos(string name, string zeit, PlayMode modus)
    {
        gameObject.name = "session";
        PlayerName = name;
        zeitstempel = zeit;
        mode = modus;

        tasks = GenerateTaskArray(maxRounds);
        Debug.Log("Size Array" + tasks.Length);
    }

    //public Session(string name, string zeit, PlayMode modus)
    //{
    //    PlayerName = name;
    //    zeitstempel = zeit;
    //    mode = modus;

    //    tasks = GenerateTaskArray(maxRounds);
    //    Debug.Log("Size Array" + tasks.Length);
    //}

    public int SumTaskResults()
    {
        int sum = 0;
        foreach(PepperOnlyTask a in tasks)
        {
            if (a.Points == -1) break;
            sum += a.Points;
        }
        return sum;
    }

    public void SetFirstRound()
    {
        currentRound = 0;
    }

    private PepperOnlyTask CreateNewTask()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<PepperOnlyTask>();
        return obj.GetComponent<PepperOnlyTask>();
    }

    public void AddTask(TaskToDo task)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<PepperOnlyTask>();
        obj.GetComponent<PepperOnlyTask>().SetTask((int)task);
        obj.name = "task" + currentRound;
        tasks[currentRound] = obj.GetComponent<PepperOnlyTask>();
    }

    public void SetTask(TaskToDo task)
    {
        tasks[currentRound].SetTask((int)task);
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void IncreaseRound()
    {
        currentRound++;
    }

    // Methode zum Speichern der Session in einer Textdatei
    public void SaveSessionToFile(string filePath)
    {
        //string sessionData = playerName + "," + "," + zeitstempel + "," + points+"," + mode.ToString();
        //File.AppendAllText(filePath, sessionData + Environment.NewLine);
    }

    private PepperOnlyTask[] GenerateTaskArray(int maxRounds)
    {
        PepperOnlyTask[] tasks = new PepperOnlyTask[maxRounds];
        for(int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = CreateNewTask();
            tasks[i].name = "task" + i;
        }

        return tasks;
    }

    // Methode zum Laden der Sessions aus einer Datei und Rückgabe einer Liste von Sessions
    public static List<Session> LoadSessionsFromFile(string filePath)
    {
        List<Session> sessions = new List<Session>();

        // Datei Zeile für Zeile lesen
        foreach (var line in File.ReadLines(filePath))
        {
            // Zeile in Komponenten aufteilen (Name,Zeit,Punkte,Modus)
            string[] parts = line.Split(',');

            // Erstelle eine neue Session basierend auf den gespeicherten Daten
            string name = parts[0];
            string zeit = DateTime.Parse(parts[1]).ToString();
            int punkte = int.Parse(parts[2]);

            // Konvertiere den Modus-String zurück in ein Enum
            PlayMode modus = (PlayMode)Enum.Parse(typeof(PlayMode), parts[3]);

            //sessions.Add(new Session(name, zeit, modus));
        }

        return sessions;
    }

    public void EnableEndScreen()
    {

    }

    // Methode zum Anzeigen der Rangliste
    //public static void DisplayRangliste(List<Session> sessions)
    //{
    //    // Sortiere die Liste basierend auf den Punkten (absteigend)
    //    var sortedSessions = sessions.OrderByDescending(s => s.points).ToList();

    //    // Zeige die Rangliste an
    //    Console.WriteLine("Rangliste:");
    //    for (int i = 0; i < sortedSessions.Count; i++)
    //    {
    //        Session s = sortedSessions[i];
    //        Console.WriteLine($"{i + 1}. {s.playerName} - {s.points} Punkte ({s.zeitstempel}, Modus: {s.mode})");
    //    }
    //}
}
