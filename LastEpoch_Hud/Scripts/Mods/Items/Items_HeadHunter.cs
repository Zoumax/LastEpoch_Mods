﻿using HarmonyLib;
using MelonLoader;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastEpoch_Hud.Scripts.Mods.Items
{
    [RegisterTypeInIl2Cpp]
    public class Items_HeadHunter : MonoBehaviour
    {
        public static Items_HeadHunter instance { get; private set; }
        public Items_HeadHunter(System.IntPtr ptr) : base(ptr) { }
        public static bool Initialized = false;
        bool InGame = false;

        void Awake()
        {
            instance = this;
            SceneManager.add_sceneLoaded(new System.Action<Scene, LoadSceneMode>(OnSceneLoaded));
        }        
        void Update()
        {
            if ((Unique.Icon.IsNullOrDestroyed()) || (Config.json.IsNullOrDestroyed())) { Assets.Loaded = false; }            
            if (!Assets.Loaded) { Assets.Load(); }
            if (!Basic.AddedToBasicList) { Basic.AddToBasicList(); }
            if (!Unique.AddedToUniqueList) { Unique.AddToUniqueList(); }
            if (!Events.OnKillEvent_Initialized) { Events.Init_OnKillEvent(); }
            if (!Events.OnMinionKillEvent_Initialized) { Events.Init_OnMinionKillEvent(); }

            if ((!Initialized) && (Assets.Loaded) && (Basic.AddedToBasicList) && (Unique.AddedToUniqueList) &&
                    (Events.OnKillEvent_Initialized) && (Events.OnMinionKillEvent_Initialized))
            {
                Initialized = Config.LoadConfig();
            }
        }        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (Scenes.IsGameScene())
            {
                if (!InGame)
                {
                    //Check itemlist here
                    Events.OnKillEvent_Initialized = false;
                    Events.OnMinionKillEvent_Initialized = false;
                }
                InGame = true;
            }
            else if (InGame) { InGame = false; }
        }

        public class Assets
        {
            public static bool Loaded = false;
            public static void Load()
            {
                if ((!Loaded) && (!Hud_Manager.asset_bundle.IsNullOrDestroyed()))
                {
                    foreach (string name in Hud_Manager.asset_bundle.GetAllAssetNames())
                    {
                        if (name.Contains("/headhunter/"))
                        {
                            if ((Functions.Check_Texture(name)) && (name.Contains("icon")) && (Unique.Icon.IsNullOrDestroyed()))
                            {
                                Texture2D texture = Hud_Manager.asset_bundle.LoadAsset(name).TryCast<Texture2D>();
                                Unique.Icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                                //Object.DontDestroyOnLoad(Unique.Icon);
                            }
                            else if ((Functions.Check_Json(name)) && (name.Contains("hh_buffs")) && (Config.json.IsNullOrDestroyed()))
                            {
                                Config.json = Hud_Manager.asset_bundle.LoadAsset(name).TryCast<TextAsset>();
                                //Object.DontDestroyOnLoad(Config.json);
                            }
                        }
                    }
                    if ((!Unique.Icon.IsNullOrDestroyed()) && (!Config.json.IsNullOrDestroyed())) { Loaded = true; }
                    else { Loaded = false; }
                }
            }
        }
        public class Basic
        {
            public static bool AddedToBasicList = false;
            public static readonly byte base_type = 2; //Belt            
            public static readonly string base_name = "HH Leather Belt";
            public static readonly int base_id = 13;
            public static ItemList.EquipmentItem Item()
            {
                ItemList.EquipmentItem item = new ItemList.EquipmentItem
                {
                    classRequirement = ItemList.ClassRequirement.None,
                    implicits = implicits(),
                    subClassRequirement = ItemList.SubClassRequirement.None,
                    cannotDrop = Save_Manager.instance.data.Items.Headhunter.BaseDrop,
                    itemTags = ItemLocationTag.None,
                    levelRequirement = 40,
                    name = base_name,
                    subTypeID = base_id
                };

                return item;
            }
            
            public static void AddToBasicList()
            {
                if ((!AddedToBasicList) && (!Refs_Manager.item_list.IsNullOrDestroyed()))
                {
                    Refs_Manager.item_list.EquippableItems[base_type].subItems.Add(Item());
                    AddedToBasicList = true;
                }
            }

            private static Il2CppSystem.Collections.Generic.List<ItemList.EquipmentImplicit> implicits()
            {
                Il2CppSystem.Collections.Generic.List<ItemList.EquipmentImplicit> implicits = new Il2CppSystem.Collections.Generic.List<ItemList.EquipmentImplicit>();
                implicits.Add(new ItemList.EquipmentImplicit
                {
                    implicitMaxValue = 40,
                    implicitValue = 25,
                    property = SP.Health,
                    specialTag = 0,
                    tags = AT.None,
                    type = BaseStats.ModType.ADDED
                });

                return implicits;
            }
        }
        public class Unique
        {
            public static bool AddedToUniqueList = false;
            public static Sprite Icon = null;
            public static readonly string unique_name = "Headhunter";
            public static readonly ushort unique_id = 500;
            public static readonly string lore = "\"A man's soul rules from a cavern of bone, learns and judges through flesh-born windows. The heart is meat. The head is where the Man is.\"\r\n- Lavianga, Advisor to Kaom";
            public static UniqueList.Entry Item()
            {
                UniqueList.Entry item = new UniqueList.Entry
                {
                    name = unique_name,
                    displayName = unique_name,
                    uniqueID = unique_id,
                    isSetItem = false,
                    setID = 0,
                    overrideLevelRequirement = true,
                    levelRequirement = 40,
                    legendaryType = LegendaryType(),
                    overrideEffectiveLevelForLegendaryPotential = true,
                    effectiveLevelForLegendaryPotential = 0,
                    canDropRandomly = Save_Manager.instance.data.Items.Headhunter.UniqueDrop,
                    rerollChance = 1,
                    itemModelType = UniqueList.ItemModelType.Unique,
                    subTypeForIM = 0,
                    baseType = Basic.base_type,
                    subTypes = SubType(),
                    mods = Mods(),
                    tooltipDescriptions = TooltipDescription(),
                    loreText = lore,
                    tooltipEntries = TooltipEntries(),
                    oldSubTypeID = 0,
                    oldUniqueID = 0
                };

                return item;
            }
            
            public static void AddToUniqueList()
            {
                if ((!AddedToUniqueList) && (!Refs_Manager.unique_list.IsNullOrDestroyed()))
                {
                    Refs_Manager.unique_list.uniques.Add(Item());
                    AddedToUniqueList = true;
                }
            }
            
            private static Il2CppSystem.Collections.Generic.List<byte> SubType()
            {
                Il2CppSystem.Collections.Generic.List<byte> result = new Il2CppSystem.Collections.Generic.List<byte>();
                byte r = (byte)Basic.base_id;
                result.Add(r);

                return result;
            }
            private static Il2CppSystem.Collections.Generic.List<UniqueItemMod> Mods()
            {
                Il2CppSystem.Collections.Generic.List<UniqueItemMod> result = new Il2CppSystem.Collections.Generic.List<UniqueItemMod>();
                result.Add(new UniqueItemMod
                {
                    canRoll = true,
                    property = SP.Strength,
                    tags = AT.None,
                    type = BaseStats.ModType.ADDED,
                    maxValue = 55,
                    value = 40
                });
                result.Add(new UniqueItemMod
                {
                    canRoll = true,
                    property = SP.Dexterity,
                    tags = AT.None,
                    type = BaseStats.ModType.ADDED,
                    maxValue = 55,
                    value = 40
                });
                result.Add(new UniqueItemMod
                {
                    canRoll = true,
                    property = SP.Health,
                    tags = AT.None,
                    type = BaseStats.ModType.ADDED,
                    maxValue = 60,
                    value = 50
                });
                result.Add(new UniqueItemMod
                {
                    canRoll = true,
                    property = SP.Damage,
                    tags = AT.None,
                    type = BaseStats.ModType.INCREASED,
                    maxValue = 0.3f,
                    value = 0.2f
                });

                return result;
            }
            private static Il2CppSystem.Collections.Generic.List<UniqueModDisplayListEntry> TooltipEntries()
            {
                Il2CppSystem.Collections.Generic.List<UniqueModDisplayListEntry> result = new Il2CppSystem.Collections.Generic.List<UniqueModDisplayListEntry>();
                result.Add(new UniqueModDisplayListEntry(0));
                result.Add(new UniqueModDisplayListEntry(1));
                result.Add(new UniqueModDisplayListEntry(2));
                result.Add(new UniqueModDisplayListEntry(3));
                result.Add(new UniqueModDisplayListEntry(128));

                return result;
            }
            private static Il2CppSystem.Collections.Generic.List<ItemTooltipDescription> TooltipDescription()
            {
                Il2CppSystem.Collections.Generic.List<ItemTooltipDescription> result = new Il2CppSystem.Collections.Generic.List<ItemTooltipDescription>();                
                result.Add(new ItemTooltipDescription { description = "When you or your minions Kill a monster, you gain " + Save_Manager.instance.data.Items.Headhunter.MinGenerated +
                        " to " + Save_Manager.instance.data.Items.Headhunter.MaxGenerated + " random Modifiers for " +
                        Save_Manager.instance.data.Items.Headhunter.BuffDuration + " seconds" });

                return result;
            }
            private static UniqueList.LegendaryType LegendaryType()
            {
                UniqueList.LegendaryType legendaryType = UniqueList.LegendaryType.LegendaryPotential;
                if (Save_Manager.instance.data.Items.Headhunter.WeaverWill) { legendaryType = UniqueList.LegendaryType.WeaversWill; }

                return legendaryType;
            }

            [HarmonyPatch(typeof(InventoryItemUI), "GetSpriteImage")]
            public class InventoryItemUI_GetSpriteImage
            {
                [HarmonyPostfix]
                static void Postfix(ref UnityEngine.Sprite __result, ItemData __0, ItemUIContext __1)
                {
                    if ((__0.getAsUnpacked().FullName == unique_name) && (!Icon.IsNullOrDestroyed()))
                    {
                        __result = Icon;
                    }
                }
            }

            [HarmonyPatch(typeof(UITooltipItem), "SetItemSprite")]
            public class UITooltipItem_SetItemSprite
            {
                [HarmonyPostfix]
                static void Postfix(ref UnityEngine.Sprite __result, ItemDataUnpacked __0)
                {
                    if ((__0.FullName == unique_name) && (!Icon.IsNullOrDestroyed()))
                    {
                        __result = Icon;
                    }
                }
            }
        }
        public class Config
        {
            public static TextAsset json = null;
            public static System.Collections.Generic.List<RandomBuffs.HH_Buff> HH_Buff_Config = new System.Collections.Generic.List<RandomBuffs.HH_Buff>();
            public static System.Collections.Generic.List<RandomBuffs.HH_Buff> HH_Buff_Config_Backup = new System.Collections.Generic.List<RandomBuffs.HH_Buff>();
            public static bool LoadConfig()
            {
                bool result = false;
                if (!Save_Manager.instance.IsNullOrDestroyed())
                {
                    HH_Buff_Config = new System.Collections.Generic.List<RandomBuffs.HH_Buff>();
                    HH_Buff_Config_Backup = new System.Collections.Generic.List<RandomBuffs.HH_Buff>();
                    if (!System.IO.File.Exists(Save_Manager.instance.path + filename)) { DefaultConfig(); }
                    else
                    {
                        try
                        {
                            HH_Buff_Config = JsonConvert.DeserializeObject<System.Collections.Generic.List<RandomBuffs.HH_Buff>>(System.IO.File.ReadAllText(Save_Manager.instance.path + filename));
                            HH_Buff_Config_Backup = HH_Buff_Config;
                            RandomBuffs.Generate_HH_BuffsList();
                        }
                        catch { DefaultConfig(); }
                        result = true;
                    }
                }

                return result;
            }
            public static void SaveConfig()
            {
                HH_Buff_Config_Backup = HH_Buff_Config; //Use to check if buffs changed                
                string jsonString = JsonConvert.SerializeObject(HH_Buff_Config, Formatting.Indented);
                if (!System.IO.Directory.Exists(Save_Manager.instance.path)) { System.IO.Directory.CreateDirectory(Save_Manager.instance.path); }
                if (System.IO.File.Exists(Save_Manager.instance.path + filename)) { System.IO.File.Delete(Save_Manager.instance.path + filename); }
                System.IO.File.WriteAllText(Save_Manager.instance.path + filename, jsonString);
                RandomBuffs.Generate_HH_BuffsList();
            }

            private static string filename = "hh_buffs.json";
            private static void DefaultConfig()
            {
                HH_Buff_Config = JsonConvert.DeserializeObject<System.Collections.Generic.List<RandomBuffs.HH_Buff>>(json.text);
                HH_Buff_Config_Backup = HH_Buff_Config;
                SaveConfig();
            }
        }
        public class RandomBuffs
        {
            public struct HH_Buff
            {
                public string property;
                public bool added;
                public bool increased;
                public int max_added;
                public int max_increased;
            }
            public static string hh_buff = "HHBuff";
            public static void GenerateBuffs()
            {
                int NbBuff = Random.Range(Save_Manager.instance.data.Items.Headhunter.MinGenerated, Save_Manager.instance.data.Items.Headhunter.MaxGenerated + 1);
                for (int i = 0; i < NbBuff; i++)
                {
                    Actor playerActor = PlayerFinder.getPlayerActor();
                    Buff random_buff = Generate_Random_HHBuff();
                    int max_value = 255;
                    int max_addvalue = max_value;
                    int max_increasedvalue = max_value;
                    bool found = false;
                    foreach (var p in Config.HH_Buff_Config)
                    {
                        if (random_buff.name.Contains(p.property))
                        {
                            if ((p.max_added == 0) | (p.max_added > max_value) | (p.max_added < 0)) { max_addvalue = max_value; }
                            else { max_addvalue = p.max_added; }
                            if ((p.max_increased == 0) | (p.max_increased > max_value) | (p.max_increased < 0)) { max_increasedvalue = max_value; }
                            else { max_increasedvalue = p.max_increased; }
                            found = true;
                            break;
                        }
                    }
                    if (!found) { Main.logger_instance.Msg("Error : Property " + random_buff.name + " Not Found"); }
                    if (random_buff != null)
                    {
                        float old_value = 0;
                        string BuffToRemove = "";
                        foreach (Buff player_buff in playerActor.statBuffs.buffs)
                        {
                            if (player_buff.name.Contains(random_buff.name))
                            {
                                BuffToRemove = player_buff.name;
                                float new_value = 0;
                                if (!GetIsIncrease(player_buff))
                                {
                                    old_value = player_buff.stat.addedValue;
                                    if (old_value < max_addvalue)
                                    {
                                        new_value = old_value + Save_Manager.instance.data.Items.Headhunter.AddValue;
                                    }
                                    else { new_value = max_addvalue; }
                                    random_buff.stat.addedValue = new_value;
                                }
                                else
                                {
                                    old_value = player_buff.stat.increasedValue;
                                    if (old_value < max_increasedvalue)
                                    {
                                        new_value = old_value + Save_Manager.instance.data.Items.Headhunter.IncreasedValue;
                                    }
                                    else { new_value = max_increasedvalue; }
                                    random_buff.stat.increasedValue = new_value;
                                }
                                break;
                            }
                        }
                        if (BuffToRemove != null) { playerActor.statBuffs.removeBuffsWithName(BuffToRemove); }
                        playerActor.statBuffs.addBuff(random_buff.remainingDuration, random_buff.stat.property,
                            random_buff.stat.addedValue, random_buff.stat.increasedValue, random_buff.stat.moreValues,
                            random_buff.stat.tags, random_buff.stat.specialTag, random_buff.name);
                    }
                }
            }
            public static void Generate_HH_BuffsList()
            {
                HH_Buff_Add = new System.Collections.Generic.List<HH_Buff>();
                HH_Buff_Increased = new System.Collections.Generic.List<HH_Buff>();
                for (int i = 0; i < System.Enum.GetValues(typeof(SP)).Length; i++)
                {
                    SP property = (SP)i;
                    foreach (HH_Buff hh_buff in Config.HH_Buff_Config)
                    {
                        if (hh_buff.property == property.ToString())
                        {
                            if (hh_buff.added) { HH_Buff_Add.Add(hh_buff); }
                            if (hh_buff.increased) { HH_Buff_Increased.Add(hh_buff); }
                            break;
                        }
                    }
                }
            }

            private static SP GetPropertyFromName(string name)
            {
                SP result = SP.None;
                for (int i = 0; i < System.Enum.GetValues(typeof(SP)).Length; i++)
                {
                    SP property = (SP)i;
                    if (name == property.ToString())
                    {
                        result = property;
                        break;
                    }
                }

                return result;
            }
            private static Buff Generate_Random_HHBuff()
            {
                Buff result = null;
                byte specialTag = 0;
                AT tags = AT.None;
                float addedValue = 0;
                float increasedValue = 0;
                string name = hh_buff + " : ";
                bool error = false;
                bool addedd = false;
                SP property = SP.None;

                if ((HH_Buff_Add.Count > 0) && (HH_Buff_Increased.Count > 0))
                {
                    int add_increase = Random.Range(0, 2);
                    if (add_increase == 0) { addedd = true; }
                }
                else if (HH_Buff_Add.Count > 0) { addedd = true; }
                else if (HH_Buff_Increased.Count > 0) { }
                else { error = true; }
                if (!error)
                {
                    if (addedd)
                    {
                        int random = Random.Range(0, HH_Buff_Add.Count);
                        if (random > HH_Buff_Add.Count) { random = HH_Buff_Add.Count - 1; }
                        string property_name = HH_Buff_Add[random].property;
                        property = GetPropertyFromName(property_name);
                        name += "Add ";
                        addedValue = Save_Manager.instance.data.Items.Headhunter.AddValue;
                    }
                    else
                    {
                        int random = Random.Range(0, HH_Buff_Increased.Count);
                        if (random > HH_Buff_Increased.Count) { random = HH_Buff_Increased.Count - 1; }
                        string property_name = HH_Buff_Increased[random].property;
                        property = GetPropertyFromName(property_name);
                        name += "Increased ";
                        increasedValue = Save_Manager.instance.data.Items.Headhunter.IncreasedValue;
                    }
                    if (property != SP.None)
                    {
                        name += property.ToString();
                        result = new Buff
                        {
                            name = name,
                            remainingDuration = Save_Manager.instance.data.Items.Headhunter.BuffDuration,
                            stat = new Stats.Stat
                            {
                                addedValue = addedValue,
                                increasedValue = increasedValue,
                                moreValues = null,
                                property = property,
                                specialTag = specialTag,
                                tags = tags
                            }
                        };
                    }
                }

                return result;
            }
            private static bool GetIsIncrease(Buff b)
            {
                if (b.name.Contains("Increased")) { return true; }
                else { return false; }
            }
            private static System.Collections.Generic.List<HH_Buff> HH_Buff_Add = new System.Collections.Generic.List<HH_Buff>();
            private static System.Collections.Generic.List<HH_Buff> HH_Buff_Increased = new System.Collections.Generic.List<HH_Buff>();
        }        
        public class Events
        {
            public static bool OnKillEvent_Initialized = false;
            public static void Init_OnKillEvent()
            {
                if (!Refs_Manager.player_actor.IsNullOrDestroyed())
                {
                    if (!Refs_Manager.player_actor.gameObject.IsNullOrDestroyed())
                    {
                        AbilityEventListener listener = Refs_Manager.player_actor.gameObject.GetComponent<AbilityEventListener>();
                        if (!listener.IsNullOrDestroyed())
                        {
                            listener.add_onKillEvent(OnKillAction);
                            OnKillEvent_Initialized = true;
                        }
                    }
                }
            }
            private static readonly System.Action<Ability, Actor> OnKillAction = new System.Action<Ability, Actor>(OnKill);
            private static void OnKill(Ability ability, Actor killedActor)
            {
                if (!Refs_Manager.player_actor.IsNullOrDestroyed())
                {
                    if (Refs_Manager.player_actor.itemContainersManager.hasUniqueEquipped(Unique.unique_id))
                    {
                        RandomBuffs.GenerateBuffs();
                    }
                }
            }

            public static bool OnMinionKillEvent_Initialized = false;
            public static void Init_OnMinionKillEvent()
            {
                if (!Refs_Manager.player_actor.IsNullOrDestroyed())
                {
                    if (!Refs_Manager.player_actor.gameObject.IsNullOrDestroyed())
                    {
                        SummonTracker listener = Refs_Manager.player_actor.gameObject.GetComponent<SummonTracker>();
                        if (!listener.IsNullOrDestroyed())
                        {
                            listener.add_minionKillEvent(OnMinionKillAction);
                            OnMinionKillEvent_Initialized = true;
                        }
                    }
                }
            }
            private static readonly System.Action<Summoned, Ability, Actor> OnMinionKillAction = new System.Action<Summoned, Ability, Actor>(OnMinionKill);
            private static void OnMinionKill(Summoned summon, Ability ability, Actor killedActor)
            {
                if (!Refs_Manager.player_actor.IsNullOrDestroyed())
                {
                    if (Refs_Manager.player_actor.itemContainersManager.hasUniqueEquipped(Unique.unique_id))
                    {
                        RandomBuffs.GenerateBuffs();
                    }
                }
            }
        }        
    }
}