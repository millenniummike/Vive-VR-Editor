using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePrefabsContent : MonoBehaviour
{
    public GameObject prefabButton;
    List<GameObject> myObjects;
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

        StateManager.Instance.menu = GameObject.Find("PrefabCanvas");
        //StateManager.Instance.prefabsFromFolder = Resources.LoadAll<GameObject>("Prefabs");
        StateManager.Instance.updateView = true;

        if (myObjects != null) {
        for (int i = 0; i < myObjects.Count; i++)
        {
            Destroy(myObjects[i]);
        }
        myObjects.Clear();


        for (int i = 0; i < StateManager.Instance.prefabsFromFolder.Length; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            myObjects.Add(Instantiate(prefabButton, transform));
            myObjects[i].name = "" + i;
            myObjects[i].transform.GetChild(0).GetComponent<Text>().text = "" + StateManager.Instance.prefabsFromFolder[i].name;
        }
    }
    }
}