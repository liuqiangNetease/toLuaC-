require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

PromptCtrlA = {};
local this = PromptCtrlA;

local luaRunner;
local transform;
local gameObject;

--构建函数--
function PromptCtrlA.New()
	logWarn("PromptCtrlA.New--->>");
	return this;
end

function PromptCtrlA.Create()
	logWarn("PromptCtrlA.Create--->>");
	PanelManager:CreatePanel('Prompt', this.OnCreate);
end

function PromptCtrlA.Awake(obj)
	logWarn("PromptCtrlA.Awake--->>");
	gameObject = obj;
	transform = obj.transform;
	
	PromptPanel.Awake(obj)
	PromptCtrlA.OnCreate(obj);
	--PanelManager:CreatePanel('Prompt', this.OnCreate);
end

--启动事件--
function PromptCtrlA.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;

	--panel = transform:GetComponent('UIPanel');
	luaRunner = transform:GetComponent('LuaRunner');
	logWarn("Start lua--->>"..gameObject.name);

	luaRunner:AddClick(PromptPanel.btnOpen, this.OnClick);
	--ResManager:LoadAsset('prompt', 'PromptItem', this.InitPanel);
end

--初始化面板--
function PromptCtrlA.InitPanel(prefab)
	local count = 100; 
	local parent = PromptPanel.gridParent;
	for i = 1, count do
		local go = newObject(prefab);
		go.name = 'Item'..tostring(i);
		go.transform:SetParent(parent);
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
        luaRunner:AddClick(go, this.OnItemClick);

	    local label = go.transform:FindChild('Text');
	    label:GetComponent('Text').text = tostring(i);
	end
	local rtTrans = parent:GetComponent("RectTransform");
	local rowNum = count / 4;
	if count % 4 > 0 then
		rowNum = toInt(rowNum + 1);
	end
	local size = rtTrans.sizeDelta;
	size.y = rowNum * 100 + (rowNum - 1) * 50;
	rtTrans.sizeDelta = size;

	local position = rtTrans.localPosition;
	position.y = -(size.y / 2);
	rtTrans.localPosition = position;
end

--滚动项单击--
function PromptCtrlA.OnItemClick(go)
    log(go.name);
end

--单击事件--
function PromptCtrlA.OnClick(go)
	if TestProtoType == ProtocalType.BINARY then
		this.TestSendBinary();
	end
	if TestProtoType == ProtocalType.PB_LUA then
		this.TestSendPblua();
	end
	if TestProtoType == ProtocalType.PBC then
		this.TestSendPbc();
	end
	if TestProtoType == ProtocalType.SPROTO then
		this.TestSendSproto();
	end
	logWarn("OnClick---->>>"..go.name);
end

--测试发送SPROTO--
function PromptCtrlA.TestSendSproto()
    local sp = sproto.parse [[
    .Person {
        name 0 : string
        id 1 : integer
        email 2 : string

        .PhoneNumber {
            number 0 : string
            type 1 : integer
        }

        phone 3 : *PhoneNumber
    }

    .AddressBook {
        person 0 : *Person(id)
        others 1 : *Person
    }
    ]]

    local ab = {
        person = {
            [10000] = {
                name = "Alice",
                id = 10000,
                phone = {
                    { number = "123456789" , type = 1 },
                    { number = "87654321" , type = 2 },
                }
            },
            [20000] = {
                name = "Bob",
                id = 20000,
                phone = {
                    { number = "01234567890" , type = 3 },
                }
            }
        },
        others = {
            {
                name = "Carol",
                id = 30000,
                phone = {
                    { number = "9876543210" },
                }
            },
        }
    }
    local code = sp:encode("AddressBook", ab)
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteByte(ProtocalType.SPROTO);
    buffer:WriteBuffer(code);
    NetManager:SendMessage(buffer);
end

--测试发送PBC--
function PromptCtrlA.TestSendPbc()
    local path = Util.DataPath.."lua/3rd/pbc/addressbook.pb";

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)

    local addressbook = {
        name = "Alice",
        id = 12345,
        phone = {
            { number = "1301234567" },
            { number = "87654321", type = "WORK" },
        }
    }
    local code = protobuf.encode("tutorial.Person", addressbook)
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteByte(ProtocalType.PBC);
    buffer:WriteBuffer(code);
    NetManager:SendMessage(buffer);
end

--测试发送PBLUA--
function PromptCtrlA.TestSendPblua()
    local login = login_pb.LoginRequest();
    login.id = 2000;
    login.name = 'game';
    login.email = 'jarjin@163.com';
    local msg = login:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteByte(ProtocalType.PB_LUA);
    buffer:WriteBuffer(msg);
    NetManager:SendMessage(buffer);
end

--测试发送二进制--
function PromptCtrlA.TestSendBinary()
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteByte(ProtocalType.BINARY);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    NetManager:SendMessage(buffer);
end

--关闭事件--
function PromptCtrlA.Close()
	PanelManager:ClosePanel(CtrlName.Prompt);
	PromptPanel.OnDestroy();
end