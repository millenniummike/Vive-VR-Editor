using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : MonoBehaviour {

    public GameObject setupRig;
	// Use this for initialization
	void Start () {
        StateManager.Instance.rig = setupRig;
        StateManager.Instance.instatiateObject = null;
        StateManager.Instance.stageObject = GameObject.Find("/Stage");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public void Load()
    {
        Debug.Log("TO DO WRITE LOAD CODE!");
    } 
}
