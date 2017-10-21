using UnityEngine;
using System;

public class DebugUtil
{
    public enum Level
    {
        Log = 0,
        Warning,
        Error,
        None,
    };

    private static Level level = Level.Log;

    public static void setLevel(Level l)
    {
        level = l;
    }

    public static void Log(object obj = null)
    {
        if (level <= Level.Log)
        {
            UnityEngine.Debug.Log("[" + Time.frameCount + "]" + obj);
        }
    }

    public static void LogWarning(object obj = null)
    {
        if (level <= Level.Warning)
        {
            UnityEngine.Debug.LogWarning("[" + Time.frameCount + "]" + obj);
        }
    }


    public static void LogError(object obj = null)
    {
        if (level <= Level.Error)
        {
            UnityEngine.Debug.LogError("[" + Time.frameCount + "]" + obj);
        }
    }

    public static void LogException(System.Exception obj = null)
    {
        if (level <= Level.Error)
        {
            UnityEngine.Debug.LogException(obj);
        }
    }

    public static void LogFormat(string format, params object[] pars)
    {
        if (level <= Level.Log)
        {
            UnityEngine.Debug.LogFormat(format, pars);
        }
    }

    public static void LogErrorFormat(string format, params object[] pars)
    {
        if (level <= Level.Error)
        {
            UnityEngine.Debug.LogErrorFormat(format, pars);
        }
    }

    public static void Assert(bool expression)
    {
        if (!expression)
            throw new Exception("Assertion failed");
    }
}