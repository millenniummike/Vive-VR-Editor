namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class VRControllerSelector : MonoBehaviour
    {
        public bool showHoverState = false;

        private void Start()
        {
            if (GetComponent<VRTK_DestinationMarker>() == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerPointerEvents_ListenerExample", "VRTK_DestinationMarker", "the Controller Alias"));
                return;
            }

            //Setup controller event listeners
            GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
            if (showHoverState)
            {
                GetComponent<VRTK_DestinationMarker>().DestinationMarkerHover += new DestinationMarkerEventHandler(DoPointerHover);
            }
            GetComponent<VRTK_DestinationMarker>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
            GetComponent<VRTK_DestinationMarker>().DestinationMarkerSet += new DestinationMarkerEventHandler(DoPointerDestinationSet);
        }

        private void DebugLogger(uint index, string action, Transform target, RaycastHit raycastHit, float distance, Vector3 tipPosition)
        {
            string targetName = (target ? target.name : "<NO VALID TARGET>");
            string colliderName = (raycastHit.collider ? raycastHit.collider.name : "<NO VALID COLLIDER>");
          //  VRTK_Logger.Info("Controller on index '" + index + "' is " + action + " at a distance of " + distance + " on object named [" + targetName + "] on the collider named [" + colliderName + "] - the pointer tip position is/was: " + tipPosition);
        }

        private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
        {

            Transform finalTarget = e.target;

            if (finalTarget.parent != null)
            {
                while (finalTarget.parent.name != "Stage") // traverse up toStage gameobject
                {
                    finalTarget = finalTarget.parent;
                    if (finalTarget.parent == null) break;
                }
            }

            StateManager.Instance.controlledObject = finalTarget.gameObject;
            StateManager.Instance.instatiateObject = finalTarget.gameObject;

            StateManager.Instance.editMode = 1;
            StateManager.Instance.updateView = true;

            if (StateManager.Instance.previousControlledObject != null)
            {
                StateManager.Instance.previousControlledObject.GetComponent<Renderer>().material.shader = StateManager.Instance.originalShader;
            }

            StateManager.Instance.previousControlledObject = StateManager.Instance.controlledObject;

            Shader shader = Shader.Find("SuperSystems/Wireframe");
            StateManager.Instance.originalShader = finalTarget.gameObject.GetComponent<Renderer>().material.shader;
            finalTarget.gameObject.GetComponent<Renderer>().material.shader = shader;
        }

        private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER OUT", e.target, e.raycastHit, e.distance, e.destinationPosition);
        }

        private void DoPointerHover(object sender, DestinationMarkerEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER HOVER", e.target, e.raycastHit, e.distance, e.destinationPosition);
        }

        private void DoPointerDestinationSet(object sender, DestinationMarkerEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "POINTER DESTINATION", e.target, e.raycastHit, e.distance, e.destinationPosition);
        }
    }
}