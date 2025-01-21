using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test2 : MonoBehaviour
{
    public ComputeShader cs;
    public Camera cam;
    public Material fullscreen;
    RenderTexture RT;

    void Start()
    {
        RT = new RenderTexture(Screen.width, Screen.height, 1);
        RT.enableRandomWrite = true;
        RT.Create();
    }

    void Update()
    {
        //copier image du jeu
        Graphics.Blit(cam.targetTexture, RT);

        //fx sur l'image copiée
        int kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", RT);
        cs.Dispatch(kernel, Screen.width / 8, Screen.height / 8, 1);
        //^ pour du 1080p, ça va dispatch 240*135 groupes de 8x8 threads, qui feront 1x1 pixels
        //pour l'instant

        //donner texture fx'ed au material en fullscreen
        fullscreen.SetTexture("_Texture2D", RT);
    }
}

