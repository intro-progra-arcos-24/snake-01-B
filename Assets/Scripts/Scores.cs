using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public TextMeshProUGUI[] scoreText;

    //Add an input field in the inspector
    public TMP_InputField input;
    //Add the respective button in the inspector
    public Button button;

    public int[] scores = {100,79,41,21,10};

    // Start is called before the first frame update
    void Start()
    {
        PrintScore();
    }

    void PrintScore()
    {
        for (int i = 0; i <scores.Length; i++)
        {
            int currentScore = scores[i];
            TextMeshProUGUI actualText = scoreText[i];
            actualText.text = currentScore.ToString();
        }
    }

    public void ButtonPress()
    {
        if (string.IsNullOrEmpty(input.text)) return;

        int value = int.Parse(input.text);
        AddValue(value);

        PrintScore();
    }

    public void AddValue(int value)
    {
        int aux = 0;
        bool movingData = false;

        for (int i = 0; i <scores.Length; i++)
        {
            int actualValue = scores[i];
            if (!movingData)
            {
                if (actualValue < value)
                {
                    aux = scores[i];
                    movingData = true;
                    scores[i] = value;
                }
            }
            else
            {
                scores[i] = aux;
                aux = actualValue;
            }
        }
    }
}
