using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AROperate
{
    [System.Serializable]
    public class Bind
    {
        [Header("动画片段名称")]
        public string clipName;
        [Header("执行动画事件的按钮")]
        public Button btn;
        [Header("按钮上的显示文字内容")]
        public string btnContent;
        [Header("是否循环播放")]
        public bool isLoop = false;
        [Header("是否一开始播放")]
        public bool isPlayOnStart = false;
    }
    public enum InputType
    {
        None,//不可操作的情况
        Mouse,
        Gesture,
    }
    /// <summary>
    /// 基础功能 接口
    /// </summary>
    public interface IBaseEvent
    {
        /// <summary>
        /// 模型缩放
        /// </summary>
        void Model_Scale(InputType inputType = InputType.Mouse);
        /// <summary>
        /// 模型旋转
        /// </summary>
        void Model_Rotate(InputType inputType = InputType.Mouse);
    }
    public interface IBaseEventAnimation
    {
        void Play(Bind bindinfo);
    }
}