using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Form : MonoBehaviour
{
    public TextMeshProUGUI textMP;
    private string tempt;
    // Start is called before the first frame update
    void Start()
    {
        TextMachine("Helo...");   
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
}
