using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PepperOnlyManager : MonoBehaviour
{
    [SerializeField] private Canvas welcome, auftrag;
    [SerializeField] private TMP_Text playerNameWelcome, playerNameAuftrag;
    [SerializeField] private TMP_Text taskPoints;


    public Session currentSession;

    // Start is called before the first frame update
    void Start()
    {
        if(auftrag != null)
        {
            if (auftrag.gameObject.activeSelf)
            {
                auftrag.gameObject.SetActive(false);
                Debug.Log("Set Auftrag Canvas to false");
            }
            
        }
        welcome.gameObject.SetActive(true);
    }

    public void StartSession()
    {
        DateTime currentDateTime = DateTime.Now;
        string customFormat = currentDateTime.ToString("yyyy-MM-dd-HH:mm:ss");
        
        currentSession = new Session(playerNameWelcome.text, customFormat,PlayMode.PepperOnly);
        welcome.gameObject.SetActive(false);
        auftrag.gameObject.SetActive(true);
        playerNameAuftrag.text = currentSession.playerName;
    }

    public bool IsPlayMode()
    {
        return !welcome.gameObject.activeSelf && !auftrag.gameObject.activeSelf;
    }

    public void EnableTaskCanvas()
    {
        playerNameAuftrag.text = currentSession.playerName;
        taskPoints.text = currentSession.points.ToString();
        welcome.gameObject.SetActive(false);
        auftrag.gameObject.SetActive(true);
    }
}
