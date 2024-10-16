using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum PlayMode
{
    Koopterative,
    VOOnly,
    PepperOnly
}

public class Session
{
    public string playerName;

    public string zeitstempel;
    public int points;
    public PlayMode mode;

    // Methode zum Speichern der Session in einer Textdatei
    public void SaveSessionToFile(string filePath)
    {
        string sessionData = playerName + "," + "," + zeitstempel + "," + points+"," + mode.ToString();
        File.AppendAllText(filePath, sessionData + Environment.NewLine);
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
