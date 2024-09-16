using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class trucF
{
    public string message;
    public float cleartime;
    public float intervaltime;
}

public class TextAndContext : MonoBehaviour
{
    public int currentDialogue = 0;
    public string currentSection = "A";

    public string[] sectionA; //hi | bruh i really need someone to help me write this shit... | anyway, what's your name? | interesting... {userName}
    public string[] sectionA1;
    public string[] sectionAP;

    public trucF lol;

    public string[] sectionFAILSAFE;



    private void Start()
    {
        
    }


    public string returnCurrentDial()
    {
        if (currentSection == "A")
        {
            return sectionA[currentDialogue];
        }
        return null;
    }

    public string[] returnCurrentSection()
    {
        if (currentSection == "A")
        {
            return sectionA;
        }
        return sectionFAILSAFE;
    }

}