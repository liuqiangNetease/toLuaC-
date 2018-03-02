local transform;
local gameObject;

PromptPanel = {};
local this = PromptPanel;

--启动事件--
function PromptPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;

	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function PromptPanel.InitPanel()
	this.btnOpen = transform:FindChild("Open").gameObject;
	this.gridParent = transform:FindChild('ScrollView/Grid');
	logWarn("InitPanel--->>"..gameObject.name);
end

function PromptPanel.Start()	
	logWarn("Start--->>"..gameObject.name);
end

--单击事件--
function PromptPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end