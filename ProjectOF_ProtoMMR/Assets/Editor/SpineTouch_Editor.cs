#define Active_//이거 비활성화 하고 멀티 셀렉션 다시 진행하기[0726]
#define DEBUG_

#if Active_
#else
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
    }
#endif

    public override void OnInspectorGUI()
    {
#if DEBUG_
        EditorGUI.BeginChangeCheck();
        EditorGUI.showMixedValue = false;

        foreach (var SpineScript in SpineScripts)
        {
            SpineScript.PlayME = EditorGUILayout.BeginToggleGroup("본인애님 실행", SpineScripts[0].PlayME);
            EditorGUI.indentLevel++; //들여쓰기의 정도
            ShowList(serializedObject.FindProperty("anim_name"), "anim");
            SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
            SpineScript.isLOOP = EditorGUILayout.Toggle("oop anim?", SpineScripts[0].isLOOP);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();

            //case 2
            SpineScript.PlayOTHER = EditorGUILayout.BeginToggleGroup("다른애님 실행", SpineScripts[0].PlayOTHER);
            EditorGUI.indentLevel++; //들여쓰기의 정도
            ShowList(serializedObject.FindProperty("anim_name"), "anim");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OtherAnim"));
            SpineScript.isIDLE = EditorGUILayout.Toggle("has idle?", SpineScripts[0].isIDLE);
            SpineScript.isLOOP = EditorGUILayout.Toggle("loop anim?", SpineScripts[0].isLOOP);
            EditorGUI.indentLevel--;
            EditorGUILayout.EndToggleGroup();
        }

        EditorGUI.EndChangeCheck();
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