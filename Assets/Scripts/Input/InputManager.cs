using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    static private InputManager instance = null;
    public static InputManager Instance {
        get {
            if (instance == null) {
                instance = new InputManager();
            }
            return instance;
        }
    }

    Dictionary<string, float> axisValues = new Dictionary<string, float>();

    public void SetAxis(string axis, float value) {
        if (!axisValues.ContainsKey(axis))
            axisValues.Add(axis, value);
        axisValues[axis] = value;
    }

    private float GetOrAddAxis(string axis) {
        if (!axisValues.ContainsKey(axis))
            axisValues.Add(axis, 0f);
        return axisValues[axis];
    }

    public bool GetButton(string button)
    {
        return Input.GetButton(button);
    }

    public float GetAxis(string axis) {
#if UNITY_EDITOR
        return GetOrAddAxis(axis) + Input.GetAxis(axis);
#elif UNITY_ANDROID || UNITY_IOS
        return GetOrAddAxis(axis);
#elif UNITY_STANDALONE
        return Input.GetAxis(axis);
#endif
    }

    public bool GetUpButton(string axis)
    {
#if UNITY_EDITOR
        float value = Input.GetAxis(axis);
        return value > 0 || GetAxis(axis) > 0.5f;
#elif UNITY_ANDROID || UNITY_IOS
        return GetAxis(axis) > 0.5f;
#elif UNITY_STANDALONE
        float value = Input.GetAxis(axis);
        return value > 0;
#endif
    }

    public bool GetDownButton(string axis)
    {
#if UNITY_EDITOR
        float value = Input.GetAxis(axis);
        return value < 0 || GetAxis(axis) < -0.5f;
#elif UNITY_ANDROID || UNITY_IOS
        return GetAxis(axis) < -0.5f;
#elif UNITY_STANDALONE
        float value = Input.GetAxis(axis);
        return value < 0;
#endif
    }

    public bool GetLeftButton(string axis)
    {
#if UNITY_EDITOR
        float value = Input.GetAxis(axis);
        return value < 0 || GetAxis(axis) < -0.5f;
#elif UNITY_ANDROID || UNITY_IOS
        return GetAxis(axis) < -0.5f;
#elif UNITY_STANDALONE
        float value = Input.GetAxis(axis);
        return value < 0;
#endif
    }

    public bool GetRightButton(string axis)
    {
#if UNITY_EDITOR
        float value = Input.GetAxis(axis);
        return value > 0 || GetAxis(axis) > 0.5f;
#elif UNITY_ANDROID || UNITY_IOS
        return GetAxis(axis) > 0.5f;
#elif UNITY_STANDALONE
        float value = Input.GetAxis(axis);
        return value > 0;
#endif
    }
}
