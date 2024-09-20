using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;



public class TextAndContext : MonoBehaviour
{
    public int currentDialogue = -1;
    public string currentSection = "A";

    public string[] sectionA; //hi | bruh i really need someone to help me write this test... | anyway, what's your name? | interesting... {userName}
    public string[] sectionAEVENT; //text | text | question(input)
    public float[] sectionAWtime;

    public string[] sectionA1;

    public string[] sectionAP;
    public string[] sectionAPEVENT;
    public float[] sectionAPWtime;


    public string[] sectionFAILSAFE;

    private SectionAcontainer SAc;



    private void Start()
    {

    }



    public string returnCurrentDial()
    {
        if (currentSection == "A")
        {
            return sectionA[currentDialogue];
        }
        if (currentSection == "AP")
        {
            return sectionAP[currentDialogue];
        }
        return null;
    }

    public string returnCurrentDialEvent()
    {
        if (currentSection == "A")
        {
            return sectionAEVENT[currentDialogue];
        }
        if (currentSection == "AP")
        {
            return sectionAPEVENT[currentDialogue];
        }
        return "text";
    }

    public string[] returnCurrentSection()
    {
        if (currentSection == "A")
        {
            return sectionA;
        }
        if (currentSection == "AP")
        {
            return sectionAP;
        }
        return sectionFAILSAFE;
    }

    public float returnSpacetime()
    {
        if (currentSection == "A")
        {
            return sectionAWtime[currentDialogue];
        }
        /*        if (currentSection == "AP")
                {
                    return sectionAPWtime[currentDialogue];
                }
        */
        return 0f;
    }

}