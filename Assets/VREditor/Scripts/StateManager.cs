using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    protected StateManager() {} // guarantee this will be always a singleton only - can't use the constructor!

    public GameObject rig;
    public int rigMode = 0;
    public float speed = 0.1f;

    public Vector3 direction = new Vector3(0, 0, 0);
    public Vector3 rigDirection = new Vector3(0, 0, 0);
    public float scaleDirection = 1f;
    public bool rotationEnabled = false;
    public bool orbitEnabledRight = false;
    public bool orbitEnabledLeft = false;
    public float gridSnap = 0.1f;
    public int editMode = 0;
    public string lastButtonTouched = "";
    public string buttonsTouched = "";

    public GameObject lastBlockTouched;

    public GameObject menu;

    public GameObject previousControlledObject;
    public GameObject controlledObject;
    public GameObject instatiateObject;

    public Shader originalShader;

    public bool togglePointerOnHit = false;
    public GameObject[] prefabs;
    public GameObject[] prefabsFromFolder;
    public List<GameObject> unityGameObjects;
    public int prefabSelectedIndex = 1;

    public GameObject displayThumbnailControlledObject;
    public GameObject stageObject;
    public int counter;
    public bool updateView = false;

    public string[] editModes = { "Select Object", "Move", "Scale", "Rotate", "Clone", "Delete", "Orbit" };
    public GameObject[] editModeIndicator;

    public bool moveEnabled = false;
    public Vector3 moveDelta;
    public Quaternion rotationOriginal;
    public Quaternion targetRotationOriginal;
    public Quaternion deltaRotation;

    public int itemSelectedIndex = 0;
}

// 0 = select object
// 1 = move
// 2 = scale
// 3 = rotate
// 4 = instantiate
// 5 = delete
// 6 = orbit