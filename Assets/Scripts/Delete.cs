using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete : MonoBehaviour
{
    string word = null;
    int wordIndex = -1;
    string alpha = null;
    string alpha2 = null;
    public Text myName = null;
    char[] nameChar = new char[30]; 

    public void nameFunc (string alphabet)
    {
        wordIndex++;
        char[] keepchar = alphabet.ToCharArray();
        nameChar[wordIndex] = keepchar[0];
        alpha = nameChar [wordIndex]. ToString();
        word = word + alpha;
        myName.text = word;
       // index.text = wordIndex.ToString();
    }
    public void DeleteFuntion()
    {
        if(wordIndex >=0)
        {
            wordIndex--;
            alpha2 = null; 
            for(int i = 0; i < wordIndex +1; i++)
            {
                alpha2 = alpha2 + nameChar[i].ToString();

            }
            word = alpha2;
            myName.text = word;
           // index.text = wordIndex.ToString();
        }
    
    }
}
