using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Test1 : MonoBehaviour
{
    public ComputeShader cs;
    public RenderTexture RT;
    public Texture2D tex2D;//CPU //g�n�ralement fait pour �tre lu pour texturer des objets
                           //utile en lecture
    Texture2D tex2D_output;

    void rt_to_tex2D(RenderTexture rt, Texture2D tex2D)
    {
        RenderTexture tmp = Camera.main.targetTexture;
        RenderTexture tmp2 = RenderTexture.active;

        Camera.main.targetTexture = rt;
        RenderTexture.active = rt;
        try
        {
            tex2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0, false);
            tex2D.Apply();
        }
        catch
        {
            //print("si ce message apparait hors du mode play, c'est un bug de unity sans cons�quence.");
        }

        RenderTexture.active = tmp2;
        Camera.main.targetTexture = tmp;
    }


    void Start()
    {
        //le script suppose qu'on a une texture carr�e

        tex2D_output = new Texture2D(1024, 1024);
        //^ on a besoin d'une Texture2D s�par�e car la fonction pour write des textures sur le disque
        //^ n�cessite une Texture2D, mais on peut pas aller �crire dans tex2D pcq �a contient notre asset


        RT = new RenderTexture(1024, 1024, 1); //GPU //fait pour qu'on puise render dedans //utile en �criture
        RT.enableRandomWrite = true; //si unity se plaint de UAV flag missing
                                     //note: unity se plaint de UAV flag missing sur les tex2D aussi
                                     //�a veut dire qu'il peut pas �crire dedans, mais c'est normal pour les tex2D
                                     //surtout si les variables pointent vers des texture assets
        RT.Create(); //normalement Unity veut qu'on confirme la cr�ation de notre RenderTex avant d'envoyer �a au GPU
                     //mais �a peut marcher sans apparemment

        //async prog
        //de mani�re g�n�rale on a un temps de transfert/lag entre cpu <> gpu, donc on finit de g�rer les choses avant de les sync avec le gpu
        //par exemple si on manipule le contenu d'une Texture2D, il faut appeler Apply() pour envoyer les modifs

        //Graphics.ConvertTexture(tex2D, RT); //�a fail, il faut plut�t utiliser Blit()
        Graphics.Blit(tex2D, RT);

        int kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", RT);
        cs.Dispatch(kernel, 128, 128, 1); //64x64 groupes de 8x8 threads qui g�rent 1x1 case chacun

        rt_to_tex2D(RT, tex2D_output); //fonction maison

        File.WriteAllBytes("kermit_satan.jpg", ImageConversion.EncodeToJPG(tex2D_output));
    }
}
