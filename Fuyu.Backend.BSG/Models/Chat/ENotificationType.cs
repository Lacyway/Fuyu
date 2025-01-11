using System.Runtime.Serialization;

namespace Fuyu.Backend.BSG.Models.Chat;

[DataContract]
public enum ENotificationType
{
    [DataMember(Name = "ping")]
    Ping,
    [DataMember(Name = "channel_deleted")]
    ChannelDeleted,
    [DataMember(Name = "trader_supply")]
    TraderSupply,
    [DataMember(Name = "groupMatchInviteAccept")]
    GroupMatchInviteAccept,
    [DataMember(Name = "groupMatchInviteDecline")]
    GroupMatchInviteDecline,
    [DataMember(Name = "groupMatchWasRemoved")]
    GroupMatchWasRemoved,
    [DataMember(Name = "groupMatchInviteSend")]
    GroupMatchInviteSend,
    [DataMember(Name = "groupMatchInviteCancel")]
    GroupMatchInviteCancel,
    [DataMember(Name = "groupMatchInviteExpired")]
    GroupMatchInviteExpired,
    [DataMember(Name = "groupMatchLeaderChanged")]
    GroupMatchLeaderChanged,
    [DataMember(Name = "groupMatchUserLeave")]
    GroupMatchUserLeave,
    [DataMember(Name = "groupMaxCountReached")]
    GroupMaxCountReached,
    [DataMember(Name = "groupMatchStartGame")]
    GroupMatchStartGame,
    [DataMember(Name = "groupMatchRaidSettings")]
    GroupMatchRaidSettings,
    [DataMember(Name = "groupMatchRaidReady")]
    GroupMatchRaidReady,
    [DataMember(Name = "groupMatchRaidNotReady")]
    GroupMatchRaidNotReady,
    [DataMember(Name = "groupMatchAbort")]
    GroupMatchAbort,
    [DataMember(Name = "groupMatchUserHasBadVersion")]
    WrongMajorVersion,
    [DataMember(Name = "new_message")]
    ChatMessageReceived,
    [DataMember(Name = "youAreRemovedFromFriendList")]
    RemovedFromFriendsList,
    [DataMember(Name = "friendListNewRequest")]
    FriendsListNewRequest,
    [DataMember(Name = "friendListRequestCancel")]
    FriendsListRequestCanceled,
    [DataMember(Name = "tournamentWarning")]
    TournamentWarning,
    [DataMember(Name = "friendListRequestDecline")]
    FriendsListDecline,
    [DataMember(Name = "friendListRequestAccept")]
    FriendsListAccept,
    [DataMember(Name = "groupMatchYouWasKicked")]
    YouWasKickedFromDialogue,
    [DataMember(Name = "youAreAddToIgnoreList")]
    YouWereAddedToIgnoreList,
    [DataMember(Name = "youAreRemoveFromIgnoreList")]
    YouWereRemovedToIgnoreList,
    [DataMember(Name = "CustomizationUpdateRequired")]
    CustomizationUpdateRequired,
    RagfairOfferSold,
    RagfairRatingChange,
    RagfairNewRating,
    ForceLogout,
    InGameBan,
    InGameUnBan,
    Hideout,
    TraderStanding,
    ProfileLevel,
    SkillPoints,
    HideoutAreaLevel,
    AssortmentUnlockRule,
    ExamineItems,
    ExamineAllItems,
    TraderSalesSum,
    UnlockTrader,
    StashRows,
    ProfileLockTimer,
    MasteringSkill,
    ProfileExperienceDelta,
    TraderStandingDelta,
    TraderSalesSumDelta,
    SkillPointsDelta,
    MasteringSkillDelta,
    [DataMember(Name = "userMatched")]
    UserMatched,
    [DataMember(Name = "userMatchOver")]
    UserMatchOver,
    [DataMember(Name = "userConfirmed")]
    UserConfirmed
}