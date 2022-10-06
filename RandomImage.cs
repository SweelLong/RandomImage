using System;
using PluginCore.IPlugins;
using QQBotHub.Sdk.IPlugins;
using Konata.Core;
using Konata.Core.Events.Model;
using Konata.Core.Interfaces.Api;
using PluginCore;
using System.Net.Http;
using Konata.Core.Message;

namespace RandomImage
{
    public class RandomImage : BasePlugin, IQQBotPlugin
    {
        public override (bool IsSuccess, string Message) AfterEnable()
        {
            Console.WriteLine($"{nameof(RandomImage)}: {nameof(AfterEnable)}");
            return base.AfterEnable();
        }

        public override (bool IsSuccess, string Message) BeforeDisable()
        {
            Console.WriteLine($"{nameof(RandomImage)}: {nameof(BeforeDisable)}");
            return base.BeforeDisable();
        }

        public void OnGroupMessage((Bot s, GroupMessageEvent e) obj, string message, string groupName, uint groupUin, uint memberUin)
        {
            var settings = PluginSettingsModelFactory.Create<SettingsModel>(nameof(RandomImage));
            /***********************************************************************************/ // 随机图片模块
            MessageBuilder mb = new();
            if (message.ToLower().Contains("api") && !message.ToLower().StartsWith("newapi"))
            {
                obj.s.SendGroupMessage(groupUin, $"图片Api为：{settings.ImageAPI}\n输入【NewApi】关键词 + URL网址】\n如 NewApi https://sweellong.up.railway.app.php 即可更换\n\n这里有API大全哦：https://blog.csdn.net/likepoems/article/details/123924270");
            }
            if (message.ToLower().StartsWith("newapi") && memberUin == settings.AdminQQ)
            {
                string newApi = message[7..];
                settings.ImageAPI = newApi;
                obj.s.SendGroupMessage(groupUin, $"图片Api成功更换为：{newApi}");
            }
            if (message.ToLower().StartsWith("newapi") && memberUin != settings.AdminQQ)
            {
                obj.s.SendGroupMessage(groupUin, "图片Api更换失败！\n原因：你不是管理员");
            }
            if (message.Contains(settings.Key))
            {
                if (settings.AtOperator)
                {
                    mb.At(memberUin);
                }
                mb.Text(settings.AdditionalWords);
                mb.Image(new HttpClient().GetByteArrayAsync(settings.ImageAPI).Result);
                obj.s.SendGroupMessage(groupUin, mb);
            }
        }

        public void OnFriendMessage((Bot s, FriendMessageEvent e) obj, string message, uint friendUin)
        {
            var settings = PluginSettingsModelFactory.Create<SettingsModel>(nameof(RandomImage));
            /***********************************************************************************/ // 随机图片模块
            MessageBuilder mb = new();
            if (message.ToLower().Contains("api") && !message.ToLower().StartsWith("newapi") && friendUin == settings.AdminQQ)
            {
                obj.s.SendFriendMessage(friendUin, $"图片Api为：{settings.ImageAPI}\n输入【NewApi】关键词 + URL网址】\n如 NewApi https://sweellong.up.railway.app.php 即可更换\n\n这里有API大全哦：https://blog.csdn.net/likepoems/article/details/123924270");
            }
            if (message.ToLower().StartsWith("newapi") && friendUin == settings.AdminQQ)
            {
                string newApi = message[7..];
                settings.ImageAPI = newApi;
                obj.s.SendFriendMessage(friendUin, $"图片Api成功更换为：{newApi}");
            }
            if (message.ToLower().StartsWith("newapi") && friendUin != settings.AdminQQ)
            {
                obj.s.SendFriendMessage(friendUin, "图片Api更换失败！\n原因：你不是管理员");
            }
            if (message.Contains(settings.Key))
            {
                mb.Text(settings.AdditionalWords);
                mb.Image(new HttpClient().GetByteArrayAsync(settings.ImageAPI).Result);
                obj.s.SendFriendMessage(friendUin, mb);
            }
        }

        public void OnBotOnline((Bot s, BotOnlineEvent e) obj, string botName, uint botUin)
        {
        }

        public void OnBotOffline((Bot s, BotOfflineEvent e) obj, string botName, uint botUin)
        {
        }
    }
}