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

public class Session
{
    public string playerName;

    public string zeitstempel;
    public int points;
    public PlayMode mode;

    private int[] pointsPerRound;
    private int currentRound = -1;

    public void SetFirstRound()
    {
        currentRound = 0;
    }



    public void SetPlayername(string pname)
    {
        playerName = pname;
    }
    public void SetPointsRound(int round, int points)
    {
        pointsPerRound[currentRound] = points;
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
        string sessionData = playerName + "," + "," + zeitstempel + "," + points+"," + mode.ToString();
        File.AppendAllText(filePath, sessionData + Environment.NewLine);
    }

    public void ResetPoints()
    {
        for(int i = 0; i < pointsPerRound.Length; i++)
        {
            pointsPerRound[i] = -1;
        }
    }

    public bool IsRoundUnplayed(int round)
    {
        if (round < 0 || round >= pointsPerRound.Length)
        {
            Debug.LogError("no such round available!" + round);
        }

        return pointsPerRound[round] != -1;
    }

    public Session(string name, string zeit, int punkte, PlayMode modus)
    {
        playerName = name;
        zeitstempel = zeit;
        points = punkte;
        mode = modus;
    }

    public Session(string name, string zeit, PlayMode modus)
    {
        playerName = name;
        zeitstempel = zeit;
        mode = modus;
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

            sessions.Add(new Session(name, zeit, punkte, modus));
        }

        return sessions;
    }

    public void EnableEndScreen()
    {

    }

    // Methode zum Anzeigen der Rangliste
    public static void DisplayRangliste(List<Session> sessions)
    {
        // Sortiere die Liste basierend auf den Punkten (absteigend)
        var sortedSessions = sessions.OrderByDescending(s => s.points).ToList();

        // Zeige die Rangliste an
        Console.WriteLine("Rangliste:");
        for (int i = 0; i < sortedSessions.Count; i++)
        {
            Session s = sortedSessions[i];
            Console.WriteLine($"{i + 1}. {s.playerName} - {s.points} Punkte ({s.zeitstempel}, Modus: {s.mode})");
        }
    }
}
