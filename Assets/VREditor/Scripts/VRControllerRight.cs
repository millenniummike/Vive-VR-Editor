namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class VRControllerRight: MonoBehaviour
    {
        private void Start()
        {
            
            if (GetComponent<VRTK_ControllerEvents>() == null)
            {
                //VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
                return;
            }

             //Setup controller event listeners
            GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
            GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);

            GetComponent<VRTK_ControllerEvents>().TriggerTouchStart += new ControllerInteractionEventHandler(DoTriggerTouchStart);
            GetComponent<VRTK_ControllerEvents>().TriggerTouchEnd += new ControllerInteractionEventHandler(DoTriggerTouchEnd);

            GetComponent<VRTK_ControllerEvents>().TriggerHairlineStart += new ControllerInteractionEventHandler(DoTriggerHairlineStart);
            GetComponent<VRTK_ControllerEvents>().TriggerHairlineEnd += new ControllerInteractionEventHandler(DoTriggerHairlineEnd);

            GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerClicked);
            GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnclicked);

            GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);

            GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
            GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(DoGripReleased);

            GetComponent<VRTK_ControllerEvents>().GripTouchStart += new ControllerInteractionEventHandler(DoGripTouchStart);
            GetComponent<VRTK_ControllerEvents>().GripTouchEnd += new ControllerInteractionEventHandler(DoGripTouchEnd);

            GetComponent<VRTK_ControllerEvents>().GripHairlineStart += new ControllerInteractionEventHandler(DoGripHairlineStart);
            GetComponent<VRTK_ControllerEvents>().GripHairlineEnd += new ControllerInteractionEventHandler(DoGripHairlineEnd);

            GetComponent<VRTK_ControllerEvents>().GripClicked += new ControllerInteractionEventHandler(DoGripClicked);
            GetComponent<VRTK_ControllerEvents>().GripUnclicked += new ControllerInteractionEventHandler(DoGripUnclicked);

            GetComponent<VRTK_ControllerEvents>().GripAxisChanged += new ControllerInteractionEventHandler(DoGripAxisChanged);

            GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
            GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);

            GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouchStart);
            GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

            GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

            GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(DoButtonOnePressed);
            GetComponent<VRTK_ControllerEvents>().ButtonOneReleased += new ControllerInteractionEventHandler(DoButtonOneReleased);

            GetComponent<VRTK_ControllerEvents>().ButtonOneTouchStart += new ControllerInteractionEventHandler(DoButtonOneTouchStart);
            GetComponent<VRTK_ControllerEvents>().ButtonOneTouchEnd += new ControllerInteractionEventHandler(DoButtonOneTouchEnd);

            GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoButtonTwoPressed);
            GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);

            GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchStart += new ControllerInteractionEventHandler(DoButtonTwoTouchStart);
            GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchEnd += new ControllerInteractionEventHandler(DoButtonTwoTouchEnd);

            GetComponent<VRTK_ControllerEvents>().StartMenuPressed += new ControllerInteractionEventHandler(DoStartMenuPressed);
            GetComponent<VRTK_ControllerEvents>().StartMenuReleased += new ControllerInteractionEventHandler(DoStartMenuReleased);

            GetComponent<VRTK_ControllerEvents>().ControllerEnabled += new ControllerInteractionEventHandler(DoControllerEnabled);
            GetComponent<VRTK_ControllerEvents>().ControllerDisabled += new ControllerInteractionEventHandler(DoControllerDisabled);

            GetComponent<VRTK_ControllerEvents>().ControllerIndexChanged += new ControllerInteractionEventHandler(DoControllerIndexChanged);
        }
       

        // The snapping code
        private Vector3 applyGridSnap(Vector3 v)
        {
            return new Vector3
            (
                 StateManager.Instance.gridSnap * Mathf.Round(v.x / StateManager.Instance.gridSnap),
                 StateManager.Instance.gridSnap * Mathf.Round(v.y / StateManager.Instance.gridSnap),
                 StateManager.Instance.gridSnap * Mathf.Round(v.z / StateManager.Instance.gridSnap)
            );
        }

        private Quaternion applyRotationGridSnap(Quaternion q)
        {
            return new Quaternion
            (
                 StateManager.Instance.gridSnap * Mathf.Round(q.x / StateManager.Instance.gridSnap),
                 StateManager.Instance.gridSnap * Mathf.Round(q.y / StateManager.Instance.gridSnap),
                 StateManager.Instance.gridSnap * Mathf.Round(q.z / StateManager.Instance.gridSnap),
                 StateManager.Instance.gridSnap * Mathf.Round(q.w / StateManager.Instance.gridSnap)
            );
        }

        private void checkSnap()
        {
            if (StateManager.Instance.gridSnap > 0 && StateManager.Instance.controlledObject != null) {
                StateManager.Instance.controlledObject.transform.position = applyGridSnap(StateManager.Instance.controlledObject.transform.position);
                StateManager.Instance.controlledObject.transform.localRotation = applyRotationGridSnap(StateManager.Instance.controlledObject.transform.localRotation);
            }
        }

        private void Update()
        {
            string displayText = "" + StateManager.Instance.editModes[StateManager.Instance.editMode] + " ";
            if (StateManager.Instance.controlledObject != null) {
                StateManager.Instance.controlledObject.transform.position += StateManager.Instance.direction;
                StateManager.Instance.controlledObject.transform.localScale = StateManager.Instance.controlledObject.transform.localScale * StateManager.Instance.scaleDirection;
                if (StateManager.Instance.rotationEnabled)
                {
                    StateManager.Instance.controlledObject.transform.rotation = transform.rotation * StateManager.Instance.deltaRotation; // set in line with controller
                }


                displayText += StateManager.Instance.controlledObject.name;

                if (StateManager.Instance.moveEnabled) {
                    StateManager.Instance.controlledObject.transform.position = transform.position - StateManager.Instance.moveDelta;
                }
            }

            if (StateManager.Instance.controlledObject == null && StateManager.Instance.instatiateObject != null)
            {
                displayText += StateManager.Instance.instatiateObject.name;
            }

            TMPro.TextMeshPro textmeshPro = GameObject.Find("EditModeState").GetComponent<TMPro.TextMeshPro>();
            textmeshPro.SetText(displayText);

            for (int count=0; count< StateManager.Instance.editModeIndicator.Length; count++)
            {
                StateManager.Instance.editModeIndicator[count].SetActive(false);
            }
            StateManager.Instance.editModeIndicator[StateManager.Instance.editMode].SetActive(true); 

            if (StateManager.Instance.updateView) { StateManager.Instance.updateView = false; setSelectedObject(); }
        }

        public void setSelectedObject()
        {

            //**TODO optimise
            if (StateManager.Instance.controlledObject == null && StateManager.Instance.displayThumbnailControlledObject != null)
            {
                Destroy(StateManager.Instance.displayThumbnailControlledObject);
            }
                
            if (StateManager.Instance.controlledObject != null && StateManager.Instance.displayThumbnailControlledObject == null)
            {
                updateThumbnail(StateManager.Instance.controlledObject);
            }

            if (StateManager.Instance.controlledObject != null && StateManager.Instance.displayThumbnailControlledObject != null)
            {
                Destroy(StateManager.Instance.displayThumbnailControlledObject);
                updateThumbnail(StateManager.Instance.controlledObject);
            }

            if (StateManager.Instance.instatiateObject != null)
            {
                Destroy(StateManager.Instance.displayThumbnailControlledObject);
                updateThumbnail(StateManager.Instance.instatiateObject);
            }
        }

        public void updateThumbnail(GameObject go)
        {
            StateManager.Instance.displayThumbnailControlledObject = Instantiate(go);
            Renderer rend = StateManager.Instance.displayThumbnailControlledObject.GetComponentsInChildren<MeshRenderer>()[0]; // get first renderer
            if (rend != null)
            {
                Vector3 center = rend.bounds.center;
                float radius = rend.bounds.extents.magnitude;

                float scaleFactor;
                if (radius > 1)
                { scaleFactor = 1 / radius * 0.2f; }
                else
                { scaleFactor = 1 * radius * 0.2f; }

                StateManager.Instance.displayThumbnailControlledObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                StateManager.Instance.displayThumbnailControlledObject.transform.parent = transform;
                StateManager.Instance.displayThumbnailControlledObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                StateManager.Instance.displayThumbnailControlledObject.transform.Rotate(new Vector3(45, 0, 0));
                StateManager.Instance.displayThumbnailControlledObject.transform.localPosition = new Vector3(0, 0.05f, 0.15f);
            }
            var allColliders = StateManager.Instance.displayThumbnailControlledObject.GetComponentsInChildren<Collider>();
            foreach (var childCollider in allColliders) Destroy(childCollider);
        }

        private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
        {
            /*
            VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
                    + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
            */
        }

        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
            if (StateManager.Instance.editMode == 1) // move
            {
                StateManager.Instance.moveEnabled = true;
                StateManager.Instance.moveDelta = transform.position - StateManager.Instance.controlledObject.transform.position;
            }
        }

        private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
            // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
            StateManager.Instance.moveEnabled = false;
            checkSnap();
        }

        private void DoTriggerTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "touched", e);
        }

        private void DoTriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "untouched", e);
        }

        private void DoTriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline start", e);
        }

        private void DoTriggerHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline end", e);
        }

        private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "clicked", e);
        }

        private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "unclicked", e);
        }

        private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "axis changed", e);
        }

        private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
        {
            // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "pressed", e);
            StateManager.Instance.direction.y = StateManager.Instance.speed * -1;
        }

        private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
        {
            // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "released", e);
            StateManager.Instance.direction.y = 0f;
            checkSnap();
        }

        private void DoGripTouchStart(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "touched", e);
        }

        private void DoGripTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "untouched", e);
        }

        private void DoGripHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline start", e);
        }

        private void DoGripHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline end", e);
        }

        private void DoGripClicked(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "clicked", e);
        }

        private void DoGripUnclicked(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "unclicked", e);
        }

        private void DoGripAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "axis changed", e);
        }

        private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
        {
            float dir = e.touchpadAxis.y;
            float dir2 = e.touchpadAxis.x;

            if (StateManager.Instance.editMode == 1) // move
            {
                StateManager.Instance.direction.y = StateManager.Instance.speed; // pressed so go up
                if (dir > 0.5) dir = StateManager.Instance.speed;
                if (dir < -0.5) dir = StateManager.Instance.speed * -1;

                if (dir2 > 0.5) dir2 = StateManager.Instance.speed;
                if (dir2 < -0.5) dir2 = StateManager.Instance.speed * -1;

                if (System.Math.Abs(dir) == StateManager.Instance.speed) { StateManager.Instance.direction = transform.forward * dir; }
                if (System.Math.Abs(dir2) == StateManager.Instance.speed) { StateManager.Instance.direction = transform.right * dir2; }
            }

            if (StateManager.Instance.editMode == 2) // scale
            {
                if (dir < 0.5) StateManager.Instance.scaleDirection = 0.99f;
                if (dir > -0.5) StateManager.Instance.scaleDirection = 1.01f;
            }

            if (StateManager.Instance.editMode == 3) // rotate
            {
                StateManager.Instance.rotationEnabled = true;
                StateManager.Instance.targetRotationOriginal = StateManager.Instance.controlledObject.transform.rotation;
                StateManager.Instance.rotationOriginal = transform.rotation;
                StateManager.Instance.deltaRotation = Quaternion.Inverse(StateManager.Instance.rotationOriginal) * StateManager.Instance.targetRotationOriginal;
            }

            if (StateManager.Instance.editMode == 4) // instantiate
            {
                Renderer rend = StateManager.Instance.instatiateObject.GetComponentsInChildren<MeshRenderer>()[0];

                StateManager.Instance.controlledObject = Instantiate(StateManager.Instance.instatiateObject);
                if (StateManager.Instance.stageObject != null) StateManager.Instance.controlledObject.transform.SetParent(StateManager.Instance.stageObject.transform);

                Vector3 center = rend.bounds.center;
                float radius = rend.bounds.extents.magnitude;

                float scaleFactor;
                if (radius > 1)
                { scaleFactor = 1 / radius * 0.2f; }
                else
                { scaleFactor = 1 * radius * 0.2f; }

                GameObject tempCollider = new GameObject();
                tempCollider.transform.SetParent(StateManager.Instance.controlledObject.transform);
                tempCollider.name = "TempVRCollider";
                tempCollider.AddComponent<SphereCollider>();
                SphereCollider sphereCollider = tempCollider.GetComponent<SphereCollider>();
                sphereCollider.radius = radius;
                StateManager.Instance.controlledObject.transform.position = transform.position;
                StateManager.Instance.controlledObject.transform.position += transform.forward * radius;
                StateManager.Instance.editMode = 1;

                string newName = StateManager.Instance.instatiateObject.name;
                if (newName.IndexOf("-") != -1) {
                    newName = newName.Substring(0, newName.IndexOf("-")).Trim();
                }
                newName += "-" + StateManager.Instance.counter++;
                StateManager.Instance.controlledObject.name = newName;
                
            }

            if (StateManager.Instance.editMode == 5) // delete
            {
                Destroy(StateManager.Instance.controlledObject);
                StateManager.Instance.controlledObject = null;
                StateManager.Instance.instatiateObject = null;
                StateManager.Instance.editMode = 0;
            }

            setSelectedObject();
        }

        private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
        {
            // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "released", e);
            StateManager.Instance.direction.x = 0f;
            StateManager.Instance.direction.y = 0f;
            StateManager.Instance.direction.z = 0f;
            StateManager.Instance.scaleDirection = 1f;
            StateManager.Instance.rotationEnabled = false;
            checkSnap();
        }

        private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "touched", e);
        }

        private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "untouched", e);
        }

        private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "axis changed", e);
        }

        private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "pressed down", e);
        }

        private void DoButtonOneReleased(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "released", e);
        }

        private void DoButtonOneTouchStart(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "touched", e);
        }

        private void DoButtonOneTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "untouched", e);
        }

        private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {

            // MENU BUTTON
            // editmode
            // 0 = select object
            // 1 = move
            // 2 = scale
            // 3 = rotate
            // 4 = instantiate
            // 5 = delete
            // 6 = orbit

            //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "pressed down", e);
            StateManager.Instance.editMode++;
            if (StateManager.Instance.editMode > 6) StateManager.Instance.editMode = 1;

            if (StateManager.Instance.controlledObject == null && StateManager.Instance.instatiateObject == null) StateManager.Instance.editMode = 0;
            if (StateManager.Instance.controlledObject == null && StateManager.Instance.instatiateObject != null) StateManager.Instance.editMode = 4;
            setSelectedObject();

            if (StateManager.Instance.editMode == 6) {
                StateManager.Instance.rigMode = 1; }
            else { StateManager.Instance.rigMode = 0; }
        }

        private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "released", e);
        }

        private void DoButtonTwoTouchStart(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "touched", e);
        }

        private void DoButtonTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "untouched", e);
        }

        private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "pressed down", e);
        }

        private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
        {
           // DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "released", e);
        }

        private void DoControllerEnabled(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "ENABLED", e);
        }

        private void DoControllerDisabled(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "DISABLED", e);
        }

        private void DoControllerIndexChanged(object sender, ControllerInteractionEventArgs e)
        {
          //  DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "INDEX CHANGED", e);
        }
    }
}