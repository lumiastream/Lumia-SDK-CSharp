﻿using System.Collections.Generic;

namespace Lumia
{
    class LumiaLookup
    {
        public static Dictionary<int, string> types_values = new Dictionary<int, string>() {
            {1, "alert"},
            {2, "midi"},
            {3, "osc"},
            {4, "artnet"},
            {5, "rgb-color"},
            {6, "hex-color"},
            {7, "chat-command"},
            {8, "twitch-points"},
            {9, "twitch-extension"},
            {10, "trovo-spells"},
            {11, "studio-scene"},
            {12, "studio-animation"},
            {13, "studio-theme"},
            {14, "chatbot-message"},
            {15, "tts"},

            {16, "twitch-follower"},
            {17, "twitch-subscriber"},
            {18, "twitch-bits"},
            {19, "twitch-host"},
            {20, "twitch-raid"},
            {21, "youtube-subscriber"},
            {22, "youtube-superchat"},
            {23, "youtube-supersticker"},
            {24, "youtube-member"},
            {25, "facebook-follower"},
            {26, "facebook-reaction"},
            {27, "facebook-star"},
            {28, "facebook-support"},
            {29, "facebook-share"},
            {30, "facebook-fan"},
            {31, "trovo-follower"},
            {32, "trovo-subscriber"},
            {33, "streamlabs-donation"},
            {34, "streamlabs-merch"},
            {35, "streamlabs-redemption"},
            {36, "streamlabs-primegift"},
            {37, "streamelements-donation"},
            {38, "obs-switchProfile"},
            {39, "obs-switchScene"},
            {40, "obs-switch-transition"},
            {41, "obs-streamStarting"},
            {42, "obs-streamStopping"},
            {43, "slobs-switchScene"},
            {44, "treatstream-treat"},
            {45, "pulse-heartrate"},
            {46, "pulse-calories"},

            {47, "states"},
            {48, "chat"},
            {49, "command"},
            {50, "twitch_point"},
            {51, "twitch_extension"},
            {52, "pulse"},
            {53, "trovo_spell"},
            {54, "alert"},

            {111, "twitch"},
            {112, "youtube"},
            {113, "facebook"},
            {114, "trovo"},
            {115, "glimesh"},

            {116, "hue"},
            {117, "nanoleaf"},
            {118, "nanoleaf2"},
            {119, "lifx"},
            {120, "tplink"},
            {121, "yeelight"},
            {122, "cololight"},
            {123, "tuya"},
            {124, "smartlife"},
            {125, "wyze"},
            {126, "wiz"},
            {127, "homeassistant"},
            {128, "govee"},
            {129, "wled"},
            {130, "magichome"},
            {131, "logitech"},
            {132, "razer"},
            {133, "corsair"},
            {134, "steelseries"},
            {135, "overlay"},
            {136, "elgato"},

            {137, "lumia"},
            {138, "twitch"},
            {139, "youtube"},
            {140, "facebook"},
            {141, "glimesh"},
            {142, "trovo"},
            {143, "streamlabs"},
            {144, "streamelements"},
            {145, "extralife"},
            {146, "donordrive"},
            {147, "tiltify"},
            {148, "patreon"},
            {149, "tipeeestream"},
            {150, "treatstream"},
            {151, "discord"},
            {152, "obs"},
            {153, "slobs"},
            {154, "pulsoid"},
            {155, "paypal"}

        };


        public static IDictionary<string, int> types_values_str_LumiaSDKCommandTypes = new Dictionary<string, int>() {
            {"alert", 1},
            {"midi", 2},
            {"osc", 3},
            {"artnet", 4},
            {"rgb-color", 5},
            {"hex-color", 6},
            {"chat-command", 7},
            {"twitch-points", 8},
            {"twitch-extension", 9},
            {"trovo-spells", 10},
            {"studio-scene", 11},
            {"studio-animation", 12},
            {"studio-theme", 13},
            {"chatbot-message", 14},
            {"tts", 15},

        };

        public static IDictionary<string, int> types_values_str_LumiaSDKAlertValues = new Dictionary<string, int>() {
            {"twitch-follower", 16},
            {"twitch-subscriber", 17},
            {"twitch-bits", 18},
            {"twitch-host", 19},
            {"twitch-raid", 20},
            {"youtube-subscriber", 21},
            {"youtube-superchat", 22},
            {"youtube-supersticker", 23},
            {"youtube-member", 24},
            {"facebook-follower", 25},
            {"facebook-reaction", 26},
            {"facebook-star", 27},
            {"facebook-support", 28},
            {"facebook-share", 29},
            {"facebook-fan", 30},
            {"trovo-follower", 31},
            {"trovo-subscriber", 32},
            {"streamlabs-donation", 33},
            {"streamlabs-merch", 34},
            {"streamlabs-redemption", 35},
            {"streamlabs-primegift", 36},
            {"streamelements-donation", 37},
            {"obs-switchProfile", 38},
            {"obs-switchScene", 39},
            {"obs-switch-transition", 40},
            {"obs-streamStarting", 41},
            {"obs-streamStopping", 42},
            {"slobs-switchScene", 43},
            {"treatstream-treat", 44},
            {"pulse-heartrate", 45},
            {"pulse-calories", 46},
        };

        public static IDictionary<string, int> types_values_str_LumiaSdkEventTypes = new Dictionary<string, int>() {
            {"states", 47},
            {"chat", 48},
            {"command", 49},
            {"twitch_point", 50},
            {"twitch_extension", 51},
            {"pulse", 52},
            {"trovo_spell", 53},
            {"alert", 54}
        };

        public static IDictionary<string, int> types_values_str_Platforms = new Dictionary<string, int>() {
            {"twitch", 111},
            {"youtube", 112},
            {"facebook", 113},
            {"trovo", 114},
            {"glimesh", 115},
        };

        public static IDictionary<string, int> types_values_str_LightBrands = new Dictionary<string, int>() {

            {"hue", 116},
            {"nanoleaf", 117},
            {"nanoleaf2", 118},
            {"lifx", 119},
            {"tplink", 120},
            {"yeelight", 121},
            {"cololight", 122},
            {"tuya", 123},
            {"smartlife", 124},
            {"wyze", 125},
            {"wiz", 126},
            {"homeassistant", 127},
            {"govee", 128},
            {"wled", 129},
            {"magichome", 130},
            {"logitech", 131},
            {"razer", 132},
            {"corsair", 133},
            {"steelseries", 134},
            {"overlay", 135},
            {"elgato", 136},
        };
        public static IDictionary<string, int> types_values_str_EventOrigins = new Dictionary<string, int>() {

            {"lumia", 137},
            {"twitch", 138},
            {"youtube", 139},
            {"facebook", 140},
            {"glimesh", 141},
            {"trovo", 142},
            {"streamlabs", 143},
            {"streamelements", 144},
            {"extralife", 145},
            {"donordrive", 146},
            {"tiltify", 147},
            {"patreon", 148},
            {"tipeeestream", 149},
            {"treatstream", 150},
            {"discord", 151},
            {"obs", 152},
            {"slobs", 153},
            {"pulsoid", 154},
            {"paypal", 155}
        };
    }
}