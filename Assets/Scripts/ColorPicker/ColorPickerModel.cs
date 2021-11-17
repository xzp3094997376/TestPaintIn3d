using System.Collections;
using System.Collections.Generic;
using ColorUiTools;
using PaintIn3D;
using UnityEngine;
using  UnityEngine.UI;

public class ColorPickerModel : MonoBehaviour
{
    /// <summary>
    /// 颜色选择器
    /// </summary>
    public ColorPicker ColorPicker = null;
    /// <summary>
    /// 猪的图片
    /// </summary>
    public P3dPaintSphere Image = null;

    public Button colorOpenBtn, colorCloseBtn,mCtrlBtn;

    public ModelControl modelCtrl;

    public GameObject colorPickerObj;

    private void Awake()
    {
        ColorPicker.onPicker.AddListener(color =>
        {
            // 设置猪的颜色
            Image.Color = color;
        });
    }

    void Start()
    {
        colorOpenBtn.onClick.AddListener(() =>
        {
            colorOpenBtn.gameObject.SetActive((false));
            colorPickerObj.SetActive(true);
        });
        colorCloseBtn.onClick.AddListener(() =>
        {
            colorOpenBtn.gameObject.SetActive((true));
            colorPickerObj.SetActive(false);
        });
        mCtrlBtn.onClick.AddListener(() =>
        {
            modelCtrl.enabled = !modelCtrl.enabled;
        });
    }
}
