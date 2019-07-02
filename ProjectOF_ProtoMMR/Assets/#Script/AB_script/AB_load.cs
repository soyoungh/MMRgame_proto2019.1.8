using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AB_load : MonoBehaviour
{
    //https://docs.unity3d.com/kr/2017.4/Manual/AssetBundles-Native.html 참고자료_공식문서
    //https://wergia.tistory.com/32 참고자료
    
    public void LoadOnLocal()
    {
        //로컬저장소용 1.LoadFromFile
        var AB_loading = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "AssetBundles/studyab"));
        print("Folder Path is : " + AB_loading);
        if (AB_loading == null)
        {
            print("failed to load AssetBundle");
            return;
        }
        var prefabs = AB_loading.LoadAsset<GameObject>("AB_Cube");
        Instantiate(prefabs);
    }
    public void LoadOnServer()
    {
        //웹서버용 2.UnityWebRequest
        StartCoroutine(DownloadAndCache());
    }

    IEnumerator DownloadAndCache()
    {
        string BundleUrl = "http://cfile218.uf.daum.net/attach/996F55355CDA60D61C0ED0";
        //"file://" + Application.dataPath + "/AssetBundles/studyab";~~~~~로컬
        //"http://cfile218.uf.daum.net/attach/996F55355CDA60D61C0ED0";~~~~서버
        //서버에 올릴수 없는경우 ("file://"+로컬파일 경로)를 URL에 넣어줌

        var request = UnityWebRequestAssetBundle.GetAssetBundle(BundleUrl);//UnityWebRequest.Get(BundleUrl);
        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject sphere = bundle.LoadAsset<GameObject>("AB_Sphere");

        Instantiate(sphere);
        
    }
    /*
     * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*
     * 남은과제
     *  이미 로드된 같은 에셋번들은 또 다시 로드될수 없는거같음
     *  로드된 에셋번들에서 다른 에셋을 사용하는 방법을 찾아보기 <<
     *  unitywebrequest를 로컬에서 확인하였으니 서버에서도 확인 <<
     *  에셋번들을 로드후 해제하는것도 찾아봐야합니다 <<
     * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*
     */
}
