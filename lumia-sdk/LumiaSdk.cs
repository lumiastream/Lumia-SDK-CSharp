using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lumia
{
    public delegate void NotifyInternal(string data);
    public delegate void Notify(JObject data);

    class LumiaSdk
    {
        private WebSocket ws;

        public LumiaSdk(string? token = null, string? name = null, string? host = null)
        {
            if (token != null) token_ = token;
            if (name != null) name_ = name;
            if (host != null) host_ = host;
        }

        public Task<bool> init(string? token = null, string? name = null, string? host = null)
        {
            if (token != null) token_ = token;
            if (name != null) name_ = name;
            if (host != null) host_ = host;

            return startWs();

        }

        private Task<bool> startWs()
        {
            var promise = new TaskCompletionSource<bool>();
            var host = host_ + "/api?token=" + token_ + "&name=" + name_;
            ws = new WebSocket(host);
            
            ws.OnOpen += (sender, e) => {
                promise.SetResult(true);
                isConnected = true;
                connected?.Invoke("");
            };
            
            ws.OnMessage += (sender, e) => {

                string t;
                if (e.IsBinary)
                {
                     t = System.Text.Encoding.Default.GetString(e.RawData);
                }
                else
                {
                    t = e.Data;
                }

              
                try
                {
                    JObject data = JObject.Parse(t);
                   
                    if (data["context"] == null || cbs[data["context"].Value<string>()] == null)
                    {
                        events?.Invoke(data);
                    }
                    else
                    {
                        string ctx = data["context"].Value<string>();
                        data.Property("context").Remove();
                        cbs[ctx].SetResult(data);

                    }
                }
                catch (Exception err)
                {
                    error?.Invoke(err.Message);
                }
            };

            ws.OnError += (sender, e) => {
                error?.Invoke(e.Message);
            };

            ws.OnClose += (sender, e) => {
                isConnected = false;
                closed?.Invoke(e.Reason);
                if (e.Code == 1006)
                {
                    Task.Delay(2000).ContinueWith(async (t) => startWs());
                } else if  (e.Code == 1002 || e.Reason == "Invalid HTTP status.")
                {
                    stoped = true;
                }else
                {
                    startWs();
                }
            };

            ws.Connect();
            return promise.Task;
        }

        private Task<JObject> sendWsMessage(JObject o)
        {
            var promise = new TaskCompletionSource<JObject>();
            cbs[(++event_count).ToString()] = promise;
            o["context"] = event_count.ToString();
            ws.Send(o.ToString(Newtonsoft.Json.Formatting.None));
            return promise.Task;
        }

        public Task<JObject> getInfo()
	    {
            JObject o = new JObject();
            o["method"] = "retrieve";
		    o["retrieve"] = true;
		    return sendWsMessage(o);
        }

        public Task<JObject> send(ILumiaSdkSendPack pack)
        {

            JObject o = JObject.FromObject(pack);
            o["lsorigin"] = "lumia-sdk";
            return sendWsMessage(o);
        }
        
        public Task<JObject> sendAlert(LumiaSDKAlertValues alert)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.ALERT;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = LumiaUtils.getTypeValue<LumiaSDKAlertValues>(alert);
            return send(pack);
        }

        // Sends command
        public Task<JObject> sendCommand(string command, bool? default_ = null, bool? skipQueue = null)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.CHAT_COMMAND;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = command;
            pack.params_.hold = default_;
            pack.params_.skipQueue = skipQueue;
            return send(pack);
        }

        // Sends a color pack
        public Task<JObject> sendColor(
			    RGB color,
			    int? brightness = null, // 0-100
                int? duration = null,	 // In milliseconds
                int? transition = null, // In milliseconds
                bool? default_ = null,
			    bool? skipQueue = null,
			    List<ILumiaSdkLight>? lights = null)
        {


            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.CHAT_COMMAND;
            pack.params_ = new LumiaSDKPackParams();

            pack.type = LumiaSDKCommandTypes.RGB_COLOR;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.brightness = brightness;
            pack.params_.duration = duration;
            pack.params_.transition = transition;
            pack.params_.hold = default_;
            pack.params_.skipQueue = skipQueue;
            pack.params_.lights = lights;
            return send(pack);
        }

        // Sends brightness only
        public Task<JObject> sendBrightness(int brightness, int? transition = null, bool? skipQueue = null)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.RGB_COLOR;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.brightness = brightness;
            pack.params_.transition = transition;
            pack.params_.skipQueue = skipQueue;
            return send(pack);
        }

        // Sends tts
        public Task<JObject> sendTts(string text, int? volume = null, string? voice = null)
        {
            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.TTS;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = text;
            pack.params_.volume = volume;
            pack.params_.voice = voice;
            return send(pack);
        }

        // Sends Chatbot message
        public Task<JObject> sendChatbot(Platforms platform, string text)
        {
            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaSDKCommandTypes.CHATBOT_MESSAGE;
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = text;
            pack.params_.platform = platform;
            return send(pack);
        }


        public Task<JObject> stop()
        {

            JObject o = new JObject();
            o["method"] = "stop";
            var r = sendWsMessage(o).GetAwaiter().GetResult();
            stoped = true;
            isConnected = false;
            ws.Close();
            var promise = new TaskCompletionSource<JObject>();
            closed?.Invoke("Normal");
            promise.SetResult(r);
            return promise.Task;
            
        }


        private string token_,  name_;
        private string host_ = "ws://127.0.0.1:39231";
        private int event_count = 0;
        private IDictionary<string, TaskCompletionSource<JObject>> cbs = new Dictionary<string, TaskCompletionSource<JObject>>();
        private bool isConnected = false;
        private bool delay = false;
        private bool stoped = false;


        public event NotifyInternal connected;
        public event NotifyInternal error;
        public event NotifyInternal closed;
        public event Notify events;
    }
}
