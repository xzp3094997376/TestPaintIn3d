using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AROperate;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ModelControl : BaseEventModel
{
    public override void Start()
    {
        base.Start();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Model_Scale(InputType inputType = InputType.Mouse)
    {
        base.Model_Scale(inputType);

    }
    public override void Model_Rotate(InputType inputType = InputType.Mouse)
    {
        base.Model_Rotate(inputType);

    }


}
#if UNITY_EDITOR

[CustomEditor(typeof(ModelControl))]
public class BaseEventViewEditor : Editor
{
    public static ModelControl get;
    public void OnEnable()
    {
        get = (ModelControl)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal("Box");
        {
            if (GUILayout.Button("动画"))
            {
                //if (get.gameObject.GetComponent<BaseEventView_Animation>() == null)
                //{
                //    get.gameObject.AddComponent<BaseEventView_Animation>();
                //    get.gameObject.GetComponent<Animation>().playAutomatically = false;
                //}
            }
            if (GUILayout.Button("未开发"))
            {
                //if (get.gameObject.GetComponent<BaseEventModel_Animation>() == null)
                //    get.gameObject.AddComponent<BaseEventModel_Animation>();
            }
            if (GUILayout.Button("未开发"))
            {
                //if (get.gameObject.GetComponent<BaseEventModel_Animation>() == null)
                //    get.gameObject.AddComponent<BaseEventModel_Animation>();
            }
            if (GUILayout.Button("未开发"))
            {
                //if (get.gameObject.GetComponent<BaseEventModel_Animation>() == null)
                //    get.gameObject.AddComponent<BaseEventModel_Animation>();
            }
            if (GUILayout.Button("未开发"))
            {
                //if (get.gameObject.GetComponent<BaseEventModel_Animation>() == null)
                //    get.gameObject.AddComponent<BaseEventModel_Animation>();
            }
        }
        GUILayout.EndHorizontal();
    }
}
#endif