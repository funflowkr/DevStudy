using UnityEngine;
using UnityEngine.Experimental.Rendering;

/// <summary>
/// 랜더파이프 라인 에셋 생성 
/// 생성된 에셋을 Project Setting -> Graphics -> Scriptable Render Pipeline Setting 에 설정하면
/// 커스텀한 랜더파이프라인이 적용된다
/// </summary>
public class CustomSRPAsset : RenderPipelineAsset
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("SRP_Demo/Create RenderPipeline Asset")]
    static void CreateBasicAssetPipline()
    {
        var instance = ScriptableObject.CreateInstance<CustomSRPAsset>();
        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/SRPAsset/CustomSRPAsset.asset");
    }
#endif

    protected override IRenderPipeline InternalCreatePipeline()
    {
        return new CustomSRP();
    }
}
