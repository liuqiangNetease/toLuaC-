using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class SimpleFramework_Manager_ResourceManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("LoadAsset", LoadAsset),
			new LuaMethod("GetLoadedAssetBundle", GetLoadedAssetBundle),
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("UnloadAssetBundle", UnloadAssetBundle),
			new LuaMethod("LoadAssetAsync", LoadAssetAsync),
			new LuaMethod("New", _CreateSimpleFramework_Manager_ResourceManager),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Instance", get_Instance, null),
			new LuaField("BaseDownloadingURL", get_BaseDownloadingURL, set_BaseDownloadingURL),
			new LuaField("Variants", get_Variants, set_Variants),
			new LuaField("AssetBundleManifestObject", null, set_AssetBundleManifestObject),
		};

		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.ResourceManager", typeof(SimpleFramework.Manager.ResourceManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSimpleFramework_Manager_ResourceManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.ResourceManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(SimpleFramework.Manager.ResourceManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework.Manager.ResourceManager.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaseDownloadingURL(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework.Manager.ResourceManager.BaseDownloadingURL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Variants(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, SimpleFramework.Manager.ResourceManager.Variants);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BaseDownloadingURL(IntPtr L)
	{
		SimpleFramework.Manager.ResourceManager.BaseDownloadingURL = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Variants(IntPtr L)
	{
		SimpleFramework.Manager.ResourceManager.Variants = LuaScriptMgr.GetArrayString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AssetBundleManifestObject(IntPtr L)
	{
		SimpleFramework.Manager.ResourceManager.AssetBundleManifestObject = (AssetBundleManifest)LuaScriptMgr.GetUnityObject(L, 3, typeof(AssetBundleManifest));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		SimpleFramework.Manager.ResourceManager obj = (SimpleFramework.Manager.ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		LuaFunction arg2 = LuaScriptMgr.GetLuaFunction(L, 4);
		obj.LoadAsset(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLoadedAssetBundle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = null;
		SimpleFramework.Manager.AssetBundleInfo o = SimpleFramework.Manager.ResourceManager.GetLoadedAssetBundle(arg0,out arg1);
		LuaScriptMgr.PushObject(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		SimpleFramework.Manager.AssetBundleManifestOperation o = SimpleFramework.Manager.ResourceManager.Initialize(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadAssetBundle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		SimpleFramework.Manager.ResourceManager.UnloadAssetBundle(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetAsync(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Type arg2 = LuaScriptMgr.GetTypeObject(L, 3);
		SimpleFramework.Manager.AssetBundleAssetOperation o = SimpleFramework.Manager.ResourceManager.LoadAssetAsync(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

