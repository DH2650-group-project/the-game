
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        [Range(144, 1024)]
        public int pixelHeight = 256;

        public bool renderInSceneView = false;
    }



    [SerializeField] private CustomPassSettings settings;
    private PixelizePass customPass;

    public override void Create()
    {
        customPass = new PixelizePass(settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera && !settings.renderInSceneView)
            return;
#endif
        renderer.EnqueuePass(customPass);
    }

}
