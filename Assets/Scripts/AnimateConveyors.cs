using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateConveyors : MonoBehaviour {
    public Material conveyorMat;

    private void Update() {
        conveyorMat.mainTextureOffset -= new Vector2(0f, Time.deltaTime * 0.5f);
    }
}
