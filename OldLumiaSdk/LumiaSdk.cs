﻿using System;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Lumia
{
    public delegate void NotifyInternal(string data);
    public delegate void Notify(JObject data);

    public class LumiaSdk
    {
        private WebSocket ws;

        public LumiaSdk(string token = null, string name = null, string host = null)
        {
            if (token != null) token_ = token;
            if (name != null) name_ = name;
            if (host != null) host_ = host;
        }

        public Task<bool> init(string token = null, string name = null, string host = null)
        {
            if (token != null) token_ = token;
            if (name != null) name_ = name;
            if (host != null) host_ = host;

            return StartWs();

        }

        private Task<bool> StartWs()
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
                    Task.Delay(2000).ContinueWith(async (t) => StartWs());
                }
                else if (e.Code == 1002 || e.Reason == "Invalid HTTP status.")
                {
                    stopped = true;
                }
                else
                {
                    StartWs();
                }
            };

            ws.Connect();
            return promise.Task;
        }

        private Task<JObject> SendWsMessage(JObject o)
        {
            var promise = new TaskCompletionSource<JObject>();
            cbs[(++event_count).ToString()] = promise;
            o["context"] = event_count.ToString();
            ws.Send(o.ToString(Newtonsoft.Json.Formatting.None));
            return promise.Task;
        }

        public Task<JObject> GetInfo()
        {
            JObject o = new JObject();
            o["method"] = "retrieve";
            o["retrieve"] = true;
            return SendWsMessage(o);
        }

        public Task<JObject> Send(ILumiaSdkSendPack pack)
        {

            JObject o = JObject.FromObject(pack);
            o["lsorigin"] = "lumia-sdk";
            return SendWsMessage(o);
        }

        public Task<JObject> SendAlert(LumiaSDKAlertValues alert)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.ALERT);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = LumiaUtils.getTypeValue<LumiaSDKAlertValues>(alert);
            return Send(pack);
        }

        // Sends command
        public Task<JObject> SendCommand(string command, bool default_ = false, bool skipQueue = false)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.CHAT_COMMAND);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = command;
            pack.params_.hold = default_;
            pack.params_.skipQueue = skipQueue;
            return Send(pack);
        }

        // Sends a color pack
        public Task<JObject> SendColor(
                RGB color,
                int brightness, // 0-100
                int duration,	 // In milliseconds
                int transition, // In milliseconds
                bool default_,
                bool skipQueue,
                List<ILumiaSdkLight> lights)
        {


            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.RGB_COLOR);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = color;
            pack.params_.brightness = brightness;
            pack.params_.duration = duration;
            pack.params_.transition = transition;
            pack.params_.hold = default_;
            pack.params_.skipQueue = skipQueue;
            pack.params_.lights = lights;
            return Send(pack);
        }

        // Sends brightness only
        public Task<JObject> SendBrightness(int brightness, int transition, bool skipQueue)
        {

            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.RGB_COLOR);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.brightness = brightness;
            pack.params_.transition = transition;
            pack.params_.skipQueue = skipQueue;
            return Send(pack);
        }

        // Sends tts
        public Task<JObject> SendTts(string text, int volume, string voice)
        {
            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.TTS);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = text;
            pack.params_.volume = volume;
            pack.params_.voice = voice;
            return Send(pack);
        }

        // Sends Chatbot message
        public Task<JObject> SendChatbot(Platforms platform, string text)
        {
            ILumiaSdkSendPack pack = new ILumiaSdkSendPack();
            pack.type = LumiaUtils.getTypeValue<LumiaSDKCommandTypes>(LumiaSDKCommandTypes.CHATBOT_MESSAGE);
            pack.params_ = new LumiaSDKPackParams();
            pack.params_.value = text;
            pack.params_.platform = LumiaUtils.getTypeValue<Platforms>(platform);
            return Send(pack);
        }


        public Task<JObject> Stop()
        {

            JObject o = new JObject();
            o["method"] = "Stop";
            var r = SendWsMessage(o).GetAwaiter().GetResult();
            stopped = true;
            isConnected = false;
            ws.Close();
            var promise = new TaskCompletionSource<JObject>();
            closed?.Invoke("Normal");
            promise.SetResult(r);
            return promise.Task;

        }


        private string token_, name_;
        private string host_ = "ws://127.0.0.1:39231";
        private int event_count = 0;
        private IDictionary<string, TaskCompletionSource<JObject>> cbs = new Dictionary<string, TaskCompletionSource<JObject>>();

        public bool stopped = false;
        public bool isConnected = false;

        public event NotifyInternal connected;
        public event NotifyInternal error;
        public event NotifyInternal closed;
        public event Notify events;
    }
}