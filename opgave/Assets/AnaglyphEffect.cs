using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AnaglyphEffect : MonoBehaviour
{
    public Material material;
    public Camera cam2;

    private RenderTexture rtLeft;
    private RenderTexture rtRight;

    private void OnEnable()
    {
        if (material == null)
        {
            return;
        }

        cam2.enabled = false;
        int w = Screen.width, h = Screen.height;

        rtLeft = RenderTexture.GetTemporary(w, h, 8, RenderTextureFormat.Default);
        rtRight = RenderTexture.GetTemporary(w, h, 8, RenderTextureFormat.Default);

        cam2.targetTexture = rtRight;


    }

    private void OnDisable()
    {
        if (rtLeft != null) { rtLeft.Release(); }
        if (rtRight != null) { rtRight.Release(); }

        cam2.targetTexture = null;
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
        if (material == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        cam2.Render();

        material.SetTexture("_MainTex", source);
        material.SetTexture("_MainTex2", rtRight);

        Graphics.Blit(source, destination, material);
    }
}