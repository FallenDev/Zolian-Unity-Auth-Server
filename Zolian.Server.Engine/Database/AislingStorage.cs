using Dapper;

using Zolian.Enums;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;
using Zolian.Network.Server;
using Zolian.Common.Identity;
using Zolian.Sprites.Entities;

namespace Zolian.Database;

public record AislingStorage : Sql, IEqualityOperators<AislingStorage, AislingStorage, bool>
{
    public const string ConnectionString = "Data Source=.;Initial Catalog=ZolianMMOPlayers;Integrated Security=True;Encrypt=False;MultipleActiveResultSets=True;";
    private const string EncryptedConnectionString = "Data Source=.;Initial Catalog=ZolianMMOPlayers;Integrated Security=True;Column Encryption Setting=enabled;TrustServerCertificate=True;MultipleActiveResultSets=True;";

    #region LoginServer Operations

    /// <summary>
    /// Creation of a new player from the LoginServer
    /// </summary>
    public async Task Create(Player obj)
    {
        try
        {
            var serial = Guid.NewGuid();
            var serialFound = await CheckIfSerialExists(serial);
            if (serialFound)
                serial = Guid.NewGuid();

            var connection = ConnectToDatabase(ConnectionString);
            var cmd = ConnectToDatabaseSqlCommandWithProcedure("PlayerCreation", connection);

            #region Parameters

            cmd.Parameters.Add("@Serial", SqlDbType.UniqueIdentifier).Value = serial;
            cmd.Parameters.Add("@SteamId", SqlDbType.Decimal).Value = obj.SteamId;
            cmd.Parameters["@SteamId"].Precision = 20;
            cmd.Parameters["@SteamId"].Scale = 0;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = obj.UserName;
            cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@LastLogged", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@BaseHp", SqlDbType.Decimal).Value = obj.BaseHp;
            cmd.Parameters["@BaseHp"].Precision = 10;
            cmd.Parameters["@BaseHp"].Scale = 0;
            cmd.Parameters.Add("@CurrentHp", SqlDbType.Decimal).Value = obj.CurrentHp;
            cmd.Parameters["@CurrentHp"].Precision = 10;
            cmd.Parameters["@CurrentHp"].Scale = 0;
            cmd.Parameters.Add("@BaseMp", SqlDbType.Decimal).Value = obj.BaseMp;
            cmd.Parameters["@BaseMp"].Precision = 10;
            cmd.Parameters["@BaseMp"].Scale = 0;
            cmd.Parameters.Add("@CurrentMp", SqlDbType.Decimal).Value = obj.CurrentMp;
            cmd.Parameters["@CurrentMp"].Precision = 10;
            cmd.Parameters["@CurrentMp"].Scale = 0;
            cmd.Parameters.Add("@BaseStamina", SqlDbType.Decimal).Value = obj.BaseStamina;
            cmd.Parameters["@BaseStamina"].Precision = 10;
            cmd.Parameters["@BaseStamina"].Scale = 0;
            cmd.Parameters.Add("@CurrentStamina", SqlDbType.Decimal).Value = obj.CurrentStamina;
            cmd.Parameters["@CurrentStamina"].Precision = 10;
            cmd.Parameters["@CurrentStamina"].Scale = 0;
            cmd.Parameters.Add("@BaseRage", SqlDbType.Decimal).Value = obj.BaseRage;
            cmd.Parameters["@BaseRage"].Precision = 10;
            cmd.Parameters["@BaseRage"].Scale = 0;
            cmd.Parameters.Add("@CurrentRage", SqlDbType.Decimal).Value = obj.CurrentRage;
            cmd.Parameters["@CurrentRage"].Precision = 10;
            cmd.Parameters["@CurrentRage"].Scale = 0;
            cmd.Parameters.Add("@BaseRegen", SqlDbType.Decimal).Value = obj.BaseRegen;
            cmd.Parameters["@BaseRegen"].Precision = 10;
            cmd.Parameters["@BaseRegen"].Scale = 0;
            cmd.Parameters.Add("@BaseDmg", SqlDbType.Decimal).Value = obj.BaseDmg;
            cmd.Parameters["@BaseDmg"].Precision = 10;
            cmd.Parameters["@BaseDmg"].Scale = 0;
            cmd.Parameters.Add("@BaseHit", SqlDbType.Decimal).Value = 5;
            cmd.Parameters["@BaseHit"].Precision = 10;
            cmd.Parameters["@BaseHit"].Scale = 0;
            cmd.Parameters.Add("@BaseFortitude", SqlDbType.Decimal).Value = 5;
            cmd.Parameters["@BaseFortitude"].Precision = 10;
            cmd.Parameters["@BaseFortitude"].Scale = 0;
            cmd.Parameters.Add("@BaseMagicResist", SqlDbType.Decimal).Value = 5;
            cmd.Parameters["@BaseMagicResist"].Precision = 10;
            cmd.Parameters["@BaseMagicResist"].Scale = 0;
            cmd.Parameters.Add("@BaseStr", SqlDbType.Int).Value = obj.BaseStr;
            cmd.Parameters.Add("@BaseInt", SqlDbType.Int).Value = obj.BaseInt;
            cmd.Parameters.Add("@BaseWis", SqlDbType.Int).Value = obj.BaseWis;
            cmd.Parameters.Add("@BaseCon", SqlDbType.Int).Value = obj.BaseCon;
            cmd.Parameters.Add("@BaseDex", SqlDbType.Int).Value = obj.BaseDex;
            cmd.Parameters.Add("@BaseLuck", SqlDbType.Int).Value = obj.BaseLuck;
            cmd.Parameters.Add("@FirstClass", SqlDbType.VarChar).Value = obj.FirstClass;
            cmd.Parameters.Add("@EntityLevel", SqlDbType.Decimal).Value = 1;
            cmd.Parameters["@EntityLevel"].Precision = 10;
            cmd.Parameters["@EntityLevel"].Scale = 0;
            cmd.Parameters.Add("@JobLevel", SqlDbType.Decimal).Value = 0;
            cmd.Parameters["@JobLevel"].Precision = 10;
            cmd.Parameters["@JobLevel"].Scale = 0;

            var cmd2 = ConnectToDatabaseSqlCommandWithProcedure("PlayerLooksCreation", connection);
            cmd2.Parameters.Add("@Serial", SqlDbType.UniqueIdentifier).Value = serial;
            cmd2.Parameters.Add("@SteamId", SqlDbType.Decimal).Value = obj.SteamId;
            cmd2.Parameters["@SteamId"].Precision = 20;
            cmd2.Parameters["@SteamId"].Scale = 0;
            cmd2.Parameters.Add("UserName", SqlDbType.VarChar).Value = obj.UserName;
            cmd2.Parameters.Add("@Race", SqlDbType.VarChar).Value = obj.Race;
            cmd2.Parameters.Add("@Gender", SqlDbType.VarChar).Value = obj.Gender.ToString();
            cmd2.Parameters.Add("@Hair", SqlDbType.SmallInt).Value = obj.Hair;
            cmd2.Parameters.Add("@HairColor", SqlDbType.SmallInt).Value = obj.HairColor;
            cmd2.Parameters.Add("@HairHighlightColor", SqlDbType.SmallInt).Value = obj.HairHighlightColor;
            cmd2.Parameters.Add("@SkinColor", SqlDbType.SmallInt).Value = obj.SkinColor;
            cmd2.Parameters.Add("@EyeColor", SqlDbType.SmallInt).Value = obj.EyeColor;
            cmd2.Parameters.Add("@Beard", SqlDbType.SmallInt).Value = obj.Beard;
            cmd2.Parameters.Add("@Mustache", SqlDbType.SmallInt).Value = obj.Mustache;
            cmd2.Parameters.Add("@Bangs", SqlDbType.SmallInt).Value = obj.Bangs;
            cmd2.ExecuteNonQuery();

            #endregion

            ExecuteAndCloseConnection(cmd, connection);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
    }

