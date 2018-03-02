using UnityEngine;
using System.Collections;
using System.IO;
using LuaInterface;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using SimpleFramework.Manager;
using SimpleFramework;


public class LuaRunner : MonoBehaviour {

//#if UNITY_EDITOR
//    public UnityEditor.DefaultAsset LuaEditorPath;
//#endif
    
    public string LuaPath = null;

    public string LuaTabName = null;

    private List<LuaFunction> buttons = new List<LuaFunction>();

    LuaModule m_Module;
    
    public string GetLuaPath()
    {
//#if UNITY_EDITOR
//        string path = UnityEditor.AssetDatabase.GetAssetPath(LuaEditorPath);
//        string token = "Assets/Lua/";
//        bool bContains = path.Contains(token);
//        if(bContains)
//        {
//            LuaPath = path.Substring(token.Length, path.Length - token.Length);
//        }
//        return LuaPath;
//#else

        return LuaPath + LuaTabName + ".lua";
//#endif

    }

    /// <summary>
    /// 执行Lua方法
    /// </summary>
    protected object[] CallMethod(string func, params object[] args)
    {
        if( GameManager.IsInited())
            return SimpleFramework.Util.CallMethod(LuaTabName, func, args);
        return null;
    }
	// Use this for initialization
	void Awake () 
    {
        string path = GetLuaPath();
        LoadScript(path);

        CallMethod("Awake",gameObject);
	}

    void Start()
    {       
       
            //LuaState l = LuaScriptMgr.Instance.lua;
            //l[name + ".transform"] = transform;
            //l[name + ".gameObject"] = gameObject;
        //CallMethod("Awake", gameObject);
        CallMethod("Start");
    }

    void Update()
    {
        CallMethod("Update");
    }

    void LoadScript(string path)
    {
        string ext = Path.GetExtension(path);
        if (ext.ToLower().Equals(".lua"))
        {
            m_Module = LuaModule.CreateModule(path);
            m_Module.Load(path);
        }
        else
            Debug.LogWarning("expect a lua file" + path);

    }

    protected void OnClick()
    {
        CallMethod("OnClick");
    }

    protected void OnClickEvent(GameObject go)
    {
        CallMethod("OnClick", go);
    }

    /// <summary>
    /// 添加单击事件
    /// </summary>
    public void AddClick(GameObject go, LuaFunction luafunc)
    {
        if (go == null) return;
        buttons.Add(luafunc);
        go.GetComponent<Button>().onClick.AddListener(
            delegate()
            {
                luafunc.Call(go);
            }
        );
    }

    /// <summary>
    /// 清除单击事件
    /// </summary>
    public void ClearClick()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null)
            {
                buttons[i].Dispose();
                buttons[i] = null;
            }
        }
    }

    protected void OnDestroy()
    {
        ClearClick();
        //LuaManager = null;
#if ASYNC_MODE
        string abName = name.ToLower().Replace("panel", "");
        ResourceManager.UnloadAssetBundle(abName + AppConst.ExtName);
#endif
        Util.ClearMemory();
        Debug.Log("~" + name + " was destroy!");
    }

}
