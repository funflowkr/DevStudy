using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

public class CustomSRP : RenderPipeline
{
    /// <summary>
    /// context 그래픽 하드웨어와 통신에 사용되는 변수
    /// </summary>
    public override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        base.Render(context, cameras);

        for (int i = 0; i < cameras.Length; ++i)
        {
            var camera = cameras[i];
            //Culling 현재 카메라에 보이는 부분
            ScriptableCullingParameters cullingParams;
            if (!CullResults.GetCullingParameters(camera, out cullingParams))
                continue;

            CullResults cull = CullResults.Cull(ref cullingParams, context);

            // 카메라 세팅 (랜더 타겟, view/projection matrices등)
            context.SetupCameraProperties(camera);

            VisibleLight light = cull.visibleLights[0];
            Vector3 lightdir = -light.localToWorld.GetColumn(2);

            // clear depth buffer
            var cmd = new CommandBuffer();
            cmd.ClearRenderTarget(true, false, Color.black);
            context.ExecuteCommandBuffer(cmd);
            cmd.Release();


            cmd = new CommandBuffer();
            cmd.SetGlobalVector("_LightDir", new Vector4(lightdir.x, lightdir.y, lightdir.z, 0.0f));
            //cmd.SetGlobalVector("_LightDir", new Vector4(0, 1, 0, 0.0f));
            context.ExecuteCommandBuffer(cmd);
            cmd.Release();

            // 불투명 오브젝트 그리기
            var settings = new DrawRendererSettings(camera, new ShaderPassName("ForwardBase")); //셰이더 스크립트의 Pass 부분과 일치
            settings.sorting.flags = SortFlags.CommonOpaque;

            var filterSetting = new FilterRenderersSettings(true) { renderQueueRange = RenderQueueRange.opaque };
            context.DrawRenderers(cull.visibleRenderers, ref settings, filterSetting);

            context.DrawSkybox(camera); //스카이박스 그리기

            context.Submit();
        }
    }
}
