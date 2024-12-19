using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SoftOutlines : MonoBehaviour
{
    public ComputeShader cs;
    public Camera cam; //cam3
    public Material fullscreen;
    RenderTexture silhouettes; //nettes
    RenderTexture silhouettes_floues;
    public int blur_radius = 5;

    void Start()
    {
        silhouettes = new RenderTexture(Screen.width, Screen.height, 1);
        //silhouettes.enableRandomWrite = true;
        silhouettes.Create();

        silhouettes_floues = new RenderTexture(Screen.width, Screen.height, 1);
        silhouettes_floues.enableRandomWrite = true;
        silhouettes_floues.Create();
    }

    void Update()
    {
        //copier image du jeu (x2)
        Graphics.Blit(cam.targetTexture, silhouettes); //on garde une copie nette
        Graphics.Blit(silhouettes, silhouettes_floues); //on fera le flou surplace ici

        //fx sur l'image copiée
        int kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", silhouettes_floues);
        cs.SetInt("blur_radius", blur_radius);
        cs.Dispatch(kernel, Screen.width / 8, Screen.height / 8, 1);

        //donner 2 textures au material fx en fullscreen : silhouettes floues et nettes
        //il fera la soustraction et le rendu
        fullscreen.SetTexture("_silhouettes", silhouettes);
        fullscreen.SetTexture("_silhouettes_floues", silhouettes_floues);
    }
}
