using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderGraphController : MonoBehaviour
{
    float displacementAmount = 0f;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Amount", displacementAmount);

        if (Input.GetMouseButtonDown(0))
            displacementAmount += 1;
    }
}