using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Spine.Unity;
using System;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(Spine_Touch))]
public class SpineTouch_Editor : Editor
{
    Spine_Touch[] SpineScripts;
    List<string> list_animname;
    List<string> list_otheranimname;
    bool bool_isidle;
    bool bool_isloop;
    List<SkeletonAnimation> GetOtherAnim = new List<SkeletonAnimation>();
    private void OnEnable()
    {
        SpineScripts = targets.Cast<Spine_Touch>().ToArray();
        //여러 오브젝트를 선택하여 다수의 컴포넌트에 접근하기위해서는 targets 변수를 사용하여 선택 오브젝트들을 가져옴
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        SerializedObject animLIST = new SerializedObject(SpineScripts);

        bool_isidle = EditorGUILayout.Toggle("idle 유뮤", SpineScripts[0].isIDLE);
        bool_isloop = EditorGUILayout.Toggle("loop 여부", SpineScripts[0].isLOOP);

        //case1
        var bool_playme = EditorGUILayout.BeginToggleGroup("#본인애님 실행", SpineScripts[0].PlayME);//토글로 묶지않고 using써서 하면 list사이즈 에러생김
        EditorGUI.indentLevel++; //들여쓰기의 정도
        //~~~~~~~~~~~시리얼된거랑 리스트 맞추기~~~~~~~~~~~~~~~~~~~~~~~##
        list_animname = SpineScripts[0].anim_name;
        ShowList(animLIST.FindProperty("anim_name"), "name_");
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();

        //case2
        var bool_playother = EditorGUILayout.BeginToggleGroup("#다른애님 실행", SpineScripts[0].PlayOTHER);
        EditorGUI.indentLevel++; //들여쓰기의 정도
        foreach(var spinescript in SpineScripts)
        {
            GetOtherAnim.Add((SkeletonAnimation)EditorGUILayout.ObjectField("가져올 다른 애니메이션", spinescript.OtherAnim, typeof(SkeletonAnimation), true));
        }
        //게임오브젝트를 리스트로 만들어 주고 해당 스크립트에 해당 오브젝트를 적용
        list_otheranimname = SpineScripts[0].anim_name;
        ShowList(animLIST.FindProperty("anim_name"), "name_");
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();

        Undo.RecordObjects(SpineScripts, "spine value");

        if (EditorGUI.EndChangeCheck())
        {
            int i = 0;
            foreach (var SpineScript in SpineScripts)
            {
                SpineScript.isIDLE = bool_isidle;
                SpineScript.isLOOP = bool_isloop;
                SpineScript.PlayME = bool_playme;
                SpineScript.anim_name = list_animname;
                animLIST.ApplyModifiedProperties();//바꾸고 적용시켜주는게 문제인듯

                //case 2
                SpineScript.PlayOTHER = bool_playother;
                SpineScript.anim_name = list_otheranimname;
                SpineScript.OtherAnim = GetOtherAnim[i];
                animLIST.ApplyModifiedProperties();
                i++;
            }
        }
    }

    public void ShowList(SerializedProperty ListProperty, string ListName)
    {
        if(ListProperty.isExpanded =  EditorGUILayout.Foldout(ListProperty.isExpanded, ListProperty.name))
        {
            EditorGUILayout.PropertyField(ListProperty.FindPropertyRelative("Array.size"));
            int count = ListProperty.arraySize;
            for (int i = 0; i < count; i++)
            {
                EditorGUILayout.PropertyField(ListProperty.GetArrayElementAtIndex(i), new GUIContent(ListName + i));
            }
        }
    }
}