    /// <summary>
    /// Loads a player's account from the LoginServer
    /// </summary>
    public static async Task<List<Player>> LoadAccount(long steamId)
    {
        var account = new List<Player>();

        try
        {
            var sConn = ConnectToDatabase(ConnectionString);
            var values = new { SteamId = steamId };
            var records = await sConn.QueryAsync<Player>("[SelectAccount]", values, commandType: CommandType.StoredProcedure);
            account = records.AsList();
            sConn.Close();
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }

        return account;
    }

    /// <summary>
    /// Loads a player's data from the LoginServer
    /// </summary>
    public static async Task<Player> LoadPlayer(Guid serial, long steamId, string userName)
    {
        var player = new Player();

        try
        {
            var sConn = ConnectToDatabase(ConnectionString);
            var values = new { Serial = serial, SteamId = steamId, UserName = userName };
            player = await sConn.QueryFirstAsync<Player>("[SelectPlayer]", values, commandType: CommandType.StoredProcedure);
            sConn.Close();
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }

        return player;
    }

    #endregion

    #region Player Save Methods

    /// <summary>
    /// Saves a player's state on disconnect or error
    /// Utilizes an active connection that self-heals if closed
    /// </summary>
    public static async Task<bool> Save(Player obj)
    {
        if (obj == null) return false;
        var connection = ServerSetup.Instance.ServerSaveConnection;

        try
        {
            _ = PlayerSaveRoutine(obj, connection);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
        finally
        {
            if (connection.State != ConnectionState.Open)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Reconnecting Player Save-State");
                ServerSetup.Instance.ServerSaveConnection = new SqlConnection(ConnectionString);
                ServerSetup.Instance.ServerSaveConnection.Open();
            }
        }

        return true;
    }

