using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour {

    [SerializeField]
    public string[] m_Keywords;
    public string[] m_Numbers;
    public GameObject[] objects;
    private KeywordRecognizer m_Recognizer;

    void Start()
    {
        var allWords = new string[m_Keywords.Length + m_Numbers.Length];
        m_Keywords.CopyTo(allWords, 0);
        m_Numbers.CopyTo(allWords, m_Keywords.Length);
        m_Recognizer = new KeywordRecognizer(allWords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
        Debug.Log("Voice recognition on");
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {

        StringBuilder builder = new StringBuilder();
        float newX = UnityEngine.Random.Range(0, 0);
        float newZ = UnityEngine.Random.Range(0, 0);
        Debug.Log(args.text);

        if (args.text == m_Keywords[0])
        {
            Debug.Log("************* SCALE *************");
            StateManager.Instance.editMode = 2;
        }

        if (args.text == m_Keywords[1])
        {
            Debug.Log("************* MOVE *************");
            StateManager.Instance.editMode = 1;
        }


        if (args.text == m_Keywords[2])
        {
            Debug.Log("************* ROTATE *************");
            StateManager.Instance.editMode = 3;
        }

        if (args.text == m_Keywords[3])
        {
            Debug.Log("************* ORBIT *************");
            StateManager.Instance.editMode = 6;
        }

        if (args.text == m_Keywords[4])
        {
            Debug.Log("************* DELETE *************");
            StateManager.Instance.editMode = 5;
        }

        if (args.text == m_Keywords[5])
        {
            Debug.Log("************* CLONE *************");
            StateManager.Instance.editMode = 4;
        }


        for (int c = 0; c < m_Numbers.Length; c++)
        {
            if (args.text == m_Numbers[c]) {
                Debug.Log(c);
                Instantiate(objects[c], new Vector3(newX, newZ, 1), Quaternion.identity);
            }
        }

        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);

        Debug.Log(builder.ToString());
    }
}
