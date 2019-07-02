using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Windows;

public class AB_build : MonoBehaviour
{
    //에셋 번들 빌드

    [MenuItem("Assets/Build AssetBundles")]
    //pathname/doing
    //"Assets/Build AssetBundles"이 위치에 메뉴아이템을 생성합니다.
    static void BuildAllAssetBundles()
    {
        string AB_directory = "Assets/AssetBundles";
        if (!Directory.Exists(AB_directory))
        {
            Directory.CreateDirectory(AB_directory);
        }

        BuildPipeline.BuildAssetBundles(AB_directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //에셋번들을 빌드할수 있게함 
        //아직 잘모르겠으니까 더 알아보기
    }
    //??클릭하면 빌드다이얼로그와함께 진행표시줄이 표시됩니다?
    // >> 생성된 폴더 우클릭하면 에셋번들을 빌드할수있는 명령이 추가됨
}
