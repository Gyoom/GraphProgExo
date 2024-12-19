using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DispatchKernel : MonoBehaviour
{
    public ComputeShader cs;
    public RenderTexture RT;
    void Start()
    {
        RT = new RenderTexture(512, 512, 1);
        RT.enableRandomWrite = true;
        RT.Create();

        int kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", RT);
        cs.Dispatch(kernel, 64, 64, 1); //1 x 64x64 x 8x8 x 1x1
    }
}
