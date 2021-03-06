﻿using System.Collections.Concurrent;
using LunaClient.Base;
using LunaClient.Base.Interface;
using LunaClient.Systems.SettingsSys;
using LunaCommon.Enums;
using LunaCommon.Message.Data.Chat;
using LunaCommon.Message.Interface;
using LunaCommon.Message.Types;

namespace LunaClient.Systems.Chat
{
    public class ChatMessageHandler : SubSystem<ChatSystem>, IMessageHandler
    {
        public ConcurrentQueue<IMessageData> IncomingMessages { get; set; } = new ConcurrentQueue<IMessageData>();

        public void HandleMessage(IMessageData messageData)
        {
            var msgData = messageData as ChatBaseMsgData;
            if (msgData == null) return;

            switch (msgData.ChatMessageType)
            {
                case ChatMessageType.LIST_REPLY:
                {
                    var data = (ChatListReplyMsgData) messageData;
                    foreach (var keyVal in data.PlayerChannels)
                        foreach (var channelName in keyVal.Value)
                            System.Queuer.QueueChatJoin(keyVal.Key, channelName);

                    MainSystem.Singleton.NetworkState = ClientState.CHAT_SYNCED;
                }
                    break;
                case ChatMessageType.JOIN:
                {
                    var data = (ChatJoinMsgData) messageData;
                    System.Queuer.QueueChatJoin(data.From, data.Channel);
                }
                    break;
                case ChatMessageType.LEAVE:
                {
                    var data = (ChatLeaveMsgData) messageData;
                    System.Queuer.QueueChatLeave(data.From, data.Channel);
                }
                    break;
                case ChatMessageType.CHANNEL_MESSAGE:
                {
                    var data = (ChatChannelMsgData) messageData;
                    System.Queuer.QueueChannelMessage(data.From, data.Channel, data.Text);
                }
                    break;
                case ChatMessageType.PRIVATE_MESSAGE:
                {
                    var data = (ChatPrivateMsgData) messageData;
                    if ((data.To == SettingsSystem.CurrentSettings.PlayerName) ||
                        (data.From == SettingsSystem.CurrentSettings.PlayerName))
                        System.Queuer.QueuePrivateMessage(data.From, data.To, data.Text);
                }
                    break;
                case ChatMessageType.CONSOLE_MESSAGE:
                {
                    var data = (ChatConsoleMsgData) messageData;
                    System.Queuer.QueueSystemMessage(data.Message);
                }
                    break;
            }
        }
    }
}