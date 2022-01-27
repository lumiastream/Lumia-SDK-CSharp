using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Lumia
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum  LumiaSDKCommandTypes 
    {
        [EnumMember(Value = "alert")]
        ALERT = 1,
        [EnumMember(Value = "midi")]
        MIDI = 2,
        [EnumMember(Value = "osc")]
        OSC = 3,
        [EnumMember(Value = "artnet")]
        ARTNET = 4,
        [EnumMember(Value = "rgb_color")]
        RGB_COLOR = 5,
        [EnumMember(Value = "hex_color")]
        HEX_COLOR = 6,
        [EnumMember(Value = "chat_command")]
        CHAT_COMMAND = 7,
        [EnumMember(Value = "twitch_points")]
        TWITCH_POINTS = 8,
        [EnumMember(Value = "twitch_extension")]
        TWITCH_EXTENSION = 9,
        [EnumMember(Value = "trovo_spells")]
        TROVO_SPELLS = 10,
        [EnumMember(Value = "studio_scene")]
        STUDIO_SCENE = 11,
        [EnumMember(Value = "studio_animation")]
        STUDIO_ANIMATION = 12,
        [EnumMember(Value = "studio_theme")]
        STUDIO_THEME = 13,
        [EnumMember(Value = "chatbot_message")]
        CHATBOT_MESSAGE = 14,
        [EnumMember(Value = "tts")]
        TTS = 15
    };

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LumiaSDKAlertValues
    {
        [EnumMember(Value = "twitch_follower")]
        TWITCH_FOLLOWER = 16,
        [EnumMember(Value = "twitch_subscriber")]
        TWITCH_SUBSCRIBER = 17,
        [EnumMember(Value = "twitch_bits")]
        TWITCH_BITS = 18,
        [EnumMember(Value = "twitch_host")]
        TWITCH_HOST = 19,
        [EnumMember(Value = "twitch_raid")]
        TWITCH_RAID = 20,
        [EnumMember(Value = "youtube_subscriber")]
        YOUTUBE_SUBSCRIBER = 21,
        [EnumMember(Value = "youtube_superchat")]
        YOUTUBE_SUPERCHAT = 22,
        [EnumMember(Value = "youtube_supersticker")]
        YOUTUBE_SUPERSTICKER = 23,
        [EnumMember(Value = "youtube_member")]
        YOUTUBE_MEMBER = 24,
        [EnumMember(Value = "facebook_follower")]
        FACEBOOK_FOLLOWER = 25,
        [EnumMember(Value = "facebook_reaction")]
        FACEBOOK_REACTION = 26,
        [EnumMember(Value = "facebook_star")]
        FACEBOOK_STAR = 27,
        [EnumMember(Value = "facebook_support")]
        FACEBOOK_SUPPORT = 28,
        [EnumMember(Value = "facebook_share")]
        FACEBOOK_SHARE = 29,
        [EnumMember(Value = "facebook_fan")]
        FACEBOOK_FAN = 30,
        [EnumMember(Value = "trovo_follower")]
        TROVO_FOLLOWER = 31,
        [EnumMember(Value = "trovo_subscriber")]
        TROVO_SUBSCRIBER = 32,
        [EnumMember(Value = "streamlabs_donation")]
        STREAMLABS_DONATION = 33,
        [EnumMember(Value = "streamlabs_merch")]
        STREAMLABS_MERCH = 34,
        [EnumMember(Value = "streamlabs_redemption")]
        STREAMLABS_REDEMPTION = 35,
        [EnumMember(Value = "streamlabs_primegift")]
        STREAMLABS_PRIMEGIFT = 36,
        [EnumMember(Value = "streamelements_donation")]
        STREAMELEMENTS_DONATION = 37,
        [EnumMember(Value = "obs_switchprofile")]
        OBS_SWITCHPROFILE = 38,
        [EnumMember(Value = "obs_switchscene")]
        OBS_SWITCHSCENE = 39,
        [EnumMember(Value = "obs_switch_transition")]
        OBS_SWITCH_transition = 40,
        [EnumMember(Value = "obs_streamstarting")]
        OBS_STREAMSTARTING = 41,
        [EnumMember(Value = "obs_streamstopping")]
        OBS_STREAMSTOPPING = 42,
        [EnumMember(Value = "slobs_switchscene")]
        SLOBS_SWITCHSCENE = 43,
        [EnumMember(Value = "treatstream_treat")]
        TREATSTREAM_TREAT = 44,
        [EnumMember(Value = "pulse_heartrate")]
        PULSE_HEARTRATE = 45,
        [EnumMember(Value = "pulse_calories")]
        PULSE_CALORIES = 46

    };
    [JsonConverter(typeof(StringEnumConverter))]
    public enum  LumiaSdkEventTypes
    {
        [EnumMember(Value = "states")]
        STATES = 47,
        [EnumMember(Value = "chat")]
        CHAT = 48,
        [EnumMember(Value = "command")]
        COMMAND = 49,
        [EnumMember(Value = "twitch_points")]
        TWITCH_POINTS = 50,
        [EnumMember(Value = "twitch_extesions")]
        TWITCH_EXTENSIONS = 51,
        [EnumMember(Value = "trovo_spell")]
        TROVO_SPELL = 52,
        [EnumMember(Value = "pulse")]
        PULSE = 53,
        [EnumMember(Value = "alert")]
        ALERT = 54,
    };
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Platforms
    {
        [EnumMember(Value = "twitch")]
        TWITCH = 111,
        [EnumMember(Value = "youtube")]
        YOUTUBE = 112,
        [EnumMember(Value = "facebook")]
        FACEBOOK = 113,
        [EnumMember(Value = "trovo")]
        TROVO = 114,
        [EnumMember(Value = "glimesh")]
        GLIMESH = 115
    };
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LightBrands
    {
        [EnumMember(Value = "hue")]
        HUE = 116,
        [EnumMember(Value = "nanoleaf")]
        NANOLEAF = 117,
        [EnumMember(Value = "nanoleaf2")]
        NANOLEAF2 = 118,
        [EnumMember(Value = "lifx")]
        LIFX = 119,
        [EnumMember(Value = "tplink")]
        TPLINK = 120,
        [EnumMember(Value = "yeelight")]
        YEELIGHT = 121,
        [EnumMember(Value = "cololight")]
        COLOLIGHT = 122,
        [EnumMember(Value = "tuya")]
        TUYA = 123,
        [EnumMember(Value = "smartlife")]
        SMARTLIFE = 124,
        [EnumMember(Value = "wyze")]
        WYZE = 125,
        [EnumMember(Value = "wiz")]
        WIZ = 126,
        [EnumMember(Value = "homeassistant")]
        HOMEASSISTANT = 127,
        [EnumMember(Value = "govee")]
        GOVEE = 128,
        [EnumMember(Value = "wled")]
        WLED = 129,
        [EnumMember(Value = "magichome")]
        MAGICHOME = 130,
        [EnumMember(Value = "logitech")]
        LOGITECH = 131,
        [EnumMember(Value = "razer")]
        RAZER = 132,
        [EnumMember(Value = "corsair")]
        CORSAIR = 133,
        [EnumMember(Value = "steelseries")]
        STEELSERIES = 134,
        [EnumMember(Value = "overlay")]
        OVERLAY = 135,
        [EnumMember(Value = "elgato")]
        ELGATO = 136

    };

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventOrigins
    {
        [EnumMember(Value = "lumia")]
        LUMIA = 137,
        [EnumMember(Value = "twitch")]
        TWITCH = 138,
        [EnumMember(Value = "youtube")]
        YOUTUBE = 139,
        [EnumMember(Value = "facebook")]
        FACEBOOK = 140,
        [EnumMember(Value = "glimesh")]
        GLIMESH = 141,
        [EnumMember(Value = "trovo")]
        TROVO = 142,
        [EnumMember(Value = "streamlabs")]
        STREAMLABS = 143,
        [EnumMember(Value = "streamelements")]
        STREAMELEMENTS = 144,
        [EnumMember(Value = "extralife")]
        EXTRALIFE = 145,
        [EnumMember(Value = "donordrive")]
        DONORDRIVE = 146,
        [EnumMember(Value = "tiltify")]
        TILTIFY = 147,
        [EnumMember(Value = "patreon")]
        PATREON = 148,
        [EnumMember(Value = "tipeeestream")]
        TIPEEESTREAM = 149,
        [EnumMember(Value = "treatstream")]
        TREATSTREAM = 150,
        [EnumMember(Value = "discord")]
        DISCORD = 151,
        [EnumMember(Value = "obs")]
        OBS = 152,
        [EnumMember(Value = "slobs")]
        SLOBS = 153,
        [EnumMember(Value = "pulsoid")]
        PULSOID = 154,
        [EnumMember(Value = "paypal")]
        PAYPAL = 155
    };


    public class RGB
    {
        public int r;
        public int g;
        public int b;
    };

    public class ExtraSetting
    {
        public string username;
        public int bits;
    };

    public class ILumiaSdkEventData
    {
        public string username;
        public string command;
    };

    public class ILumiaSdkEvent
    {
        public EventOrigins origin;

        public LumiaSdkEventTypes type;

        public ILumiaSdkEventData data;
    };

    public class ILumiaSdkLight
    {
       
        public LightBrands type;

        
        public dynamic id;
    };

    public class LumiaSDKPackParams
    {
        
        public dynamic value;

        [JsonProperty("lights", NullValueHandling = NullValueHandling.Ignore)]
        public List<ILumiaSdkLight>? lights;

        [JsonProperty("hold", NullValueHandling = NullValueHandling.Ignore)]
        public bool? hold;      // Sets this command to default or not
        [JsonProperty("skipQueue", NullValueHandling = NullValueHandling.Ignore)]
        public bool? skipQueue; // Skips the queue and instantly turns to this color

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public Platforms? platform;

        // Used for TTS
        [JsonProperty("voice", NullValueHandling = NullValueHandling.Ignore)]
        public string? voice;
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public int? volume;

        // Mainly used for RGB color and Hex color types
        [JsonProperty("brightness", NullValueHandling = NullValueHandling.Ignore)]
        public int? brightness;
        [JsonProperty("transition", NullValueHandling = NullValueHandling.Ignore)]
        public int? transition;
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public int? duration;

        [JsonProperty("extraSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExtraSetting? extraSettings; // Mainly used to pass in variables for things like TTS or Chat bot
    };


    
    public class ILumiaSdkSendPack
    {
        public LumiaSDKCommandTypes type;

        [JsonProperty("params")]
        public LumiaSDKPackParams params_;
    };


    public class LumiaUtils
    {


        public static string getTypeValue<T>(T value)
        {
            return LumiaLookup.types_values[(int)(object) value];
        }

        public static T getTypeValueFromString<T>(string value_type, string value)
        {
            if (value_type == "LumiaSDKCommandTypes")
            {
                return (T)(object)LumiaLookup.types_values_str_LumiaSDKCommandTypes[value];
            }
            else if (value_type == "LumiaSDKAlertValues")
            {
                return (T)(object)LumiaLookup.types_values_str_LumiaSDKAlertValues[value];
            }
            else if (value_type == "LumiaSdkEventTypes")
            {
                return (T)(object)LumiaLookup.types_values_str_LumiaSdkEventTypes[value];
            }
            else if (value_type == "Platforms")
            {
                return (T)(object)LumiaLookup.types_values_str_Platforms[value];
            }
            else if (value_type == "LightBrands")
            {
                return (T)(object)LumiaLookup.types_values_str_LightBrands[value];
            }
            else if (value_type == "EventOrigins")
            {
                return (T)(object)LumiaLookup.types_values_str_EventOrigins[value];
            }else
            {
                throw new Exception("Invalid vlaue");
            }

        }



}

}