    private static Task PlayerSaveRoutine(Player player, SqlConnection connection)
    {
        if (player.Client == null) return Task.CompletedTask;
        //player.Client.LastSave = DateTime.UtcNow;
        var dt = PlayerDataTable();
        //var qDt = QuestDataTable();
        var cDt = ComboScrollDataTable();
        var iDt = ItemsDataTable();
        var skillDt = SkillDataTable();
        var spellDt = SpellDataTable();
        var buffDt = BuffsDataTable();
        var debuffDt = DeBuffsDataTable();
        dt = PlayerStatSave(player, dt);
        //qDt = PlayerQuestSave(player, qDt);
        //cDt = PlayerComboSave(player, cDt);
        //iDt = PlayerItemSave(player, iDt);
        //skillDt = PlayerSkillSave(player, skillDt);
        //spellDt = PlayerSpellSave(player, spellDt);
        //buffDt = PlayerBuffSave(player, buffDt);
        //debuffDt = PlayerDebuffSave(player, debuffDt);

        using (var cmd = new SqlCommand("PlayerSave", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            var param = cmd.Parameters.AddWithValue("@Players", dt);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.PlayerType";
            cmd.ExecuteNonQuery();
        }

        //using (var cmd2 = new SqlCommand("PlayerQuestSave", connection))
        //{
        //    cmd2.CommandType = CommandType.StoredProcedure;
        //    var param2 = cmd2.Parameters.AddWithValue("@Quests", qDt);
        //    param2.SqlDbType = SqlDbType.Structured;
        //    param2.TypeName = "dbo.QuestType";
        //    cmd2.ExecuteNonQuery();
        //}

        //using (var cmd3 = new SqlCommand("PlayerComboSave", connection))
        //{
        //    cmd3.CommandType = CommandType.StoredProcedure;
        //    var param3 = cmd3.Parameters.AddWithValue("@Combos", cDt);
        //    param3.SqlDbType = SqlDbType.Structured;
        //    param3.TypeName = "dbo.ComboType";
        //    cmd3.ExecuteNonQuery();
        //}

        //using (var cmd4 = new SqlCommand("ItemUpsert", connection))
        //{
        //    cmd4.CommandType = CommandType.StoredProcedure;
        //    var param4 = cmd4.Parameters.AddWithValue("@Items", iDt);
        //    param4.SqlDbType = SqlDbType.Structured;
        //    param4.TypeName = "dbo.ItemType";
        //    cmd4.ExecuteNonQuery();
        //}

        //using (var cmd5 = new SqlCommand("PlayerSaveSkills", connection))
        //{
        //    cmd5.CommandType = CommandType.StoredProcedure;
        //    var param5 = cmd5.Parameters.AddWithValue("@Skills", skillDt);
        //    param5.SqlDbType = SqlDbType.Structured;
        //    param5.TypeName = "dbo.SkillType";
        //    cmd5.ExecuteNonQuery();
        //}

        //using (var cmd6 = new SqlCommand("PlayerSaveSpells", connection))
        //{
        //    cmd6.CommandType = CommandType.StoredProcedure;
        //    var param6 = cmd6.Parameters.AddWithValue("@Spells", spellDt);
        //    param6.SqlDbType = SqlDbType.Structured;
        //    param6.TypeName = "dbo.SpellType";
        //    cmd6.ExecuteNonQuery();
        //}

        //using (var cmd7 = new SqlCommand("BuffSave", connection))
        //{
        //    cmd7.CommandType = CommandType.StoredProcedure;
        //    var param7 = cmd7.Parameters.AddWithValue("@Buffs", buffDt);
        //    param7.SqlDbType = SqlDbType.Structured;
        //    param7.TypeName = "dbo.BuffType";
        //    cmd7.ExecuteNonQuery();
        //}

        //using (var cmd8 = new SqlCommand("DeBuffSave", connection))
        //{
        //    cmd8.CommandType = CommandType.StoredProcedure;
        //    var param8 = cmd8.Parameters.AddWithValue("@Debuffs", debuffDt);
        //    param8.SqlDbType = SqlDbType.Structured;
        //    param8.TypeName = "dbo.DebuffType";
        //    cmd8.ExecuteNonQuery();
        //}

        return Task.CompletedTask;
    }

    private static DataTable PlayerStatSave(Player obj, DataTable dt)
    {
        dt.Rows.Add(obj.Serial);

        return dt;
    }

    private static DataTable PlayerQuestSave(Player obj, DataTable qDt)
    {
        qDt.Rows.Add(obj.Serial);

        return qDt;
    }

    #endregion

    private static async Task<bool> CheckIfSerialExists(Guid serial)
    {
        try
        {
            var sConn = ConnectToDatabase(ConnectionString);
            var cmd = ConnectToDatabaseSqlCommandWithProcedure("CheckIfPlayerSerialExists", sConn);
            cmd.Parameters.Add("@Serial", SqlDbType.UniqueIdentifier).Value = serial;
            var reader = await cmd.ExecuteReaderAsync();
            var userFound = false;

            while (reader.Read())
            {
                var userName = reader["Username"].ToString();
                if (userName is null) continue;
                userFound = true;
            }

            reader.Close();
            sConn.Close();
            return userFound;
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }

        return false;
    }

    #region Data Tables

    private static DataTable PlayerDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Serial", typeof(long));
        dt.Columns.Add("Created", typeof(DateTime));
        dt.Columns.Add("Username", typeof(string));
        dt.Columns.Add("LoggedIn", typeof(bool));
        dt.Columns.Add("LastLogged", typeof(DateTime));
        dt.Columns.Add("X", typeof(byte));
        dt.Columns.Add("Y", typeof(byte));
        dt.Columns.Add("CurrentMapId", typeof(int));
        dt.Columns.Add("Direction", typeof(byte));
        dt.Columns.Add("CurrentHp", typeof(int));
        dt.Columns.Add("BaseHp", typeof(int));
        dt.Columns.Add("CurrentMp", typeof(int));
        dt.Columns.Add("BaseMp", typeof(int));
        dt.Columns.Add("_ac", typeof(short));
        dt.Columns.Add("_Regen", typeof(short));
        dt.Columns.Add("_Dmg", typeof(short));
        dt.Columns.Add("_Hit", typeof(short));
        dt.Columns.Add("_Mr", typeof(short));
        dt.Columns.Add("_Str", typeof(short));
        dt.Columns.Add("_Int", typeof(short));
        dt.Columns.Add("_Wis", typeof(short));
        dt.Columns.Add("_Con", typeof(short));
        dt.Columns.Add("_Dex", typeof(short));
        dt.Columns.Add("_Luck", typeof(short));
        dt.Columns.Add("AbpLevel", typeof(int));
        dt.Columns.Add("AbpNext", typeof(int));
        dt.Columns.Add("AbpTotal", typeof(long));
        dt.Columns.Add("ExpLevel", typeof(int));
        dt.Columns.Add("ExpNext", typeof(long));
        dt.Columns.Add("ExpTotal", typeof(long));
        dt.Columns.Add("Stage", typeof(string));
        dt.Columns.Add("JobClass", typeof(string));
        dt.Columns.Add("Path", typeof(string));
        dt.Columns.Add("PastClass", typeof(string));
        dt.Columns.Add("Race", typeof(string));
        dt.Columns.Add("Afflictions", typeof(string));
        dt.Columns.Add("Gender", typeof(string));
        dt.Columns.Add("HairColor", typeof(byte));
        dt.Columns.Add("HairStyle", typeof(byte));
        dt.Columns.Add("NameColor", typeof(byte));
        dt.Columns.Add("Nation", typeof(string));
        dt.Columns.Add("Clan", typeof(string));
        dt.Columns.Add("ClanRank", typeof(string));
        dt.Columns.Add("ClanTitle", typeof(string));
        dt.Columns.Add("MonsterForm", typeof(short));
        dt.Columns.Add("ActiveStatus", typeof(string));
        dt.Columns.Add("Flags", typeof(string));
        dt.Columns.Add("CurrentWeight", typeof(short));
        dt.Columns.Add("World", typeof(byte));
        dt.Columns.Add("Lantern", typeof(byte));
        dt.Columns.Add("Invisible", typeof(bool));
        dt.Columns.Add("Resting", typeof(string));
        dt.Columns.Add("FireImmunity", typeof(bool));
        dt.Columns.Add("WaterImmunity", typeof(bool));
        dt.Columns.Add("WindImmunity", typeof(bool));
        dt.Columns.Add("EarthImmunity", typeof(bool));
        dt.Columns.Add("LightImmunity", typeof(bool));
        dt.Columns.Add("DarkImmunity", typeof(bool));
        dt.Columns.Add("PoisonImmunity", typeof(bool));
        dt.Columns.Add("EnticeImmunity", typeof(bool));
        dt.Columns.Add("PartyStatus", typeof(string));
        dt.Columns.Add("RaceSkill", typeof(string));
        dt.Columns.Add("RaceSpell", typeof(string));
        dt.Columns.Add("GameMaster", typeof(bool));
        dt.Columns.Add("ArenaHost", typeof(bool));
        dt.Columns.Add("Knight", typeof(bool));
        dt.Columns.Add("GoldPoints", typeof(long));
        dt.Columns.Add("StatPoints", typeof(short));
        dt.Columns.Add("GamePoints", typeof(long));
        dt.Columns.Add("BankedGold", typeof(long));
        dt.Columns.Add("ArmorImg", typeof(short));
        dt.Columns.Add("HelmetImg", typeof(short));
        dt.Columns.Add("ShieldImg", typeof(short));
        dt.Columns.Add("WeaponImg", typeof(short));
        dt.Columns.Add("BootsImg", typeof(short));
        dt.Columns.Add("HeadAccessoryImg", typeof(short));
        dt.Columns.Add("Accessory1Img", typeof(short));
        dt.Columns.Add("Accessory2Img", typeof(short));
        dt.Columns.Add("Accessory3Img", typeof(short));
        dt.Columns.Add("Accessory1Color", typeof(byte));
        dt.Columns.Add("Accessory2Color", typeof(byte));
        dt.Columns.Add("Accessory3Color", typeof(byte));
        dt.Columns.Add("BodyColor", typeof(byte));
        dt.Columns.Add("BodySprite", typeof(byte));
        dt.Columns.Add("FaceSprite", typeof(byte));
        dt.Columns.Add("OverCoatImg", typeof(short));
        dt.Columns.Add("BootColor", typeof(byte));
        dt.Columns.Add("OverCoatColor", typeof(byte));
        dt.Columns.Add("Pants", typeof(byte));
        return dt;
    }

