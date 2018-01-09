using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateExistingContent : MonoBehaviour
{
    List<GameObject> myObjects;
    public GameObject prefabButton;
    void Start()
    {
        myObjects = new List<GameObject>();
    }

    private void OnEnable()
    {
        Populate();
    }

    void Update()
    {
 
    }

    public void Populate()
    {
        GameObject stage = GameObject.Find("/Stage");
        Transform[] allChildren = stage.GetComponentsInChildren<Transform>();
        StateManager.Instance.unityGameObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            if (child.parent != null)
            {
                if (child.parent.name == "Stage")
                {
                    StateManager.Instance.unityGameObjects.Add(child.gameObject); // only add top level gameobjects
                }
            }
        }

        for (int i = 0; i < myObjects.Count; i++)
        {
            Destroy(myObjects[i]);
        }
        myObjects.Clear();

        for (int i = 0; i < StateManager.Instance.unityGameObjects.Count; i++)
        {
            myObjects.Add(Instantiate(prefabButton, transform));
            myObjects[i].name = "" + i;
            myObjects[i].transform.GetChild(0).GetComponent<Text>().text = "" + StateManager.Instance.unityGameObjects[i].name;
        }
    }
}