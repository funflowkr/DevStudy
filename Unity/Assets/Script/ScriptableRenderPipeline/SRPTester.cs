using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SRPTester : MonoBehaviour
{
    public RenderPipelineAsset srpAsset = null;
    // Start is called before the first frame update
    void Start()
    {
        if (srpAsset != null)
            UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset = srpAsset;
    }
}
