using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TextPopUp : MonoBehaviour {
    
    private bool showGUIText = false;
    private Rect textRect = new Rect(50, 50, 400, 100); 
    public string targetName = "ModelTarget";

    void Start() {
        var observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour) {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus) {
        if (targetStatus.Status == Status.TRACKED ||
            targetStatus.Status == Status.EXTENDED_TRACKED) {
            if (behaviour.TargetName == targetName) {
                showGUIText = true;
                Debug.Log(targetName); 
            } else {
                showGUIText = false;
            }
        } else {
            showGUIText = false;
        }
    }

    void OnGUI() {
        if (showGUIText) {
            if (GUI.Button(textRect, "hiện đi bố xin mày")) {
            }
        }
    }

    void OnDestroy() {
        var observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour) {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}

