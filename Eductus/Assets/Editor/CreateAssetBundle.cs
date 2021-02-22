using UnityEditor;

 public class CreateAssetBundle 
{
    [MenuItem("Assets/BuildAssetBundle")]
    static void BuildAssetBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/BuildAssetBundle", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
    }

}
