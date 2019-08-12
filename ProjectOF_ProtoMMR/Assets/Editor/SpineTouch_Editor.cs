//#define Active_//이거 비활성화 하고 멀티 셀렉션 다시 진행하기[0726]
#define DEBUG_

#if Active_
#else
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
#if DEBUG_
    Spine_Touch[] SpineScripts;
    private void OnEnable()
    {
        SpineScripts = targets.Cast<Spine_Touch>().ToArray();
        //여러 오브젝트를 선택하여 다수의 컴포넌트에 접근하기위해서는 targets 변수를 사용하여 선택 오브젝트들을 가져옴
    }
#endif

    public override void OnInspectorGUI()
    {
#if DEBUG_
        EditorGUI.BeginChangeCheck();
        //EditorGUI.showMixedValue = SpineScripts.Count<Spine_Touch>() > 1;//선택된 오브젝트가 다수일경우, 아닐경우에따라 수치표시를 결정


        //case1
        var bool_playme = EditorGUILayout.BeginToggleGroup("본인애님 실행", SpineScripts[0].PlayME);
        EditorGUI.indentLevel++; //들여쓰기의 정도
        //~~~~~~~~~~~시리얼된거랑 리스트 맞추기~~~~~~~~~~~~~~~~~~~~~~~##
        SerializedObject animLIST = new SerializedObject(SpineScripts);
        List<string> list_animname = SpineScripts[0].anim_name;
        ShowList(animLIST.FindProperty("anim_name"), "anim");
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
        var bool_isidle = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
        var bool_isloop = EditorGUILayout.Toggle("oop anim?", SpineScripts[0].isLOOP);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();

        //case2
        var bool_playother = EditorGUILayout.BeginToggleGroup("다른애님 실행", SpineScripts[0].PlayOTHER);
        EditorGUI.indentLevel++; //들여쓰기의 정도
        List<string> list_otheranimname = SpineScripts[0].anim_name;
        ShowList(animLIST.FindProperty("anim_name"), "anim");
        var otheranim = (SkeletonAnimation)EditorGUILayout.ObjectField("otheranim.sk", SpineScripts[0].OtherAnim, typeof(SkeletonAnimation), true);
        bool_isidle = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
        bool_isloop = EditorGUILayout.Toggle("loop anim?", SpineScripts[0].isLOOP);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();

        Undo.RecordObjects(SpineScripts, "spine value");

        if (EditorGUI.EndChangeCheck())
        {
            foreach (var SpineScript in SpineScripts)
            {
                SpineScript.PlayME = bool_playme;
                SpineScript.anim_name = list_animname;
                animLIST.ApplyModifiedProperties();//바꾸고 적용시켜주는게 문제인듯
                SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
                SpineScript.isLOOP = EditorGUILayout.Toggle("oop anim?", SpineScripts[0].isLOOP);

                //case 2
                SpineScript.PlayOTHER = bool_playother;
                SpineScript.anim_name = list_otheranimname;
                animLIST.ApplyModifiedProperties();
                SpineScript.OtherAnim = otheranim;
                SpineScript.isIDLE = bool_isidle;
                SpineScript.isLOOP = bool_isloop;


            }
        }
        //case 1
        
#else
        //base.OnInspectorGUI(); //기존의 인스펙터 gui를 유지유무
        var SpineScript = target as Spine_Touch;


        //case 1
        SpineScript.PlayME = EditorGUILayout.BeginToggleGroup("본인애님 실행", SpineScript.PlayME);
        EditorGUI.indentLevel++; //들여쓰기의 정도
        ShowList(serializedObject.FindProperty("anim_name"), "anim");
        SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScript.isIDLE);
        SpineScript.isLOOP = EditorGUILayout.Toggle("oop anim?", SpineScript.isLOOP);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();

        //case 2
        SpineScript.PlayOTHER = EditorGUILayout.BeginToggleGroup("다른애님 실행", SpineScript.PlayOTHER);
        EditorGUI.indentLevel++; //들여쓰기의 정도
        ShowList(serializedObject.FindProperty("anim_name"), "anim");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OtherAnim"));
        SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScript.isIDLE);
        SpineScript.isLOOP = EditorGUILayout.Toggle("loop anim?", SpineScript.isLOOP);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndToggleGroup();
#endif

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

/*  //아이들이 있는지
SpineScript.isIDLE = EditorGUILayout.Toggle("Have Idle anim?", SpineScript.isIDLE);
using(var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(SpineScript.isIDLE)))
{
    if (group.visible)
    {
        EditorGUI.indentLevel++; //들여쓰기의 정도
        SpineScript.isLOOP = EditorGUILayout.Toggle("is loop anim?", SpineScript.isLOOP);
        EditorGUI.indentLevel--;
    }
    else
    {
        EditorGUI.indentLevel++; //들여쓰기의 정도
        SpineScript.isLOOP = EditorGUILayout.Toggle("is loop anim?", SpineScript.isLOOP);
        EditorGUI.indentLevel--;
    }
}*/

/*
SpineScripts[0].PlayME = EditorGUILayout.BeginToggleGroup("본인애님 실행", SpineScripts[0].PlayME);
EditorGUI.indentLevel++; //들여쓰기의 정도
ShowList(serializedObject.FindProperty("anim_name"), "anim");
SpineScripts[0].isIDLE = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
SpineScripts[0].isLOOP = EditorGUILayout.Toggle("oop anim?", SpineScripts[0].isLOOP);
EditorGUI.indentLevel--;
EditorGUILayout.EndToggleGroup();

//case 2
SpineScripts[0].PlayOTHER = EditorGUILayout.BeginToggleGroup("다른애님 실행", SpineScripts[0].PlayOTHER);
EditorGUI.indentLevel++; //들여쓰기의 정도
ShowList(serializedObject.FindProperty("anim_name"), "anim");
EditorGUILayout.PropertyField(serializedObject.FindProperty("OtherAnim"));
SpineScripts[0].isIDLE = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
SpineScripts[0].isLOOP = EditorGUILayout.Toggle("loop anim?", SpineScripts[0].isLOOP);
EditorGUI.indentLevel--;
EditorGUILayout.EndToggleGroup();
*/

/*foreach (var SpineScript in SpineScripts)
        {


            //SpineScript.PlayME = EditorGUILayout.BeginToggleGroup("본인애님 실행", SpineScript.PlayME);
            //EditorGUI.indentLevel++; //들여쓰기의 정도
            //ShowList(serializedObject.FindProperty("anim_name"), "anim");
            //SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScript.isIDLE);
            //SpineScript.isLOOP = EditorGUILayout.Toggle("oop anim?", SpineScript.isLOOP);
            //EditorGUI.indentLevel--;
            //EditorGUILayout.EndToggleGroup();

            ////case 2
            //SpineScript.PlayOTHER = EditorGUILayout.BeginToggleGroup("다른애님 실행", SpineScript.PlayOTHER);
            //EditorGUI.indentLevel++; //들여쓰기의 정도
            //ShowList(serializedObject.FindProperty("anim_name"), "anim");
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("OtherAnim"));
            //SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScript.isIDLE);
            //SpineScript.isLOOP = EditorGUILayout.Toggle("loop anim?", SpineScript.isLOOP);
            //EditorGUI.indentLevel--;
            //EditorGUILayout.EndToggleGroup();
        }*/
#endif