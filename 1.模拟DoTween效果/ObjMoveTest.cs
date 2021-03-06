using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
1)实现 move(GameObjct gameObject, Vector3 begin, Vector3 end, float time, bool pingpong){ } 
使 gameObject 在 time 秒内，从 begin 移动到 end，若 pingpong 为 true，则在结束时 使 gameObject 在 time 秒内从 end 移动到 begin，如此往复。
2)在上题基础上实现 easeIn easeOut easeInOut 动画效果
*/
public class ObjMoveTest : MonoBehaviour
{
    public enum EaseType
    {
        Linear = 0,
        EaseIn,
        EaseOut,
        EaseInOut,
    }

    public GameObject Obj;
    public Transform StartTrans;
    public Transform EndTrans;
    public bool IsPingpong;

    public EaseType CurEase = EaseType.Linear;

    private bool _isStart = false;
    private bool _isPingpong = false;
    private float _timer = 0;
    private int _loopTimes = 0;
    private float _realMotionProgress = 0;
    private float duration = 0;
    private Vector3 _beginPoint = Vector3.zero;
    private Vector3 _endPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Move(Obj, StartTrans.position, EndTrans.position, 5f, IsPingpong, CurEase);
        //Move(Obj, StartTrans.position, EndTrans.position, 5f, true, EaseType.EaseIn);
        //Move(Obj, StartTrans.position, EndTrans.position, 5f, true, EaseType.EaseOut);
        //Move(Obj, StartTrans.position, EndTrans.position, 5f, true, EaseType.EaseInOut);

    }

    private void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, EaseType easeType)
    {
        _beginPoint = begin;
        _endPoint = end;
        duration = time;
        _isPingpong = pingpong;
        CurEase = easeType;
        _isStart = true;
    }

    private void Update()
    {
        if (!_isStart)
            return;

        _timer += Time.deltaTime;

        _realMotionProgress = GetMotionProgressWithEaseType(Mathf.Clamp((_timer - (_loopTimes * duration)) / duration, 0, 1), CurEase);

        Obj.transform.position = Vector3.Lerp(_beginPoint, _endPoint, _realMotionProgress);

        if (_realMotionProgress == 1)
        {
            if (!_isPingpong)
                _isStart = false;
            else
            {
                _loopTimes++;
                Vector3 temp = _beginPoint;
                _beginPoint = _endPoint;
                _endPoint = temp;
            }
        }
    }

    private float GetMotionProgressWithEaseType(float progress, EaseType easeType)
    {
        switch (easeType)
        {
            case EaseType.Linear:
                return Mathf.Lerp(0, 1, progress);//插值运算
            case EaseType.EaseIn:
                return progress * progress * progress;//使用函数x^3在[0-1]区间的值
            case EaseType.EaseOut:
                progress--;
                return (progress * progress * progress + 1) + 0;//使用函数(x-1)^3+1在[0-1]区间的值
            case EaseType.EaseInOut:
                //分段函数，使用函数4*x^3在[0-0.5]区间的值 使用函数1-(-2*x+2)^3/2在[0.5-1]区间的值
                return progress < 0.5 ? 4 * progress * progress * progress : 1 - Mathf.Pow(-2 * progress + 2, 3) / 2;
            default:
                break;
        }
        return 0;
    }
}
