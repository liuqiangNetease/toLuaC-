using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LuaModule 
{
    private static Dictionary<string, LuaModule> m_LuaModuleList = new Dictionary<string, LuaModule>();

    public void Load(string path)
    {
        //doFile
        LuaScriptMgr.Instance.DoFile(path);
    }

    public static LuaModule GetModule(string path)
    {
        LuaModule module;
        if(m_LuaModuleList.TryGetValue(path, out module))
        {
            return module;
        }

        return null;
    }

    public static LuaModule CreateModule(string path)
    {       
        LuaModule ret;
        if (m_LuaModuleList.TryGetValue(path, out ret))
        {
            Debug.LogWarning("script always exists: " + path);
            return ret;
        }

        ret = new LuaModule();
        m_LuaModuleList.Add(path, ret);
        return ret;
    }
}
