using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace AROperate
{
    public class BaseEventModel : MonoBehaviour, IBaseEvent
    {
        [Header("控制方式（不可操作，鼠标，手势）")]
        public InputType inputType = InputType.Gesture;
        public readonly string DEFAULTANIMATIONCLIPNAME = "Take 001";
        private Touch oldTouch1;  //上次触摸点1(手指1)
        private Touch oldTouch2;  //上次触摸点2(手指2)
        private float speed = 0.3f;
        private Vector3 rot;
        private float scale;
        private float minScale;
        private float maxScale;

        private float t1;
        private float t2;
        private bool isStop;
        private Camera cam;
        [HideInInspector]
        public Animation model;
        [HideInInspector]
        public static bool isCanScale = true;
        [HideInInspector]
        public static bool isCanRotate = true;
        [HideInInspector]
        public static bool isCanRotateDiscuss = true;
        [Header("是否可以双击模型，暂停动画")]
        public bool isClickModelStopAnimation = false;

        public virtual void Start()
        {
            rot = transform.localEulerAngles;
            scale = transform.localScale.x;
            minScale = scale / 2f;
            maxScale = scale * 2f;
            isStop = false;
#if MODEL_CONTROL_CAERMA_EVENT
            cam = Common._instance.UICamera;
#else
            cam = Camera.main;
#endif

        }
        public virtual void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isClickModelStopAnimation)
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        try
                        {
                            //旧版本
                            if (hit.collider.name == model.name)
                            {
                                t2 = Time.realtimeSinceStartup;
                                if (t2 - t1 < 0.3f)
                                {
                                    isStop = !isStop;
                                    if (isStop)
                                    {
                                        model["Take 001"].speed = 0;
                                    }
                                    else
                                    {
                                        model["Take 001"].speed = 1;
                                        model.Play();
                                    }
                                }
                                t1 = t2;
                            }
                        }
                        catch (System.Exception)
                        {
                            //新版本

                            t2 = Time.realtimeSinceStartup;
                            if (t2 - t1 < 0.3f)
                            {
                                isStop = !isStop;
                                if (isStop)
                                {
                                    //if (GetComponent<BaseEventView_Animation>())
                                    //{
                                    //    GetComponent<BaseEventView_Animation>().SwitchPause();
                                    //}
                                }
                                else
                                {
                                    //model["Take 001"].speed = 1;
                                    //model.Play();
                                    //if (GetComponent<BaseEventView_Animation>())
                                    //{
                                    //    GetComponent<BaseEventView_Animation>().SwitchPause();
                                    //}
                                }
                            }
                            t1 = t2;

                        }

                    }
                }
            }
            if (Input.touchCount <= 0)
            {
                return;
            }
            Model_Scale(inputType);
            Model_Rotate(inputType);
        }
        /// <summary>
        /// 模型缩放
        /// </summary>
        public virtual void Model_Rotate(InputType inputType = InputType.Mouse)
        {
            if (inputType == InputType.Mouse)
            {
                Debug.Log("我正在通过鼠标旋转");
            }
            else if (inputType == InputType.Gesture)
            {
                Debug.Log("我正在通过手势旋转");
            }

            if (2 == Input.touchCount && isCanScale)
            {
                //多点触摸, 放大缩小
                Touch newTouch1 = Input.GetTouch(0);
                Touch newTouch2 = Input.GetTouch(1);

                //第2点刚开始接触屏幕, 只记录，不做处理
                if (newTouch2.phase == TouchPhase.Began)
                {
                    oldTouch2 = newTouch2;
                    oldTouch1 = newTouch1;
                    return;
                }

                //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型
                float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

                //两个距离之差，为正表示放大手势， 为负表示缩小手势
                float offset = newDistance - oldDistance;

                //放大因子， 一个像素按 0.01倍来算(100可调整)
                float scaleFactor = offset / 100f * speed;
                Vector3 localScale = transform.localScale;
                Vector3 scale = new Vector3(localScale.x + scaleFactor, localScale.y + scaleFactor, localScale.z + scaleFactor);

                //最小缩放到 0.3 倍
                if ((scale.x > minScale && scale.y > minScale && scale.z > minScale) && (scale.x < maxScale && scale.y < maxScale && scale.z < maxScale))
                {
                    transform.localScale = scale;
                }

                //记住最新的触摸点，下次使用
                oldTouch1 = newTouch1;
                oldTouch2 = newTouch2;
            }


        }
        /// <summary>
        /// 模型旋转
        /// </summary>
        public virtual void Model_Scale(InputType inputType = InputType.Mouse)
        {
            if (inputType == InputType.Mouse)
            {
                Debug.Log("我正在通过鼠标缩放");
            }
            else if (inputType == InputType.Gesture)
            {
                Debug.Log("我正在通过手势缩放");
            }
            if (isCanRotate && isCanRotateDiscuss)
            {
                //单点触摸， 水平上下旋转
                if (1 == Input.touchCount)
                {
                    if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.tag == "Slide")
                    {
                        //Debug.Log("点击到UGUI的UI界面，会返回true");
                        return;
                    }

                    Touch touch = Input.GetTouch(0);
                    Vector2 deltaPos = touch.deltaPosition * speed;
                    transform.Rotate(Vector3.down * deltaPos.x, Space.World);
                    transform.Rotate(Vector3.right * deltaPos.y, Space.World);
                }
            }
        }
        public virtual void OnDisable()
        {
            //transform.localEulerAngles = rot;
            //transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}