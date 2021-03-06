using System.Collections.Generic;
using LunaClient.Base;
using LunaClient.Systems.SettingsSys;

namespace LunaClient.Systems.Admin
{
    public class AdminSystem : MessageSystem<AdminSystem, AdminMessageSender, AdminMessageHandler>
    {
        #region Fields

        private List<string> ServerAdmins { get; } = new List<string>();

        #endregion

        #region Base overrides

        public override void OnDisabled()
        {
            base.OnDisabled();
            ServerAdmins.Clear();
        }

        #endregion

        #region Public methods

        public bool IsCurrentPlayerAdmin()
        {
            return IsAdmin(SettingsSystem.CurrentSettings.PlayerName);
        }

        public bool IsAdmin(string playerName)
        {
            return ServerAdmins.Contains(playerName);
        }

        public void RegisterServerAdmin(string adminName)
        {
            if (!ServerAdmins.Contains(adminName))
                ServerAdmins.Add(adminName);
        }

        public void UnregisterServerAdmin(string adminName)
        {
            if (ServerAdmins.Contains(adminName))
                ServerAdmins.Remove(adminName);
        }

        #endregion
    }
}