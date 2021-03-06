using Hints;
using InventorySystem.Items;
using RemoteAdmin;
using UnityEngine;

namespace Sapiox.API
{
    public class Player : MonoBehaviour
    {

        public void RemoveDisplayInfo(PlayerInfoArea playerInfo) => Hub.nicknameSync.Network_playerInfoToShow &= ~playerInfo;

        public void AddDisplayInfo(PlayerInfoArea playerInfo) => Hub.nicknameSync.Network_playerInfoToShow |= playerInfo;

        private ItemType _currentItem;

        public void Kick(string message) => ServerConsole.Disconnect(gameObject, message);
        public void Ban(int duration, string reason, string issuer = "Plugin") => PlayerManager.localPlayer.GetComponent<BanPlayer>().BanUser(gameObject, duration, reason, issuer);
        public void Broadcast(ushort time, string message) => GetComponent<global::Broadcast>().TargetAddElement(Hub.characterClassManager.connectionToClient, message, time, new global::Broadcast.BroadcastFlags());
        public void Hint(string message, float duration = 5f)
        {
            HintParameter[] parameters = new HintParameter[]
            {
                new StringHintParameter(""),
            };

            Hub.hints.Show(new TextHint(message, parameters, null, duration));
        }

        public bool IsFakePlayer => Server.FakePlayers.Contains(this);

        public Team Team => Hub.characterClassManager.CurRole.team;
        public Faction Faction => Hub.characterClassManager.Faction;
        public int Id => Hub.playerId;
        public bool GodMode
        {
            get => Hub.characterClassManager.GodMode;
            set => Hub.characterClassManager.GodMode = value;
        }
        public float Health
        {
            get => Hub.playerStats.Health;
            set => Hub.playerStats.Health = value;
        }
        public int MaxHealth
        {
            get => Hub.playerStats.maxHP;
            set => Hub.playerStats.maxHP = value;
        }
        public RoleType Role
        {
            get => Hub.characterClassManager.CurClass;
            set => Hub.characterClassManager.SetPlayersClass(value, gameObject, CharacterClassManager.SpawnReason.None);
        }
        public string UserId
        {
            get => Hub.characterClassManager.UserId;
            set => Hub.characterClassManager.UserId = value;
        }
        public PlayerMovementState MoveState
        {
            get => Hub.animationController.MoveState;
            set => Hub.animationController.UserCode_CmdChangeSpeedState((byte)value);
        }
        public bool IsHost => Hub.isDedicatedServer;
        public string IPAddress => Hub.networkIdentity.connectionToClient.address;
        public bool IsUsingRadio => Radio.UsingRadio;
        public Radio Radio => Hub.radio;

        public string RankName
        {
            get => Hub.serverRoles.Network_myText;
            set => Hub.serverRoles.SetText(value);
        }
        
        public string RankColor
        {
            get => Hub.serverRoles.Network_myColor;
            set => Hub.serverRoles.SetColor(value);
        }
        public Vector3 Position
        {
            get => Hub.playerMovementSync.GetRealPosition();
            set => Hub.playerMovementSync.OverridePosition(value, 0f);
        }
        public Vector2 Rotation
        {
            get => Hub.playerMovementSync.RotationSync;
            set => Hub.playerMovementSync.NetworkRotationSync = value;
        }

        public ItemType CurrentItem
        {
            get => _currentItem;
            set
            {
                Hub.inventory.NetworkCurItem = new ItemIdentifier(value, 0);
                _currentItem = value;
            }
        }
        public PlayerMovementSync MovementSync => Hub.playerMovementSync;

        public bool IsUsingVoiceChat => Radio.UsingVoiceChat;

        public string NickName => Hub.nicknameSync.Network_myNickSync;

           public string DisplayName
        {
            get => Hub.nicknameSync.Network_displayName;
            set => Hub.nicknameSync.Network_displayName = value;
        }
        
        public Transform Camera
        {
            get => Hub.PlayerCameraReference;
            set => Hub.PlayerCameraReference = value;
        }
        public void PlayFootStep(float volume, bool running) => Hub.footstepSync.PlayFootstepSound(volume, running);

        public ReferenceHub Hub => GetComponent<ReferenceHub>();
    }
}