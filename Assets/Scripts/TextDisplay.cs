using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Text createdText;
    public Text markedText;


    public BlockManager numbers;

    void Update()
    {
        //createdText.text = numbers.created.Count.ToString();
        //markedText.text = numbers.marked.Count.ToString();
    }
}
