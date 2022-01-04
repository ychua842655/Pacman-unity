using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[DisallowMultipleComponent]
[ExecuteAlways]
public class CanvasAdapter : MonoBehaviour
{
    //public enum AdaptType {
    //    Content,
    //    Background
    //};
    //public AdaptType type = AdaptType.Content;
    private CanvasScaler mCanvasScaler = null;

    private void Awake()
    {
        mCanvasScaler = GetComponent<CanvasScaler>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //    if (mCanvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
        //    {
        //        return;
        //    }
        //    mCanvasScaler.matchWidthOrHeight = GetMatchValue();
    }

    private void OnEnable()
    {
        if (mCanvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            return;
        }
        mCanvasScaler.matchWidthOrHeight = GetMatchValue();
    }

    private float GetMatchValue()
    {

        var canvas = GetComponent<Canvas>();
        //float match = 0;
        CanvasScaler cs = mCanvasScaler;
        float dR = cs.referenceResolution.x * 1.0f / cs.referenceResolution.y;

        float sR = canvas.pixelRect.width * 1f / canvas.pixelRect.height;
        bool adaptHeight = sR > dR;
        //Debug.Log(string.Format("dR:{0},sR:{1} screen size :{2},{3}",dR,sR, canvas.pixelRect.width, canvas.pixelRect.height));
        //if (type == AdaptType.Background && !adaptHeight) {
        //    adaptHeight = !adaptHeight;
        //}
        return adaptHeight ? 1 : 0;
    }


    // Update is called once per frame
    void Update()
    {

    }
}