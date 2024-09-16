using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Form : MonoBehaviour
{
    public TextAndContext TAC;

    public TextMeshProUGUI textMP;
    private string tempt;
    // Start is called before the first frame update
    void Start()
    {
        TextMachine("...");   
        TAC = gameObject.GetComponent<TextAndContext>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextMachine(string text)
    {
        foreach (var item in text)
            {
            tempt = tempt + item;
            print(tempt);
            }
        print(tempt);
    }

    IEnumerator TextMachineE(string text, float timespace)
    {
        if (timespace == 0 || timespace == null)
        {
            timespace = 0.1f;
        }
        foreach (var item in text)
        {
            tempt = tempt + item;
            textMP.text = tempt;
            yield return new WaitForSeconds(timespace);
        }

         yield return new WaitForSeconds(2);
         cleardial();
   
    }


    public void cleardial()
    {
        textMP.text = "";
        if (TAC.currentDialogue == TAC.returnCurrentSection().Length || TAC.currentDialogue >= TAC.returnCurrentSection().Length)
        {
            //going to next section
        }
        else
        {
        TAC.currentDialogue = TAC.currentDialogue + 1;
            //start the couroutine and return the text to TextMachineE(returnCurrentDial());
            StartCoroutine(TextMachineE(TAC.returnCurrentDial(), 0));
        }
    }
}
