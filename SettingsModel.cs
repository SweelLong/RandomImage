using PluginCore.Models;

namespace RandomImage
{
    public class SettingsModel : PluginSettingsModel
    {
        public string Key { get; set; }

        public uint AdminQQ { get; set; }

        public bool AtOperator { get; set; }

        public string AdditionalWords { get; set; }

        public string ImageAPI { get; set; }
    }
}