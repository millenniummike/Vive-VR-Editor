namespace VRTK.Examples
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class VRControllerMenu : MonoBehaviour
    {


        private void Start()
        {
            if (GetComponent<VRTK_UIPointer>() == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerUIPointerEvents_ListenerExample", "VRTK_UIPointer", "the Controller Alias"));
                return;
            }

            if (StateManager.Instance.togglePointerOnHit)
            {
                GetComponent<VRTK_UIPointer>().activationMode = VRTK_UIPointer.ActivationMethods.AlwaysOn;
            }

            //Setup controller event listeners
            GetComponent<VRTK_UIPointer>().UIPointerElementEnter += VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementEnter;
            GetComponent<VRTK_UIPointer>().UIPointerElementExit += VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementExit;
            GetComponent<VRTK_UIPointer>().UIPointerElementClick += VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementClick;
            GetComponent<VRTK_UIPointer>().UIPointerElementDragStart += VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragStart;
            GetComponent<VRTK_UIPointer>().UIPointerElementDragEnd += VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragEnd;
        }

        private void Update()
        {
            GameObject go = GameObject.Find("sliderRigSpeed");
            if (go != null) { StateManager.Instance.speed = go.GetComponent<Slider>().value; }

            GameObject go2 = GameObject.Find("gridSnapping");
            if (go2 != null) { StateManager.Instance.gridSnap = go2.GetComponent<Slider>().value * 0.1f; }
        }

        private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementEnter(object sender, UIPointerEventArgs e)
        {
            //VRTK_Logger.Info("UI Pointer entered " + e.currentTarget.name + " on Controller index [" + VRTK_ControllerReference.GetRealIndex(e.controllerReference) + "] and the state was " + e.isActive + " ### World Position: " + e.raycastResult.worldPosition);
            if (StateManager.Instance.togglePointerOnHit && GetComponent<VRTK_Pointer>())
            {
                GetComponent<VRTK_Pointer>().Toggle(true);
            }
        }

        private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementExit(object sender, UIPointerEventArgs e)
        {
           // VRTK_Logger.Info("UI Pointer exited " + e.previousTarget.name + " on Controller index [" + VRTK_ControllerReference.GetRealIndex(e.controllerReference) + "] and the state was " + e.isActive);
            if (StateManager.Instance.togglePointerOnHit && GetComponent<VRTK_Pointer>())
            {
                GetComponent<VRTK_Pointer>().Toggle(false);
            }
        }

        private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementClick(object sender, UIPointerEventArgs e)
        {
            if (e.currentTarget.transform.parent.name == "ExistingContent" || e.currentTarget.transform.parent.name == "PrefabsContent")
            {
                StateManager.Instance.prefabSelectedIndex = int.Parse(e.currentTarget.name);

                StateManager.Instance.controlledObject = null;
                if (e.currentTarget.transform.parent.name == "PrefabsContent")
                {
                    StateManager.Instance.instatiateObject = StateManager.Instance.prefabsFromFolder[StateManager.Instance.prefabSelectedIndex];
                }
                else
                {
                    StateManager.Instance.instatiateObject = StateManager.Instance.unityGameObjects[StateManager.Instance.prefabSelectedIndex];
                    StateManager.Instance.controlledObject = StateManager.Instance.unityGameObjects[StateManager.Instance.prefabSelectedIndex];

                    //** todo redo as box
                    /*
                    if (StateManager.Instance.controlledObject.GetComponent<Renderer>()) {
                        Shader shader = Shader.Find("SuperSystems/Wireframe");
                        StateManager.Instance.originalShader = StateManager.Instance.controlledObject.GetComponent<Renderer>().material.shader;
                        StateManager.Instance.controlledObject.gameObject.GetComponent<Renderer>().material.shader = shader;
                    }
                    */
                }

                StateManager.Instance.previousControlledObject = StateManager.Instance.controlledObject;

                StateManager.Instance.editMode = 4;
                GameObject menu = GameObject.Find("PrefabCanvas");
               // menu.SetActive(false);
                StateManager.Instance.updateView = true;
            }

        }

        private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragStart(object sender, UIPointerEventArgs e)
        {
           // VRTK_Logger.Info("UI Pointer started dragging " + e.currentTarget.name + " on Controller index [" + VRTK_ControllerReference.GetRealIndex(e.controllerReference) + "] and the state was " + e.isActive + " ### World Position: " + e.raycastResult.worldPosition);
        }

        private void VRTK_ControllerUIPointerEvents_ListenerExample_UIPointerElementDragEnd(object sender, UIPointerEventArgs e)
        {
           // VRTK_Logger.Info("UI Pointer stopped dragging " + e.currentTarget.name + " on Controller index [" + VRTK_ControllerReference.GetRealIndex(e.controllerReference) + "] and the state was " + e.isActive + " ### World Position: " + e.raycastResult.worldPosition);
        }
    }
}