    private static DataTable ItemsDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("ItemId", typeof(long));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Serial", typeof(long)); // Owner's Serial
        dt.Columns.Add("ItemPane", typeof(string));
        dt.Columns.Add("Slot", typeof(int));
        dt.Columns.Add("InventorySlot", typeof(int));
        dt.Columns.Add("Color", typeof(int));
        dt.Columns.Add("Cursed", typeof(bool));
        dt.Columns.Add("Durability", typeof(long));
        dt.Columns.Add("Identified", typeof(bool));
        dt.Columns.Add("ItemVariance", typeof(string));
        dt.Columns.Add("WeapVariance", typeof(string));
        dt.Columns.Add("ItemQuality", typeof(string));
        dt.Columns.Add("OriginalQuality", typeof(string));
        dt.Columns.Add("Stacks", typeof(int));
        dt.Columns.Add("Enchantable", typeof(bool));
        dt.Columns.Add("Tarnished", typeof(bool));
        dt.Columns.Add("GearEnhancement", typeof(string));
        dt.Columns.Add("ItemMaterial", typeof(string));
        dt.Columns.Add("GiftWrapped", typeof(string));
        return dt;
    }

    private static DataTable PostDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("BoardId", typeof(int));
        dt.Columns.Add("PostId", typeof(int));
        dt.Columns.Add("Highlighted", typeof(bool));
        dt.Columns.Add("DatePosted", typeof(DateTime));
        dt.Columns.Add("Owner", typeof(string));
        dt.Columns.Add("Sender", typeof(string));
        dt.Columns.Add("ReadPost", typeof(bool));
        dt.Columns.Add("SubjectLine", typeof(string));
        dt.Columns.Add("Message", typeof(string));
        return dt;
    }

    private static DataTable SkillDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Serial", typeof(long));
        dt.Columns.Add("Level", typeof(int));
        dt.Columns.Add("Slot", typeof(int));
        dt.Columns.Add("Skill", typeof(string));
        dt.Columns.Add("Uses", typeof(int));
        dt.Columns.Add("Cooldown", typeof(int));
        return dt;
    }

    private static DataTable SpellDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Serial", typeof(long));
        dt.Columns.Add("Level", typeof(int));
        dt.Columns.Add("Slot", typeof(int));
        dt.Columns.Add("Spell", typeof(string));
        dt.Columns.Add("Casts", typeof(int));
        dt.Columns.Add("Cooldown", typeof(int));
        return dt;
    }

    private static DataTable QuestDataTable()
    {
        var qDt = new DataTable();
        qDt.Columns.Add("Serial", typeof(long));
        qDt.Columns.Add("MailBoxNumber", typeof(int));
        qDt.Columns.Add("TutorialCompleted", typeof(bool));
        qDt.Columns.Add("BetaReset", typeof(bool));
        qDt.Columns.Add("ArtursGift", typeof(int));
        qDt.Columns.Add("CamilleGreetingComplete", typeof(bool));
        qDt.Columns.Add("ConnPotions", typeof(bool));
        qDt.Columns.Add("CryptTerror", typeof(bool));
        qDt.Columns.Add("CryptTerrorSlayed", typeof(bool));
        qDt.Columns.Add("CryptTerrorContinued", typeof(bool));
        qDt.Columns.Add("CryptTerrorContSlayed", typeof(bool));
        qDt.Columns.Add("NightTerror", typeof(bool));
        qDt.Columns.Add("NightTerrorSlayed", typeof(bool));
        qDt.Columns.Add("DreamWalking", typeof(bool));
        qDt.Columns.Add("DreamWalkingSlayed", typeof(bool));
        qDt.Columns.Add("Dar", typeof(int));
        qDt.Columns.Add("DarItem", typeof(string));
        qDt.Columns.Add("ReleasedTodesbaum", typeof(bool));
        qDt.Columns.Add("DrunkenHabit", typeof(bool));
        qDt.Columns.Add("FionaDance", typeof(bool));
        qDt.Columns.Add("Keela", typeof(int));
        qDt.Columns.Add("KeelaCount", typeof(int));
        qDt.Columns.Add("KeelaKill", typeof(string));
        qDt.Columns.Add("KeelaQuesting", typeof(bool));
        qDt.Columns.Add("KillerBee", typeof(bool));
        qDt.Columns.Add("Neal", typeof(int));
        qDt.Columns.Add("NealCount", typeof(int));
        qDt.Columns.Add("NealKill", typeof(string));
        qDt.Columns.Add("AbelShopAccess", typeof(bool));
        qDt.Columns.Add("PeteKill", typeof(int));
        qDt.Columns.Add("PeteComplete", typeof(bool));
        qDt.Columns.Add("SwampAccess", typeof(bool));
        qDt.Columns.Add("SwampCount", typeof(int));
        qDt.Columns.Add("TagorDungeonAccess", typeof(bool));
        qDt.Columns.Add("Lau", typeof(int));
        qDt.Columns.Add("BeltDegree", typeof(string));
        qDt.Columns.Add("MilethReputation", typeof(int));
        qDt.Columns.Add("AbelReputation", typeof(int));
        qDt.Columns.Add("RucesionReputation", typeof(int));
        qDt.Columns.Add("SuomiReputation", typeof(int));
        qDt.Columns.Add("RionnagReputation", typeof(int));
        qDt.Columns.Add("OrenReputation", typeof(int));
        qDt.Columns.Add("PietReputation", typeof(int));
        qDt.Columns.Add("LouresReputation", typeof(int));
        qDt.Columns.Add("UndineReputation", typeof(int));
        qDt.Columns.Add("TagorReputation", typeof(int));
        qDt.Columns.Add("BlackSmithing", typeof(int));
        qDt.Columns.Add("BlackSmithingTier", typeof(string));
        qDt.Columns.Add("ArmorSmithing", typeof(int));
        qDt.Columns.Add("ArmorSmithingTier", typeof(string));
        qDt.Columns.Add("JewelCrafting", typeof(int));
        qDt.Columns.Add("JewelCraftingTier", typeof(string));
        qDt.Columns.Add("StoneSmithing", typeof(int));
        qDt.Columns.Add("StoneSmithingTier", typeof(string));
        qDt.Columns.Add("ThievesGuildReputation", typeof(int));
        qDt.Columns.Add("AssassinsGuildReputation", typeof(int));
        qDt.Columns.Add("AdventuresGuildReputation", typeof(int));
        qDt.Columns.Add("BeltQuest", typeof(string));
        qDt.Columns.Add("SavedChristmas", typeof(bool));
        qDt.Columns.Add("RescuedReindeer", typeof(bool));
        qDt.Columns.Add("YetiKilled", typeof(bool));
        qDt.Columns.Add("UnknownStart", typeof(bool));
        qDt.Columns.Add("PirateShipAccess", typeof(bool));
        qDt.Columns.Add("ScubaSchematics", typeof(bool));
        qDt.Columns.Add("ScubaMaterialsQuest", typeof(bool));
        qDt.Columns.Add("ScubaGearCrafted", typeof(bool));
        qDt.Columns.Add("EternalLove", typeof(bool));
        qDt.Columns.Add("EternalLoveStarted", typeof(bool));
        qDt.Columns.Add("UnhappyEnding", typeof(bool));
        qDt.Columns.Add("HonoringTheFallen", typeof(bool));
        qDt.Columns.Add("ReadTheFallenNotes", typeof(bool));
        qDt.Columns.Add("GivenTarnishedBreastplate", typeof(bool));
        qDt.Columns.Add("EternalBond", typeof(string));
        qDt.Columns.Add("ArmorCraftingCodex", typeof(bool));
        qDt.Columns.Add("ArmorApothecaryAccepted", typeof(bool));
        qDt.Columns.Add("ArmorCodexDeciphered", typeof(bool));
        qDt.Columns.Add("ArmorCraftingCodexLearned", typeof(bool));
        qDt.Columns.Add("ArmorCraftingAdvancedCodexLearned", typeof(bool));
        qDt.Columns.Add("CthonicKillTarget", typeof(string));
        qDt.Columns.Add("CthonicFindTarget", typeof(string));
        qDt.Columns.Add("CthonicKillCompletions", typeof(int));
        qDt.Columns.Add("CthonicCleansingOne", typeof(bool));
        qDt.Columns.Add("CthonicCleansingTwo", typeof(bool));
        qDt.Columns.Add("CthonicDepthsCleansing", typeof(bool));
        qDt.Columns.Add("CthonicRuinsAccess", typeof(bool));
        qDt.Columns.Add("CthonicRemainsExplorationLevel", typeof(int));
        qDt.Columns.Add("EndedOmegasRein", typeof(bool));
        qDt.Columns.Add("CraftedMoonArmor", typeof(bool));
        return qDt;
    }

    private static DataTable ComboScrollDataTable()
    {
        var cDt = new DataTable();
        cDt.Columns.Add("Serial", typeof(long));
        cDt.Columns.Add("Combo1", typeof(string));
        cDt.Columns.Add("Combo2", typeof(string));
        cDt.Columns.Add("Combo3", typeof(string));
        cDt.Columns.Add("Combo4", typeof(string));
        cDt.Columns.Add("Combo5", typeof(string));
        cDt.Columns.Add("Combo6", typeof(string));
        cDt.Columns.Add("Combo7", typeof(string));
        cDt.Columns.Add("Combo8", typeof(string));
        cDt.Columns.Add("Combo9", typeof(string));
        cDt.Columns.Add("Combo10", typeof(string));
        cDt.Columns.Add("Combo11", typeof(string));
        cDt.Columns.Add("Combo12", typeof(string));
        cDt.Columns.Add("Combo13", typeof(string));
        cDt.Columns.Add("Combo14", typeof(string));
        cDt.Columns.Add("Combo15", typeof(string));
        return cDt;
    }

    private static DataTable BuffsDataTable()
    {
        var bDt = new DataTable();
        bDt.Columns.Add("Serial", typeof(long));
        bDt.Columns.Add("Name", typeof(string));
        bDt.Columns.Add("TimeLeft", typeof(int));
        return bDt;
    }

    private static DataTable DeBuffsDataTable()
    {
        var dbDt = new DataTable();
        dbDt.Columns.Add("Serial", typeof(long));
        dbDt.Columns.Add("Name", typeof(string));
        dbDt.Columns.Add("TimeLeft", typeof(int));
        return dbDt;
    }

    #endregion
}