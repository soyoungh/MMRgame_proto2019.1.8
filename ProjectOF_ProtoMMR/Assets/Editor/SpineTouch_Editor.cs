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
    SkeletonAnimation GetOtherAnim;
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
        var bool_playme = EditorGUILayout.Toggle("#본인애님 실행", SpineScripts[0].PlayME);
        using (var group1 = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(bool_playme)))
        {
            if(group1.visible == true)
            {
                EditorGUI.indentLevel++; //들여쓰기의 정도
                //~~~~~~~~~~~시리얼된거랑 리스트 맞추기~~~~~~~~~~~~~~~~~~~~~~~##
                list_animname = SpineScripts[0].anim_name;
                ShowList(animLIST.FindProperty("anim_name"), "name_");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
                EditorGUI.indentLevel--;
            }
        }
        //case2
        var bool_playother = EditorGUILayout.Toggle("#다른애님 실행", SpineScripts[0].PlayOTHER);
        using (var group2 = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(bool_playother)))
        {
            if(group2.visible == true)
            {
                EditorGUI.indentLevel++; //들여쓰기의 정도
                GetOtherAnim = (SkeletonAnimation)EditorGUILayout.ObjectField("가져올 다른 애니메이션", SpineScripts[0].OtherAnim, typeof(SkeletonAnimation), true);
                list_otheranimname = SpineScripts[0].anim_name;
                ShowList(animLIST.FindProperty("anim_name"), "name_");
                EditorGUI.indentLevel--;
            }
        }
        Undo.RecordObjects(SpineScripts, "spine value");

        if (EditorGUI.EndChangeCheck())
        {
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
                SpineScript.OtherAnim = GetOtherAnim;
                animLIST.ApplyModifiedProperties();
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