﻿using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace LastEpochMods.Managers
{
    public class GUI_Manager
    {
        public static void OnSceneWasInitialized(string sceneName)
        {
            if ((sceneName == "CharacterSelectScene") || (Scenes_Manager.GameScene()))
            {
                GUI_Manager.Textures.OnSceneWasInitialized(); //Initialized textures for menus
                //Mods.Items.Skins.CosmeticPanel.Slots.InitSlots(); //Textures
            }
        }
        public static void Update()
        {
            Base.Functions.Update();
            if (Base.Initialized)
            {
                PauseMenu.Functions.Update();
                if (Scenes_Manager.GameScene())
                {
                    InventoryPanel.Functions.Update();
                    BlessingsPanel.Functions.Update();
                }
            }
        }
        public static void UpdateGUI()
        {
            if (Base.Initialized) //Base Initialized
            {
                if (Scenes_Manager.GameScene())
                {
                    if (PauseMenu.Initialized) { PauseMenu.UI.UpdateGUI(); } //Pause Menu UI
                    Mods.Items.HeadHunter.Ui.Update();
                    Mods.Items.Skins.UI.UpdateGui(); //Cosmetic Panel
                }
            }
        }

        public class Base
        {
            public static bool Initialized = false;

            public class Refs
            {
                public static GameObject GameObject_GUI = null;
                public static UIBase Game_UIBase = null;
            }
            public class Functions
            {
                public static void Init()
                {
                    Initialized = false;
                    foreach (Object obj in Object.FindObjectsOfType<GameObject>())
                    {
                        if (obj.name == "GUI")
                        {
                            Refs.GameObject_GUI = obj.TryCast<GameObject>();
                            Refs.Game_UIBase = Refs.GameObject_GUI.GetComponent<UIBase>();
                            Main.logger_instance.Msg("Game GUI Found, Initializing Mod GUI");
                            Initialized = true;

                            break;
                        }
                    }
                }
                public static void Update()
                {
                    if ((Refs.GameObject_GUI.IsNullOrDestroyed()) || (Refs.GameObject_GUI.IsNullOrDestroyed()))
                    {
                        Initialized = false;
                    }
                    if (!Initialized) { Init(); }
                }
            }
        }
        public class CharacterSelectionMenu
        {
            //public static bool isVisible = false;
            
            public class Btns
            {
                public static void Btn_RemoveReq_Click()
                {
                    UI.ShowRemoveReqSection = !UI.ShowRemoveReqSection;
                }
                public static void Btn_Items_Remove_Level_Req_Click()
                {
                    Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq;
                }
                public static void Btn_Items_Remove_Class_Req_Click()
                {
                    Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq;
                }
                public static void Btn_Items_Remove_SubClass_Req_Click()
                {
                    Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq;
                }
                public static void Btn_Items_Remove_Set_Req_Click()
                {
                    Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req;
                }
            }
            public class UI
            {
                public static bool login = false;
                public static bool base_stats = false;
                public static readonly float base_stats_h = 275f;
                public static bool items = false;
                public static readonly float items_h = 100f;
                public static bool ShowRemoveReqSection = false;
                public static readonly float ui_size_w = 200f;
                public static readonly float ui_size_h = 140f;
                public static readonly float ui_pos_x = 10f;
                public static readonly float margin_w = 5f;
                public static readonly float line_h = 30f;
                public static readonly float line_margin_h = 5f;
                public static readonly float text_size_h = line_h - (2 * line_margin_h);
                public static readonly float button_size_w = 80;
                public static readonly float button_size_h = line_h - (2 * line_margin_h);

                public static void UpdateGUI()
                {
                    if (Scenes_Manager.CurrentName == Scenes_Manager.MenuNames[3])
                    {
                        GeneralFunctions.SaveConfig();
                        float section_size_w = 200;
                        int nb_sections = 2;
                        float menu_margin_w = 5f;
                        float menu_margin_h = 5f;
                        float menu_w = (section_size_w * nb_sections) + ((nb_sections + 1) * menu_margin_w);
                        float menu_h = 40;
                        float start_x = (Screen.width / 2) - (menu_w / 2);
                        float start_y = 0;
                        GUI.DrawTexture(new Rect(start_x, start_y, menu_w, menu_h), GUI_Manager.Textures.black);
                        float menu_top_pos_y = start_y;
                        float menu_bottom_pos_y = menu_top_pos_y + menu_h;
                        float first_section_x = start_x;
                        float second_section_x = start_x + margin_w + section_size_w;
                        //float third_section_x = second_section_x + margin_w + section_size_w;

                        if (GUI.Button(new Rect(first_section_x + margin_w, menu_top_pos_y + menu_margin_h, section_size_w, menu_h - (2 * menu_margin_h)), "Base Stats", GUI_Manager.Styles.Button_Style(base_stats))) { Functions.ShowHide_BaseStats(); }
                        if (base_stats)
                        {
                            int maxvalue = 999;
                            float min = 0;
                            float max = 255;
                            float multiplier = maxvalue / max;
                            float pos_x = first_section_x;
                            float pos_y = menu_bottom_pos_y;
                            float max_w = section_size_w;// - (2 * menu_margin_w);
                            GUI.DrawTexture(new Rect(pos_x, pos_y, section_size_w + (2 * menu_margin_w), base_stats_h), GUI_Manager.Textures.black);
                            pos_x += menu_margin_w;
                            //pos_y += Ui.line_margin_h;

                            //Base Str
                            GUI.DrawTexture(new Rect(pos_x, pos_y, max_w, 50), GUI_Manager.Textures.grey);
                            GUI.Label(new Rect(pos_x + 5, pos_y + ((line_h - text_size_h) / 2), max_w - (button_size_w + (2 * margin_w)), text_size_h), "Strenght", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (max_w - button_size_w), pos_y + ((line_h - button_size_h) / 2), button_size_w, button_size_h), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseStrength), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseStrength)))
                            {
                                Save_Manager.Data.UserData.Character.BaseStats.Enable_baseStrength = !Save_Manager.Data.UserData.Character.BaseStats.Enable_baseStrength;
                            }
                            pos_y += 30;

                            float str_temp_value = Save_Manager.Data.UserData.Character.BaseStats.baseStrength / multiplier;
                            str_temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, pos_y + 5, (max_w - 45), 20), str_temp_value, min, max); //* multiplier);
                            string str_value_string = GUI.TextArea(new Rect((pos_x + max_w - 40), pos_y, 40, 20), (str_temp_value * multiplier).ToString(), GUI_Manager.Styles.TextArea_Style());
                            try
                            {
                                int text = int.Parse(str_value_string, CultureInfo.InvariantCulture.NumberFormat);
                                str_temp_value = text / multiplier;
                            }
                            catch { }
                            Save_Manager.Data.UserData.Character.BaseStats.baseStrength = (int)(str_temp_value * multiplier);
                            pos_y += 20 + line_margin_h;

                            //Base Intel
                            GUI.DrawTexture(new Rect(pos_x, pos_y, max_w, 50), GUI_Manager.Textures.grey);
                            GUI.Label(new Rect(pos_x + 5, pos_y + ((line_h - text_size_h) / 2), max_w - (button_size_w + (2 * margin_w)), text_size_h), "Intelligence", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (max_w - button_size_w), pos_y + ((line_h - button_size_h) / 2), button_size_w, button_size_h), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseIntelligence), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseIntelligence)))
                            {
                                Save_Manager.Data.UserData.Character.BaseStats.Enable_baseIntelligence = !Save_Manager.Data.UserData.Character.BaseStats.Enable_baseIntelligence;
                            }
                            pos_y += 30;
                            float int_temp_value = Save_Manager.Data.UserData.Character.BaseStats.baseIntelligence / multiplier;
                            int_temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, pos_y + 5, (max_w - 45), 20), int_temp_value, min, max); //* multiplier);
                            string int_value_string = GUI.TextArea(new Rect((pos_x + max_w - 40), pos_y, 40, 20), (int_temp_value * multiplier).ToString(), GUI_Manager.Styles.TextArea_Style());
                            try
                            {
                                int text = int.Parse(int_value_string, CultureInfo.InvariantCulture.NumberFormat);
                                int_temp_value = text / multiplier;
                            }
                            catch { }
                            Save_Manager.Data.UserData.Character.BaseStats.baseIntelligence = (int)(int_temp_value * multiplier);
                            pos_y += 20 + line_margin_h;

                            //Base Vitality
                            GUI.DrawTexture(new Rect(pos_x, pos_y, max_w, 50), GUI_Manager.Textures.grey);
                            GUI.Label(new Rect(pos_x + 5, pos_y + ((line_h - text_size_h) / 2), max_w - (button_size_w + (2 * margin_w)), text_size_h), "Vitality", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (max_w - button_size_w), pos_y + ((line_h - button_size_h) / 2), button_size_w, button_size_h), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseVitality), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseVitality)))
                            {
                                Save_Manager.Data.UserData.Character.BaseStats.Enable_baseVitality = !Save_Manager.Data.UserData.Character.BaseStats.Enable_baseVitality;
                            }
                            pos_y += 30;
                            float vita_temp_value = Save_Manager.Data.UserData.Character.BaseStats.baseVitality / multiplier;
                            vita_temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, pos_y + 5, (max_w - 45), 20), vita_temp_value, min, max);
                            string vita_value_string = GUI.TextArea(new Rect((pos_x + max_w - 40), pos_y, 40, 20), (vita_temp_value * multiplier).ToString(), GUI_Manager.Styles.TextArea_Style());
                            try
                            {
                                int text = int.Parse(vita_value_string, CultureInfo.InvariantCulture.NumberFormat);
                                vita_temp_value = text / multiplier;
                            }
                            catch { }
                            Save_Manager.Data.UserData.Character.BaseStats.baseVitality = (int)(vita_temp_value * multiplier);
                            pos_y += 20 + line_margin_h;

                            //Base Dexterity
                            GUI.DrawTexture(new Rect(pos_x, pos_y, max_w, 50), GUI_Manager.Textures.grey);
                            GUI.Label(new Rect(pos_x + 5, pos_y + ((line_h - text_size_h) / 2), max_w - (button_size_w + (2 * margin_w)), text_size_h), "Dexterity", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (max_w - button_size_w), pos_y + ((line_h - button_size_h) / 2), button_size_w, button_size_h), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseDexterity), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseDexterity)))
                            {
                                Save_Manager.Data.UserData.Character.BaseStats.Enable_baseDexterity = !Save_Manager.Data.UserData.Character.BaseStats.Enable_baseDexterity;
                            }
                            pos_y += 30;
                            float dex_temp_value = Save_Manager.Data.UserData.Character.BaseStats.baseDexterity / multiplier;
                            dex_temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, pos_y + 5, (max_w - 45), 20), dex_temp_value, min, max);
                            string dex_value_string = GUI.TextArea(new Rect((pos_x + max_w - 40), pos_y, 40, 20), (dex_temp_value * multiplier).ToString(), GUI_Manager.Styles.TextArea_Style());
                            try
                            {
                                int text = int.Parse(dex_value_string, CultureInfo.InvariantCulture.NumberFormat);
                                dex_temp_value = text / multiplier;
                            }
                            catch { }
                            Save_Manager.Data.UserData.Character.BaseStats.baseDexterity = (int)(dex_temp_value * multiplier);
                            pos_y += 20 + line_margin_h;

                            //Base Attunement
                            GUI.DrawTexture(new Rect(pos_x, pos_y, max_w, 50), GUI_Manager.Textures.grey);
                            GUI.Label(new Rect(pos_x + 5, pos_y + ((line_h - text_size_h) / 2), max_w - (button_size_w + (2 * margin_w)), text_size_h), "Attunement", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (max_w - button_size_w), pos_y + ((line_h - button_size_h) / 2), button_size_w, button_size_h), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseAttunement), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Character.BaseStats.Enable_baseAttunement)))
                            {
                                Save_Manager.Data.UserData.Character.BaseStats.Enable_baseAttunement = !Save_Manager.Data.UserData.Character.BaseStats.Enable_baseAttunement;
                            }
                            pos_y += 30;
                            float att_temp_value = Save_Manager.Data.UserData.Character.BaseStats.baseAttunement / multiplier;
                            att_temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, pos_y + 5, (max_w - 45), 20), att_temp_value, min, max);
                            string att_value_string = GUI.TextArea(new Rect((pos_x + max_w - 40), pos_y, 40, 20), (att_temp_value * multiplier).ToString(), GUI_Manager.Styles.TextArea_Style());
                            try
                            {
                                int text = int.Parse(att_value_string, CultureInfo.InvariantCulture.NumberFormat);
                                att_temp_value = text / multiplier;
                            }
                            catch { }
                            Save_Manager.Data.UserData.Character.BaseStats.baseAttunement = (int)(att_temp_value * multiplier);
                        }

                        if (GUI.Button(new Rect(second_section_x + margin_w, menu_top_pos_y + menu_margin_h, section_size_w, menu_h - (2 * menu_margin_h)), "Items", GUI_Manager.Styles.Button_Style(items))) { Functions.ShowHide_Items(); }
                        if (items)
                        {
                            float pos_x = second_section_x;
                            float pos_y = menu_bottom_pos_y;
                            GUI.DrawTexture(new Rect(pos_x, pos_y, section_size_w + (2 * menu_margin_w), items_h), GUI_Manager.Textures.black);
                            pos_x += menu_margin_w;

                            GUI.DrawTexture(new Rect(pos_x, pos_y, section_size_w, 50), GUI_Manager.Textures.grey);
                            pos_x += menu_margin_w;
                            pos_y += menu_margin_h;
                            GUI.Label(new Rect(pos_x, pos_y, section_size_w - (button_size_w + (2 * margin_w)), 20), "Affixs can be", GUI_Manager.Styles.Text_Style());
                            if (GUI.Button(new Rect(pos_x + (section_size_w - button_size_w - menu_margin_w), pos_y, button_size_w, 20), Functions.EnableDisableBtn(Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots), GUI_Manager.Styles.Button_Style(Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots)))
                            {
                                Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots = !Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots;
                            }
                            pos_y += 15;
                            GUI.Label(new Rect(pos_x, pos_y + 5, section_size_w - (2 * margin_w), 20), "crafted on any Item", GUI_Manager.Styles.Text_Style());
                            pos_x -= menu_margin_w;
                            pos_y += 30 + line_margin_h;

                            if (GUI.Button(new Rect(pos_x, pos_y, section_size_w, 40), "Remove Req", GUI_Manager.Styles.Button_Style(ShowRemoveReqSection))) { Btns.Btn_RemoveReq_Click(); }
                            if (ShowRemoveReqSection)
                            {
                                pos_x += section_size_w;
                                pos_y -= 5;
                                GUI.DrawTexture(new Rect(pos_x, pos_y, (section_size_w + (2 * menu_margin_w)), 180 + 5), GUI_Manager.Textures.windowBackground);
                                pos_x += 5;
                                pos_y += 5;
                                //CustomControls.EnableButton("Level", pos_x, pos_y, Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq, Btns.Btn_Items_Remove_Level_Req_Click);
                                //pos_y += 45;
                                //CustomControls.EnableButton("Class", pos_x, pos_y, Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq, Btns.Btn_Items_Remove_Class_Req_Click);
                                //pos_y += 45;
                                //CustomControls.EnableButton("SubClass", pos_x, pos_y, Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq, Btns.Btn_Items_Remove_SubClass_Req_Click);
                                //pos_y += 45;
                                CustomControls.EnableButton("Set", pos_x, pos_y, Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req, Btns.Btn_Items_Remove_Set_Req_Click);
                            }
                        }
                    }
                }
            }
            public class Functions
            {
                public static void ShowFps()
                {
                    try
                    {
                        if (!Base.Refs.Game_UIBase.fpsDisplay.showFps)
                        {
                            Base.Refs.Game_UIBase.fpsDisplay.showFps = true;
                            Base.Refs.Game_UIBase.fpsDisplay.display.gameObject.SetActive(true);
                        }
                    }
                    catch { Main.logger_instance.Error("GUI is null or destroyed"); }
                }
                public static string EnableDisableBtn(bool check)
                {
                    if (check) { return "Disable"; }
                    else { return "Enable"; }
                }
                public static void ShowHide_BaseStats()
                {
                    UI.base_stats = !UI.base_stats;
                    if (UI.base_stats)
                    {
                        UI.login = false;
                        UI.items = false;
                    }
                }
                public static void ShowHide_Items()
                {
                    UI.items = !UI.items;
                    if (UI.items)
                    {
                        UI.login = false;
                        UI.base_stats = false;
                    }
                }
            }
            /*public class Hooks
            {
                [HarmonyPatch(typeof(CharacterSelect), "OnDisable")]
                public class CharacterSelect_OnDisable
                {
                    [HarmonyPostfix]
                    static void Postfix(CharacterSelect __instance)
                    {
                        isVisible = false;
                    }
                }
            }*/
        }
        public class PauseMenu
        {
            public static bool Initialized = false;
            public static bool ForceOpen = false;

            public class Refs
            {
                public static GameObject PauseMenu = null;
                public static GameObject Default_PauseMenu_Btns = null;
                public static Button Resume_Btn = null;
                public static Button Settings_Btn = null;
                public static Button GameGuide_Btn = null;
                public static Button Leave_Btn = null;
                public static Button Exit_Btn = null;
            }
            public class UI
            {
                public static bool Show_Wolf = false;
                public static bool Show_Scorpion = false;

                public static float Section_1_X = 0;
                public static float Section_2_X = 0;
                public static float Section_3_X = 0;
                public static float Content_Y = 0;
                public static float Section_W = 0;
                public static float ui_margin = 15;
                public static float content_margin = 5;

                public static void UpdateGUI()
                {
                    if ((Functions.IsPauseMenuOpen()) || (ForceOpen))
                    {
                        GeneralFunctions.SaveConfig(); //Check Config Changed

                        float pos_x = 0;
                        float pos_y = 0;
                        

                        #region Menu
                        float mods_basebtns_width = (Screen.width * 15) / 100; //15% Largeur du menu                        
                        float mods_basebtns_btn_width = mods_basebtns_width - (2 * ui_margin);
                        float mods_basebtns_btn_height = (Screen.height * 8) / 100; //8% hauteur des boutons
                        float mods_basebtns_height = (6 * mods_basebtns_btn_height) + (7 * content_margin);

                        float temp_x = pos_x + ui_margin;
                        float temp_y = pos_y + ui_margin;

                        GUI.DrawTexture(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_height), Textures.black);
                        temp_x += content_margin;
                        temp_y += content_margin;
                        mods_basebtns_btn_width -= (2 * content_margin);
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "Character", Styles.PauseMenu_BaseButton(BaseMenu.Character.Show))) { BaseMenu.Character.Btn_Click(); }
                        temp_y += mods_basebtns_btn_height + content_margin;
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "Items", Styles.PauseMenu_BaseButton(BaseMenu.Items.Show))) { BaseMenu.Items.Btn_Click(); }
                        temp_y += mods_basebtns_btn_height + content_margin;
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "Scenes", Styles.PauseMenu_BaseButton(BaseMenu.Scenes.Show))) { BaseMenu.Scenes.Btn_Click(); }
                        temp_y += mods_basebtns_btn_height + content_margin;
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "ForceDrop", Styles.PauseMenu_BaseButton(BaseMenu.ForceDrop.Show))) { BaseMenu.ForceDrop.Btn_Click(); }
                        temp_y += mods_basebtns_btn_height + content_margin;
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "Tree / Skills", Styles.PauseMenu_BaseButton(BaseMenu.SkillsTree.Show))) { BaseMenu.SkillsTree.Btn_Click(); }
                        temp_y += mods_basebtns_btn_height + content_margin;
                        if (GUI.Button(new Rect(temp_x, temp_y, mods_basebtns_btn_width, mods_basebtns_btn_height), "Headhunter", Styles.PauseMenu_BaseButton(BaseMenu.Headhunter.Show))) { BaseMenu.Headhunter.Btn_Click(); }
                        #endregion

                        #region BaseButtons
                        float base_btns_w = (Screen.width * 10) / 100; //8% largeur des boutons
                        float base_btns_h = (Screen.height * 5) / 100; //5% hauteur des boutons
                        float Base_w = (5 * base_btns_w) + (6 * content_margin);
                        float Base_h = base_btns_h + (2 * content_margin);
                        float Base_pos_x = (Screen.width / 2) - (Base_w / 2);
                        float Base_pos_y = Screen.height - Base_h - ui_margin;

                        GUI.DrawTexture(new Rect(Base_pos_x, Base_pos_y, Base_w, Base_h), Textures.black);
                        Base_pos_x += content_margin;
                        Base_pos_y += content_margin;
                        if (GUI.Button(new Rect(Base_pos_x, Base_pos_y, base_btns_w, base_btns_h), "Resume", Styles.PauseMenu_BaseButton(false))) { DefaultBtns.Resume_Click(); }
                        Base_pos_x += base_btns_w + content_margin;
                        if (GUI.Button(new Rect(Base_pos_x, Base_pos_y, base_btns_w, base_btns_h), "Settings", Styles.PauseMenu_BaseButton(false))) { DefaultBtns.Settings_Click(); }
                        Base_pos_x += base_btns_w + content_margin;
                        if (GUI.Button(new Rect(Base_pos_x, Base_pos_y, base_btns_w, base_btns_h), "Game Guide", Styles.PauseMenu_BaseButton(false))) { DefaultBtns.GameGuide_Click(); }
                        Base_pos_x += base_btns_w + content_margin;
                        if (GUI.Button(new Rect(Base_pos_x, Base_pos_y, base_btns_w, base_btns_h), "Leave Game", Styles.PauseMenu_BaseButton(false))) { DefaultBtns.LeaveGame_Click(); }
                        Base_pos_x += base_btns_w + content_margin;
                        if (GUI.Button(new Rect(Base_pos_x, Base_pos_y, base_btns_w, base_btns_h), "Exit Desktop", Styles.PauseMenu_BaseButton(false))) { DefaultBtns.ExitDesktop_Click(); }
                        #endregion

                        #region Content
                        float mods_content_width = Screen.width - (2 * ui_margin) - mods_basebtns_width;
                        float mods_content_height = Screen.height - (2 * ui_margin);
                        float mods_content_x = mods_basebtns_width + ui_margin;
                        float mods_content_y = ui_margin;
                        //Ref for ForceDrop
                        Content_Y = mods_content_y;
                        Section_1_X = mods_content_x;
                        Section_W = (mods_content_width - (2 * ui_margin)) / 3;
                        Section_2_X = Section_1_X + Section_W + ui_margin;
                        Section_3_X = Section_2_X + Section_W + ui_margin;


                        if (BaseMenu.Character.Show)
                        {
                            float scene_x = Section_1_X;
                            float scene_y = Content_Y;

                            //Cheats
                            float character_cheats_h = 700 + (16 * content_margin);
                            if (!Mods.Character.Cheats.LevelUp.AlreadyMaxLevel()) { character_cheats_h += 80 + (2 * content_margin); }
                            GUI.DrawTexture(new Rect(scene_x, scene_y, Section_W, character_cheats_h), Textures.black);
                            float section1_x = scene_x + content_margin;
                            float section1_y = scene_y + content_margin;
                            float section1_w = Section_W - (2 * content_margin);
                            GUI.TextField(new Rect(section1_x, section1_y, section1_w, 40), "Cheats", Styles.Content_Title());
                            section1_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section1_x, section1_y, section1_w, (character_cheats_h - 40 - (3 * content_margin))), Textures.grey);
                            section1_x += content_margin;
                            section1_y += content_margin;
                            float section1_content_w = section1_w - (2 * content_margin);

                            section1_y += CustomControls.Toggle(section1_x, section1_y, section1_content_w, "GodMode", ref Save_Manager.Data.UserData.Character.Cheats.Enable_GodMode);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "GodMode", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.Cheats.Enable_GodMode)))
                            { Save_Manager.Data.UserData.Character.Cheats.Enable_GodMode = !Save_Manager.Data.UserData.Character.Cheats.Enable_GodMode; }
                            section1_y += 40 + content_margin;*/

                            section1_y += CustomControls.Toggle(section1_x, section1_y, section1_content_w, "Force Low Life", ref Save_Manager.Data.UserData.Character.Cheats.Enable_LowLife);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Force Low Life", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.Cheats.Enable_LowLife)))
                            { Save_Manager.Data.UserData.Character.Cheats.Enable_LowLife = !Save_Manager.Data.UserData.Character.Cheats.Enable_LowLife; }
                            section1_y += 40 + content_margin;*/

                            if (!Mods.Character.Cheats.LevelUp.AlreadyMaxLevel())
                            {
                                section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Level Up", Mods.Character.Cheats.LevelUp.Once);

                                /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Level Up", Managers.GUI_Manager.Styles.Content_Button()))
                                { Mods.Character.Cheats.LevelUp.Once(); }
                                section1_y += 40 + content_margin;*/

                                section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Level Up to Max", Mods.Character.Cheats.LevelUp.ToMax);

                                /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Level Up to Max", Managers.GUI_Manager.Styles.Content_Button()))
                                { Mods.Character.Cheats.LevelUp.ToMax(); }
                                section1_y += 40 + content_margin;*/
                            }

                            section1_y += CustomControls.Toggle_FloatValue(section1_x, section1_y, section1_content_w, "Leach Rate", ref Save_Manager.Data.UserData.Character.Cheats.leech_rate, ref Save_Manager.Data.UserData.Character.Cheats.Enable_leech_rate);

                            /*GUI.TextField(new Rect(section1_x, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Leach Rate", Styles.Content_Text());
                            float Cheat_LeechRate_Temp = Save_Manager.Data.UserData.Character.Cheats.leech_rate;
                            Cheat_LeechRate_Temp = GUI.HorizontalSlider(new Rect(section1_x + content_margin, section1_y + 50, section1_content_w - (2 * content_margin), 20), Cheat_LeechRate_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Cheat_LeechRate_Temp.ToString(), Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.Cheats.Enable_leech_rate)))
                            { Save_Manager.Data.UserData.Character.Cheats.Enable_leech_rate = !Save_Manager.Data.UserData.Character.Cheats.Enable_leech_rate; }
                            Save_Manager.Data.UserData.Character.Cheats.leech_rate = (int)Cheat_LeechRate_Temp;
                            section1_y += 70 + content_margin;*/

                            section1_y += CustomControls.Toggle_FloatPercent(section1_x, section1_y, section1_content_w, "Auto Potion When Health under", 0f, 255f, false, ref Save_Manager.Data.UserData.Character.Cheats.autoPot, ref Save_Manager.Data.UserData.Character.Cheats.Enable_AutoPot);

                            //Autpot
                            /*GUI.TextField(new Rect(section1_x, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Auto Potion When Health under", Styles.Content_Text());
                            float Cheat_AutoPot_Temp = Save_Manager.Data.UserData.Character.Cheats.autoPot;
                            Cheat_AutoPot_Temp = GUI.HorizontalSlider(new Rect(section1_x + content_margin, section1_y + 50, section1_content_w - (2 * content_margin), 20), Cheat_AutoPot_Temp, 0f, 255f);
                            string AutoPot_Text = (Cheat_AutoPot_Temp / 255 * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), AutoPot_Text, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.Cheats.Enable_AutoPot)))
                            { Save_Manager.Data.UserData.Character.Cheats.Enable_AutoPot = !Save_Manager.Data.UserData.Character.Cheats.Enable_AutoPot; }
                            Save_Manager.Data.UserData.Character.Cheats.autoPot = Cheat_AutoPot_Temp;
                            section1_y += 70 + content_margin;*/

                            section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Complete Main Quest", Mods.Character.Campaign.CompleteCampaign);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Complete Main Quest", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Campaign.CompleteCampaign(); }
                            section1_y += 40 + content_margin;*/

                            if (Mods.Character.Cheats.Masteries.IsMastered())
                            {
                                section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Reset Masterie", Mods.Character.Cheats.Masteries.Reset);

                                /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Reset Masterie", Managers.GUI_Manager.Styles.Content_Button()))
                                { Mods.Character.Cheats.Masteries.Reset(); }
                                section1_y += 40 + content_margin;*/
                            }
                            else
                            {
                                section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Choose Masterie", Mods.Character.Cheats.Masteries.ChooseNewOne);

                                /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Choose Masterie", Managers.GUI_Manager.Styles.Content_Button()))
                                { Mods.Character.Cheats.Masteries.ChooseNewOne(); }
                                section1_y += 40 + content_margin;*/
                            }

                            section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Add 99 All Runes", Mods.Character.Cheats.Materials.GetAllRunesX99);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Add 99 All Runes", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Cheats.Materials.GetAllRunesX99(); }
                            section1_y += 40 + content_margin;*/

                            section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Add 99 All Glyphs", Mods.Character.Cheats.Materials.GetAllGlyphsX99);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Add 99 All Glyphs", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Cheats.Materials.GetAllGlyphsX99(); }
                            section1_y += 40 + content_margin;*/

                            section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Add 10 All Affix", Mods.Character.Cheats.Materials.GetAllShardsX10);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Add 10 All Affix", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Cheats.Materials.GetAllShardsX10(); }
                            section1_y += 40 + content_margin;*/

                            section1_y += CustomControls.Function(section1_x, section1_y, section1_content_w, "Discover All Blessings", Mods.Character.Cheats.Blessings.DiscoverAllBlessings);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Discover All Blessings", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Cheats.Blessings.DiscoverAllBlessings(); }
                            section1_y += 40 + content_margin;*/

                            section1_y += CustomControls.Toggle(section1_x, section1_y, section1_content_w, "Allow Choosing Blessing in Panel", ref Save_Manager.Data.UserData.Character.Cheats.Enable_ChooseBlessingFromBlessingPanel);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Allow Choosing Blessing in Panel", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.Cheats.Enable_ChooseBlessingFromBlessingPanel)))
                            { Save_Manager.Data.UserData.Character.Cheats.Enable_ChooseBlessingFromBlessingPanel = !Save_Manager.Data.UserData.Character.Cheats.Enable_ChooseBlessingFromBlessingPanel; }
                            section1_y += 40 + content_margin;*/
                                                        
                            GUI.TextField(new Rect(section1_x, section1_y, section1_content_w, 20), "* Implicit values can be override with ItemData", Styles.Content_Infos());
                            section1_y += 20 + content_margin;

                            section1_y += CustomControls.Toggle(section1_x, section1_y, section1_content_w, "Unlock all Idols", ref Save_Manager.Data.UserData.CharacterSelectectionMenu.Enable_UnlockAllIdols);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Unlock all Idols", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.CharacterSelectectionMenu.Enable_UnlockAllIdols)))
                            { Save_Manager.Data.UserData.CharacterSelectectionMenu.Enable_UnlockAllIdols = !Save_Manager.Data.UserData.CharacterSelectectionMenu.Enable_UnlockAllIdols; }
                            section1_y += 40 + content_margin;*/

                            GUI.TextField(new Rect(section1_x, section1_y, section1_content_w, 20), "* You have to load your character with this option enable", Styles.Content_Infos());
                            section1_y += 20 + content_margin;

                            section1_y += CustomControls.Toggle(section1_x, section1_y, section1_content_w, "Allow Use Moded Item SubType", ref Save_Manager.Data.UserData.CharacterSelectectionMenu.UniqueSubTypeFromSave);

                            /*if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Allow Use Moded Item SubType", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.CharacterSelectectionMenu.UniqueSubTypeFromSave)))
                            { Save_Manager.Data.UserData.CharacterSelectectionMenu.UniqueSubTypeFromSave = !Save_Manager.Data.UserData.CharacterSelectectionMenu.UniqueSubTypeFromSave; }
                            section1_y += 40 + content_margin;*/

                            GUI.TextField(new Rect(section1_x, section1_y, section1_content_w, 20), "* You have to load your character with this option enable", Styles.Content_Infos());
                            
                            //Data
                            scene_x += Section_W + ui_margin;
                            float character_data_h = 690 + (17 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, Section_W, character_data_h), Textures.black);
                            float section2_x = scene_x + content_margin;
                            float section2_y = scene_y + content_margin;
                            float section2_w = Section_W - (2 * content_margin);
                            GUI.TextField(new Rect(section2_x, section2_y, section2_w, 40), "Data", Styles.Content_Title());
                            section2_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section2_x, section2_y, section2_w, (character_data_h - 40 - (3 * content_margin))), Textures.grey);
                            section2_x += content_margin;
                            section2_y += content_margin;
                            float section2_content_w = section2_w - (2 * content_margin);

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 60) / 100) - content_margin, 40), "Classe", Styles.Content_Text());
                            float Character_Data_Class_Temp = Mods.Character.Data.CharacterClass;
                            Character_Data_Class_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 50, section2_content_w - (2 * content_margin), 20), Character_Data_Class_Temp, 0f, Mods.Character.Data.GetClasseCount() - 1);
                            GUI.TextField(new Rect(section2_x + ((section2_content_w * 60) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 40) / 100), 40), Mods.Character.Data.GetClasseName(Mods.Character.Data.CharacterClass), Styles.ContentR_Text());                            
                            Mods.Character.Data.CharacterClass = (int)Character_Data_Class_Temp;
                            section2_y += 70 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, section2_content_w, 20), "* Reload your character after saving to take effect", Styles.Content_Infos());
                            section2_y += 20 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 60) / 100) - content_margin, 40), "Deaths", Styles.Content_Text());
                            float Character_Data_Death_Temp = Mods.Character.Data.Deaths;
                            Character_Data_Death_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 50, section2_content_w - (2 * content_margin), 20), Character_Data_Death_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section2_x + ((section2_content_w * 60) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 40) / 100), 40), Character_Data_Death_Temp.ToString(), Styles.ContentR_Text());
                            Mods.Character.Data.Deaths = (int)Character_Data_Death_Temp;
                            section2_y += 70 + content_margin;

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Died", ref Mods.Character.Data.Died);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Died", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.Died)))
                            { Mods.Character.Data.Died = !Mods.Character.Data.Died; }
                            section2_y += 40 + content_margin;*/

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Hardcore", ref Mods.Character.Data.Hardcore);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Hardcore", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.Hardcore)))
                            { Mods.Character.Data.Hardcore = !Mods.Character.Data.Hardcore; }
                            section2_y += 40 + content_margin;*/

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 60) / 100) - content_margin, 40), "Lantern Luminance", Styles.Content_Text());
                            float Character_Data_LanternLuminance_Temp = Mods.Character.Data.LanternLuminance;
                            Character_Data_LanternLuminance_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 50, section2_content_w - (2 * content_margin), 20), Character_Data_LanternLuminance_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section2_x + ((section2_content_w * 60) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 40) / 100), 40), Character_Data_LanternLuminance_Temp.ToString(), Styles.ContentR_Text());
                            Mods.Character.Data.LanternLuminance = Character_Data_LanternLuminance_Temp;
                            section2_y += 70 + content_margin;

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Masochist", ref Mods.Character.Data.Masochist);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Masochist", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.Masochist)))
                            { Mods.Character.Data.Masochist = !Mods.Character.Data.Masochist; }
                            section2_y += 40 + content_margin;*/

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 60) / 100) - content_margin, 40), "Monolith Depth", Styles.Content_Text());
                            float Character_Data_MonolithDepht_Temp = Mods.Character.Data.MonolithDepth;
                            Character_Data_MonolithDepht_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 50, section2_content_w - (2 * content_margin), 20), Character_Data_MonolithDepht_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section2_x + ((section2_content_w * 60) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 40) / 100), 40), Character_Data_MonolithDepht_Temp.ToString(), Styles.ContentR_Text());
                            Mods.Character.Data.MonolithDepth = (int)Character_Data_MonolithDepht_Temp;
                            section2_y += 70 + content_margin;

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Portal Unlocked", ref Mods.Character.Data.PortalUnlocked);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Portal Unlocked", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.PortalUnlocked)))
                            { Mods.Character.Data.PortalUnlocked = !Mods.Character.Data.PortalUnlocked; }
                            section2_y += 40 + content_margin;*/

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Solo Challenge", ref Mods.Character.Data.SoloChallenge);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Solo Challenge", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.SoloChallenge)))
                            { Mods.Character.Data.SoloChallenge = !Mods.Character.Data.SoloChallenge; }
                            section2_y += 40 + content_margin;*/

                            section2_y += CustomControls.Toggle(section2_x, section2_y, section2_content_w, "Solo Character Challenge", ref Mods.Character.Data.SoloCharacterChallenge);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Solo Character Challenge", Managers.GUI_Manager.Styles.Content_Enable_Button(Mods.Character.Data.SoloCharacterChallenge)))
                            { Mods.Character.Data.SoloCharacterChallenge = !Mods.Character.Data.SoloCharacterChallenge; }
                            section2_y += 40 + content_margin;*/

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 60) / 100) - content_margin, 40), "Soul Embers", Styles.Content_Text());
                            float Character_Data_SoulEmbers_Temp = Mods.Character.Data.SoulEmbers;
                            Character_Data_SoulEmbers_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 50, section2_content_w - (2 * content_margin), 20), Character_Data_SoulEmbers_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section2_x + ((section2_content_w * 60) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 40) / 100), 40), Character_Data_SoulEmbers_Temp.ToString(), Styles.ContentR_Text());
                            Mods.Character.Data.SoulEmbers = (int)Character_Data_SoulEmbers_Temp;
                            section2_y += 70 + content_margin;

                            section2_y += CustomControls.Function(section2_x, section2_y, section2_content_w, "Save", Mods.Character.Data.Save);

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Save", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Character.Data.Save(); }
                            section2_y += 40 + content_margin;*/

                            //Permanent Buffs
                            scene_x += Section_W + ui_margin;
                            float Skills_h = 950 + (17 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, Section_W, Skills_h), Textures.black);
                            float section3_x = scene_x + content_margin;
                            float section3_y = scene_y + content_margin;
                            float section3_w = Section_W - (2 * content_margin);
                            GUI.TextField(new Rect(section3_x, section3_y, section3_w, 40), "Permanent Buffs", Styles.Content_Title());
                            section3_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section3_x, section3_y, section3_w, (Skills_h - 40 - (3 * content_margin))), Textures.grey);
                            section3_x += content_margin;
                            section3_y += content_margin;
                            float section3_content_w = section3_w - (2 * content_margin);

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Move Speed",0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.MoveSpeed_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_MoveSpeed_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Move Speed", Styles.Content_Text());
                            float Perm_MoveSpeed_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.MoveSpeed_Buff_Value;
                            string Perm_MoveSpeed_str = "+ " + (Perm_MoveSpeed_Temp * 100) + " %";
                            Perm_MoveSpeed_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_MoveSpeed_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_MoveSpeed_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_MoveSpeed_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_MoveSpeed_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_MoveSpeed_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.MoveSpeed_Buff_Value = Perm_MoveSpeed_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Damage", 0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Damage_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Damage_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Damage", Styles.Content_Text());
                            float Perm_Damage_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Damage_Buff_Value;
                            string Perm_Damage_str = "+ " + (Perm_Damage_Temp * 100) + " %";
                            Perm_Damage_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Damage_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Damage_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Damage_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Damage_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Damage_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Damage_Buff_Value = Perm_Damage_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Attack Speed", 0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.AttackSpeed_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_AttackSpeed_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Attack Speed", Styles.Content_Text());
                            float Perm_AttackRate_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.AttackSpeed_Buff_Value;
                            string Perm_AttackRate_str = "+ " + (Perm_AttackRate_Temp * 100) + " %";
                            Perm_AttackRate_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_AttackRate_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_AttackRate_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_AttackSpeed_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_AttackSpeed_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_AttackSpeed_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.AttackSpeed_Buff_Value = Perm_AttackRate_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Casting Speed", 0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.CastSpeed_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CastSpeed_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Casting Speed", Styles.Content_Text());
                            float Perm_CastingRate_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.CastSpeed_Buff_Value;
                            string Perm_CastingRate_str = "+ " + (Perm_CastingRate_Temp * 100) + " %";
                            Perm_CastingRate_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_CastingRate_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_CastingRate_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CastSpeed_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CastSpeed_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CastSpeed_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.CastSpeed_Buff_Value = Perm_CastingRate_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Critical Chance", 0f, 0.95f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalChance_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalChance_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Critical Chance", Styles.Content_Text());
                            float Perm_CritChance_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalChance_Buff_Value;
                            string Perm_CritChance_str = "+ " + (Perm_CritChance_Temp * 100) + " %";
                            Perm_CritChance_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_CritChance_Temp, 0f, 0.95f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_CritChance_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalChance_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalChance_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalChance_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalChance_Buff_Value = Perm_CritChance_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Critical Multiplier", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalMultiplier_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalMultiplier_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Critical Multiplier", Styles.Content_Text());
                            float Perm_CritMulti_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalMultiplier_Buff_Value;
                            string Perm_CritMulti_str = "+ " + (Perm_CritMulti_Temp * 100) + " %";
                            Perm_CritMulti_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_CritMulti_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_CritMulti_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalMultiplier_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalMultiplier_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_CriticalMultiplier_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.CriticalMultiplier_Buff_Value = Perm_CritMulti_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Health Regen", 0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.HealthRegen_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_HealthRegen_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Health Regen", Styles.Content_Text());
                            float Perm_HealthRegen_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.HealthRegen_Buff_Value;
                            string Perm_HealthRegen_str = "+ " + (Perm_HealthRegen_Temp * 100) + " %";
                            Perm_HealthRegen_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_HealthRegen_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_HealthRegen_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_HealthRegen_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_HealthRegen_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_HealthRegen_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.HealthRegen_Buff_Value = Perm_HealthRegen_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Mana Regen", 0f, 10f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.ManaRegen_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_ManaRegen_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Mana Regen", Styles.Content_Text());
                            float Perm_ManaRegen_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.ManaRegen_Buff_Value;
                            string Perm_ManaRegen_str = "+ " + (Perm_ManaRegen_Temp * 100) + " %";
                            Perm_ManaRegen_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_ManaRegen_Temp, 0f, 10f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_ManaRegen_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_ManaRegen_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_ManaRegen_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_ManaRegen_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.ManaRegen_Buff_Value = Perm_ManaRegen_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Strength", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Str_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Str_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Strength", Styles.Content_Text());
                            float Perm_Str_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Str_Buff_Value;
                            string Perm_Str_str = "+ " + (int)Perm_Str_Temp;
                            Perm_Str_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Str_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Str_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Str_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Str_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Str_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Str_Buff_Value = Perm_Str_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Intelligence", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Int_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Int_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Intelligence", Styles.Content_Text());
                            float Perm_Int_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Int_Buff_Value;
                            string Perm_Int_str = "+ " + (int)Perm_Int_Temp;
                            Perm_Int_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Int_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Int_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Int_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Int_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Int_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Int_Buff_Value = Perm_Int_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Dexterity", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Dex_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Dex_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Dexterity", Styles.Content_Text());
                            float Perm_Dex_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Dex_Buff_Value;
                            string Perm_Dex_str = "+ " + (int)Perm_Dex_Temp;
                            Perm_Dex_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Dex_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Dex_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Dex_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Dex_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Dex_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Dex_Buff_Value = Perm_Dex_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Vitality", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Vit_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Vit_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Vitality", Styles.Content_Text());
                            float Perm_Vit_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Vit_Buff_Value;
                            string Perm_Vit_str = "+ " + (int)Perm_Vit_Temp;
                            Perm_Vit_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Vit_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Vit_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Vit_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Vit_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Vit_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Vit_Buff_Value = Perm_Vit_Temp;
                            section3_y += 70 + content_margin;*/

                            section3_y += CustomControls.Toggle_FloatPercent(section3_x, section3_y, section3_content_w, "Attunement", 0f, 255f, true, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Att_Buff_Value, ref Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Att_Buff);

                            /*GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Attunement", Styles.Content_Text());
                            float Perm_Att_Temp = Save_Manager.Data.UserData.Character.PermanentBuffs.Att_Buff_Value;
                            string Perm_Att_str = "+ " + (int)Perm_Att_Temp;
                            Perm_Att_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Perm_Att_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Perm_Att_str, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Att_Buff)))
                            { Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Att_Buff = !Save_Manager.Data.UserData.Character.PermanentBuffs.Enable_Att_Buff; }
                            Save_Manager.Data.UserData.Character.PermanentBuffs.Att_Buff_Value = Perm_Att_Temp;
                            section3_y += 70 + content_margin;*/
                        }
                        else if (BaseMenu.Items.Show)
                        {
                            float section_w = (mods_content_width - (2 * ui_margin)) / 3;
                            float scene_x = mods_content_x;
                            float scene_y = mods_content_y;

                            //ItemData
                            float items_data_h = 890 + (5 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, items_data_h), Textures.black);
                            float section1_x = scene_x + content_margin;
                            float section1_y = scene_y + content_margin;
                            float section1_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section1_x, section1_y, section1_w, 40), "Item Data", Styles.Content_Title());
                            section1_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section1_x, section1_y, section1_w, (items_data_h - 40 - (3 * content_margin))), Textures.grey);
                            section1_x += content_margin;
                            section1_y += content_margin;
                            float section1_content_w = section1_w - (2 * content_margin);
                                                        
                            float force_w = (section1_content_w - (2 * content_margin)) / 3;
                            if (GUI.Button(new Rect(section1_x, section1_y, force_w, 40), "Unique", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.ForceUnique)))
                            {
                                Save_Manager.Data.UserData.Items.ItemData.ForceSet = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceLegendary = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceUnique = !Save_Manager.Data.UserData.Items.ItemData.ForceUnique;
                            }
                            if (GUI.Button(new Rect(section1_x + force_w + content_margin, section1_y, force_w, 40), "Set", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.ForceSet)))
                            {
                                Save_Manager.Data.UserData.Items.ItemData.ForceUnique = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceLegendary = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceSet = !Save_Manager.Data.UserData.Items.ItemData.ForceSet;
                            }
                            if (GUI.Button(new Rect(section1_x + (2 * (force_w + content_margin)), section1_y, force_w, 40), "Legendary", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.ForceLegendary)))
                            {
                                Save_Manager.Data.UserData.Items.ItemData.ForceSet = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceUnique = false;
                                Save_Manager.Data.UserData.Items.ItemData.ForceLegendary = !Save_Manager.Data.UserData.Items.ItemData.ForceLegendary;
                            }
                            section1_y += 40; // + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Implicits Values", Styles.Content_Text());
                            float ItemData_Implicits_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_Implicit;
                            ItemData_Implicits_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_Implicits_Temp, 0f, 255f);                            
                            string implicit_str = (int)((ItemData_Implicits_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), implicit_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_Implicit)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_Implicit = !Save_Manager.Data.UserData.Items.ItemData.Enable_Implicit; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_Implicit = (byte)ItemData_Implicits_Temp;
                            section1_y += 70; // + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Forgin Potencial", Styles.Content_Text());
                            float ItemData_ForginPotencial_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_ForgingPotencial;
                            ItemData_ForginPotencial_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_ForginPotencial_Temp, 0f, 255f);
                            string forginpotencial_str = System.Convert.ToString((int)ItemData_ForginPotencial_Temp);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), forginpotencial_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_ForgingPotencial)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_ForgingPotencial = !Save_Manager.Data.UserData.Items.ItemData.Enable_ForgingPotencial; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_ForgingPotencial = (byte)ItemData_ForginPotencial_Temp;
                            section1_y += 70;//+ content_margin;

                            
                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Force Seal", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Force_Seal)))
                            { Save_Manager.Data.UserData.Items.ItemData.Force_Seal = !Save_Manager.Data.UserData.Items.ItemData.Force_Seal; }
                            section1_y += 40;// + content_margin;
                                                        

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Seal Tier", Styles.Content_Text());
                            float ItemData_SealTier_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_SealTier;
                            ItemData_SealTier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_SealTier_Temp, 1f, 7f);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), ItemData_SealTier_Temp.ToString(), Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_SealTier)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_SealTier = !Save_Manager.Data.UserData.Items.ItemData.Enable_SealTier; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_SealTier = (byte)ItemData_SealTier_Temp;
                            section1_y += 70;// + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Seal Value", Styles.Content_Text());
                            float ItemData_SealValue_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_SealValue;
                            ItemData_SealValue_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_SealValue_Temp, 0f, 255f);
                            string SealValue_str = (int)((ItemData_SealValue_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), SealValue_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_SealValue)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_SealValue = !Save_Manager.Data.UserData.Items.ItemData.Enable_SealValue; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_SealValue = (byte)ItemData_SealValue_Temp;
                            section1_y += 70; // + content_margin;
                            
                            //Nb Affixs
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Minimum Affix(s)", Styles.Content_Text());
                            float ItemData_MinAffixs_Temp = Save_Manager.Data.UserData.Items.ItemData.Min_affixs;
                            ItemData_MinAffixs_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_MinAffixs_Temp, 0f, 20f);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 80) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), ItemData_MinAffixs_Temp.ToString(), Styles.ContentR_Text());                            
                            Save_Manager.Data.UserData.Items.ItemData.Min_affixs = (int)ItemData_MinAffixs_Temp;
                            if (Save_Manager.Data.UserData.Items.ItemData.Min_affixs > Save_Manager.Data.UserData.Items.ItemData.Max_affixs)
                            { Save_Manager.Data.UserData.Items.ItemData.Max_affixs = Save_Manager.Data.UserData.Items.ItemData.Min_affixs; }
                            section1_y += 70;
                            
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Maximum Affix(s)", Styles.Content_Text());
                            float ItemData_MaxAffixs_Temp = Save_Manager.Data.UserData.Items.ItemData.Max_affixs;
                            ItemData_MaxAffixs_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_MaxAffixs_Temp, 0f, 20f);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 80) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), ItemData_MaxAffixs_Temp.ToString(), Styles.ContentR_Text());
                            Save_Manager.Data.UserData.Items.ItemData.Max_affixs = (int)ItemData_MaxAffixs_Temp;
                            if (Save_Manager.Data.UserData.Items.ItemData.Max_affixs < Save_Manager.Data.UserData.Items.ItemData.Min_affixs)
                            { Save_Manager.Data.UserData.Items.ItemData.Min_affixs = Save_Manager.Data.UserData.Items.ItemData.Max_affixs; }
                            section1_y += 70;                            

                            //
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Affix Tiers", Styles.Content_Text());
                            float ItemData_AffixTier_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_AffixTier;
                            ItemData_AffixTier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_AffixTier_Temp, 1f, 7f);
                            GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), ItemData_AffixTier_Temp.ToString(), Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsTier)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsTier = !Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsTier; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_AffixTier = (byte)ItemData_AffixTier_Temp;
                            section1_y += 70;// + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Affix Values", Styles.Content_Text());
                            float ItemData_AffixValue_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_AffixValue;
                            ItemData_AffixValue_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_AffixValue_Temp, 0f, 255f);
                            string AffixValue_str = (int)((ItemData_AffixValue_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), AffixValue_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsValue)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsValue = !Save_Manager.Data.UserData.Items.ItemData.Enable_AffixsValue; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_AffixValue = (byte)ItemData_AffixValue_Temp;
                            section1_y += 70;// + content_margin;
                            
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Unique Mods", Styles.Content_Text());
                            float ItemData_UniqueMods_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_UniqueMod;
                            ItemData_UniqueMods_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_UniqueMods_Temp, 0f, 255f);
                            string UniqueMods_str = (int)((ItemData_UniqueMods_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), UniqueMods_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_UniqueMod)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_UniqueMod = !Save_Manager.Data.UserData.Items.ItemData.Enable_UniqueMod; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_UniqueMod = (byte)ItemData_UniqueMods_Temp;
                            section1_y += 70;// + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Legendary Potencial", Styles.Content_Text());
                            float ItemData_LegendaryPotencial_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_Legendary_Potencial;
                            ItemData_LegendaryPotencial_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_LegendaryPotencial_Temp, 0f, 255f);
                            string LegendaryPotencial_str = System.Convert.ToString((int)ItemData_LegendaryPotencial_Temp);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), LegendaryPotencial_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_LegendayPotencial)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_LegendayPotencial = !Save_Manager.Data.UserData.Items.ItemData.Enable_LegendayPotencial; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_Legendary_Potencial = (byte)ItemData_LegendaryPotencial_Temp;
                            section1_y += 70;// + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Weaver Will", Styles.Content_Text());
                            float ItemData_WeaverWill_Temp = Save_Manager.Data.UserData.Items.ItemData.Roll_Weaver_Will;
                            ItemData_WeaverWill_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemData_WeaverWill_Temp, 0f, 255f);
                            string WeaverWill_str = System.Convert.ToString((int)ItemData_WeaverWill_Temp);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), WeaverWill_str, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Enable_WeaverWill)))
                            { Save_Manager.Data.UserData.Items.ItemData.Enable_WeaverWill = !Save_Manager.Data.UserData.Items.ItemData.Enable_WeaverWill; }
                            Save_Manager.Data.UserData.Items.ItemData.Roll_Weaver_Will = (byte)ItemData_WeaverWill_Temp;
                            section1_y += 70;// + content_margin;
                            
                            /*
                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Allow  drop up to 6 Affixes", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.ItemData.Allow_6_Affixes)))
                            { Save_Manager.Data.UserData.Items.ItemData.Allow_6_Affixes = !Save_Manager.Data.UserData.Items.ItemData.Allow_6_Affixes; }
                            section1_y += 40;// + content_margin;                                                        
                            */

                            //AutoPickup
                            scene_x += section_w + ui_margin;
                            float Items_AutoPickup_h = 540 + (17 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, Items_AutoPickup_h), Textures.black);
                            float section2_x = scene_x + content_margin;
                            float section2_y = scene_y + content_margin;
                            float section2_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section2_x, section2_y, section2_w, 40), "Pickup", Styles.Content_Title());
                            section2_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section2_x, section2_y, section2_w, (Items_AutoPickup_h - 40 - (3 * content_margin))), Textures.grey);
                            section2_x += content_margin;
                            section2_y += content_margin;
                            float section2_content_w = section2_w - (2 * content_margin);

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Gold", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Gold)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Gold = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Gold; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Keys", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Key)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Key = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Key; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Pots", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Pots)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Pots = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Pots; }
                            section2_y += 40 + content_margin;

                            /*if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Unique & Set", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_UniqueAndSet)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_UniqueAndSet = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_UniqueAndSet; }
                            section2_y += 40 + content_margin;*/

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup XpTome", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_XpTome)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_XpTome = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_XpTome; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Materials", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Materials)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Materials = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Materials; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "Enable Range Pickup", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Pickup.Enable_RangePickup)))
                            { Save_Manager.Data.UserData.Items.Pickup.Enable_RangePickup = !Save_Manager.Data.UserData.Items.Pickup.Enable_RangePickup; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoPickup Filtered Items", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Filter)))
                            { Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Filter = !Save_Manager.Data.UserData.Items.AutoPickup.AutoPickup_Filter; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "Auto Sell", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Pickup.RemoveItemNotInFilter)))
                            { Save_Manager.Data.UserData.Items.Pickup.RemoveItemNotInFilter = !Save_Manager.Data.UserData.Items.Pickup.RemoveItemNotInFilter; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "Hide Materials Notifications", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.DropNotification.Hide_materials_notifications)))
                            { Save_Manager.Data.UserData.Items.DropNotification.Hide_materials_notifications = !Save_Manager.Data.UserData.Items.DropNotification.Hide_materials_notifications; }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoStore Materials on Inventory Open", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WhenOpeningInventory)))
                            {
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WithTimer = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WhenOpeningInventory = !Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WhenOpeningInventory;
                            }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoStore Materials all 10sec", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WithTimer)))
                            {
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WhenOpeningInventory = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WithTimer = !Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WithTimer;
                            }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 40), "AutoStore Materials after Drop", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials)))
                            {
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WithTimer = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials_WhenOpeningInventory = false;
                                Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials = !Save_Manager.Data.UserData.Items.AutoPickup.AutoStore_Materials;
                            }
                            section2_y += 40 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, section2_w - (2 * content_margin), 20), "* AutoStore Materials after Drop Need AutoPickup Materials Enable to work", Styles.Content_Infos());
                            section2_y += 20 + content_margin;

                            //Craft
                            scene_x += section_w + ui_margin;
                            float items_craft_h = 230 + (9 * content_margin);
                            float section6_y = scene_y;
                            GUI.DrawTexture(new Rect(scene_x, section6_y, section_w, items_craft_h), Textures.black);
                            float section6_x = scene_x + content_margin;
                            section6_y += content_margin;
                            float section6_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section6_x, section6_y, section6_w, 40), "Craft", Styles.Content_Title());
                            section6_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section6_x, section6_y, section6_w, (items_craft_h - 40 - (3 * content_margin))), Textures.grey);
                            section6_x += content_margin;
                            section6_y += content_margin;
                            float section6_content_w = section6_w - (2 * content_margin);

                            GUI.TextField(new Rect(section6_x + content_margin, section6_y, ((section6_content_w * 40) / 100) - content_margin, 40), "Add Forgin Potencial", Styles.Content_Text());
                            float AddForginpotencial_Temp = Save_Manager.Data.UserData.Items.Craft.AddForginpotencial;
                            AddForginpotencial_Temp = GUI.HorizontalSlider(new Rect(section6_x + (2 * content_margin), section6_y + 50, section6_content_w - (2 * content_margin), 20), AddForginpotencial_Temp, 0f, 255f);
                            string AddForginpotencial_Text = GUI.TextArea(new Rect(section6_x + ((section6_content_w * 40) / 100) - (2 * content_margin), section6_y, ((section1_content_w * 20) / 100), 40), AddForginpotencial_Temp.ToString(), Styles.Content_TextArea());
                            try { AddForginpotencial_Temp = float.Parse(AddForginpotencial_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section6_x + ((section6_content_w * 60) / 100), section6_y + content_margin, ((section6_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Craft.Enable_AddForginpotencial)))
                            { Save_Manager.Data.UserData.Items.Craft.Enable_AddForginpotencial = !Save_Manager.Data.UserData.Items.Craft.Enable_AddForginpotencial; }
                            Save_Manager.Data.UserData.Items.Craft.AddForginpotencial = (byte)AddForginpotencial_Temp;
                            section6_y += 70 + content_margin;

                            GUI.TextField(new Rect(section6_x, section6_y, section6_w - (2 * content_margin), 20), "* Move an item in Forge", Styles.Content_Infos());
                            section6_y += 20 + content_margin;

                            if (GUI.Button(new Rect(section6_x, section6_y, section6_w - (2 * content_margin), 40), "No Forgin Potencial Cost", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Craft.NoForgingPotentialCost)))
                            { Save_Manager.Data.UserData.Items.Craft.NoForgingPotentialCost = !Save_Manager.Data.UserData.Items.Craft.NoForgingPotentialCost; }
                            section6_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section6_x, section6_y, section6_w - (2 * content_margin), 40), "Don't Check Capability", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Craft.DontChekCapability)))
                            { Save_Manager.Data.UserData.Items.Craft.DontChekCapability = !Save_Manager.Data.UserData.Items.Craft.DontChekCapability; }
                            section6_y += 40 + content_margin;

                            GUI.TextField(new Rect(section6_x, section6_y, section6_w - (2 * content_margin), 20), "* Implicits and affixs (values) can be override using Runes and ItemData", Styles.Content_Infos());

                            //Remove Req
                            float removereq_h = 280 + (11 * content_margin);
                            float section7_y = scene_y + items_craft_h + ui_margin;
                            GUI.DrawTexture(new Rect(scene_x, section7_y, section_w, removereq_h), Textures.black);
                            float section7_x = scene_x + content_margin;
                            section7_y += content_margin;
                            float section7_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section7_x, section7_y, section7_w, 40), "Remove Requirement", Styles.Content_Title());
                            section7_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section7_x, section7_y, section7_w, (removereq_h - 40 - (3 * content_margin))), Textures.grey);
                            section7_x += content_margin;
                            section7_y += content_margin;
                            float section7_content_w = section7_w - (2 * content_margin);

                            if (GUI.Button(new Rect(section7_x, section7_y, section7_content_w, 40), "Level", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq)))
                            { Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_LevelReq; }
                            section7_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section7_x, section7_y, section7_content_w, 40), "Class", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq)))
                            { Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_ClassReq; }
                            section7_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section7_x, section7_y, section7_content_w, 40), "SubClass", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq)))
                            { Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_SubClassReq; }
                            section7_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section7_x, section7_y, section7_content_w, 40), "Set", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req)))
                            { Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req = !Save_Manager.Data.UserData.Items.RemoveReq.Remove_set_req; }
                            section7_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section7_x, section7_y, section7_content_w, 40), "All Affixs in all slots", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots)))
                            { Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots = !Save_Manager.Data.UserData.Items.RemoveReq.Enable_AllAffixsInAllSlots; }
                            section7_y += 40 + content_margin;

                            GUI.TextField(new Rect(section7_x, section7_y, section7_w - (2 * content_margin), 20), "* Need to be activate before loading your Character", Styles.Content_Infos());
                            section7_y += 20 + content_margin;
                            
                            GUI.TextField(new Rect(section7_x, section7_y, section7_w - (2 * content_margin), 20), "** All Affixs in all slots cause Lag in forge and when drop", Styles.Content_Infos());
                            section7_y += 20 + content_margin;
                        }
                        else if (BaseMenu.Scenes.Show)
                        {
                            float section_w = (mods_content_width - (2 * ui_margin)) / 3;
                            float scene_x = mods_content_x;
                            float scene_y = mods_content_y;

                            //Options
                            float option_h = 590 + (15 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, option_h), Textures.black);
                            float section1_x = scene_x + content_margin;
                            float section1_y = scene_y + content_margin;
                            float section1_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section1_x, section1_y, section1_w, 40), "Options", Styles.Content_Title());
                            section1_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section1_x, section1_y, section1_w, (option_h - 40 - (3 * content_margin))), Textures.grey);

                            section1_y += (2 * content_margin);
                            float section1_content_w = section1_w - (2 * content_margin);
                                                        
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Density Multiplier", Styles.Content_Text());
                            float Density_Multiplier_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Scenes_Density_Multiplier;
                            Density_Multiplier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), Density_Multiplier_Temp, 0f, 255f);
                            string Density_Multiplier_Text = GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Density_Multiplier_Temp.ToString(), Styles.Content_TextArea());
                            try { Density_Multiplier_Temp = float.Parse(Density_Multiplier_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Density_Multiplier)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Density_Multiplier = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Density_Multiplier; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Scenes_Density_Multiplier = Density_Multiplier_Temp;
                            section1_y += 70 + content_margin;
                                                        
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Experience Multiplier", Styles.Content_Text());
                            float Experience_Multiplier_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Scenes_Exp_Multiplier;
                            Experience_Multiplier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), Experience_Multiplier_Temp, 0f, 255f);
                            string Experience_Multiplier_Text = GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Experience_Multiplier_Temp.ToString(), Styles.Content_TextArea());
                            try { Experience_Multiplier_Temp = float.Parse(Experience_Multiplier_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Exp_Multiplier)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Exp_Multiplier = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Scenes_Exp_Multiplier; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Scenes_Exp_Multiplier = Experience_Multiplier_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x, section1_y, section1_content_w, 20), "* All options above need a scenes Change to take effect", Styles.Content_Infos());
                            section1_y += 20 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Mobs Items Drop Multiplier", Styles.Content_Text());
                            float Items_Multiplier_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_ItemsMultiplier;
                            Items_Multiplier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), Items_Multiplier_Temp, 0f, 255f);
                            string Items_Multiplier_Text = GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Items_Multiplier_Temp.ToString(), Styles.Content_TextArea());
                            try { Items_Multiplier_Temp = float.Parse(Items_Multiplier_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsMultiplier)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsMultiplier = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsMultiplier; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_ItemsMultiplier = Items_Multiplier_Temp;
                            section1_y += 70 + content_margin;
                            
                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Mobs Items Drop Chance", Styles.Content_Text());
                            float ItemsDropChance_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_ItemsDropChance;
                            ItemsDropChance_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), ItemsDropChance_Temp, 0f, 255f);
                            string itemdropchance = ((ItemsDropChance_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), itemdropchance, Styles.ContentR_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsDropChance)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsDropChance = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_ItemsDropChance; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_ItemsDropChance = ItemsDropChance_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Mobs Gold Drop Multiplier", Styles.Content_Text());
                            float Gold_Multiplier_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_GoldMultiplier;
                            Gold_Multiplier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), Gold_Multiplier_Temp, 0f, 255f);
                            string Gold_Multiplier_Text = GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Gold_Multiplier_Temp.ToString(), Styles.Content_TextArea());
                            try { Gold_Multiplier_Temp = float.Parse(Gold_Multiplier_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldMultiplier)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldMultiplier = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldMultiplier; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_GoldMultiplier = Gold_Multiplier_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Mobs Gold Drop Chance", Styles.Content_Text());
                            float GoldDropChance_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_GoldDropChance;
                            GoldDropChance_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), GoldDropChance_Temp, 0f, 255f);
                            string GoldDropChance = ((GoldDropChance_Temp / 255) * 100) + " %";
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), GoldDropChance, Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldDropChance)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldDropChance = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mobs_GoldDropChance; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Mobs_GoldDropChance = GoldDropChance_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Mobs Exp Multiplier", Styles.Content_Text());
                            float Mobs_Exp_Multiplier_Temp = Save_Manager.Data.UserData.Scene.Scene_Options.Mods_ExpMultiplier;
                            Mobs_Exp_Multiplier_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), Mobs_Exp_Multiplier_Temp, 0f, 255f);
                            GUI.TextArea(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), Mobs_Exp_Multiplier_Temp.ToString(), Styles.Content_TextArea());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mods_ExpMultiplier)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mods_ExpMultiplier = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Mods_ExpMultiplier; }
                            Save_Manager.Data.UserData.Scene.Scene_Options.Mods_ExpMultiplier = (long)Mobs_Exp_Multiplier_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 60) / 100) - content_margin, 40), "Waypoints Unlock", Styles.Content_Text());                            
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Waypoints_Unlock)))
                            { Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Waypoints_Unlock = !Save_Manager.Data.UserData.Scene.Scene_Options.Enable_Waypoints_Unlock; }
                            section1_y += 40 + content_margin;

                            //Camera
                            scene_x += section_w + ui_margin;                            
                            float camera_h = 670 + (15 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, camera_h), Textures.black);
                            float section2_x = scene_x + content_margin;                            
                            float section2_y = scene_y + content_margin;
                            float section2_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section2_x, section2_y, section2_w, 40), "Camera", Styles.Content_Title());
                            section2_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section2_x, section2_y, section2_w, (camera_h - 40 - (3 * content_margin))), Textures.grey);
                            section2_x += content_margin;
                            section2_y += content_margin;

                            float section2_content_w = section2_w - (2 * content_margin);
                            
                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Zoom Minimum", Styles.Content_Text());
                            float Camera_ZoomMin_Temp = Save_Manager.Data.UserData.Scene.Camera.ZoomMin;
                            Camera_ZoomMin_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_ZoomMin_Temp, -255f, 0);
                            string Camera_ZoomMin_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_ZoomMin_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_ZoomMin_Temp = float.Parse(Camera_ZoomMin_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.ZoomMin = Camera_ZoomMin_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Zoom Per Scroll", Styles.Content_Text());
                            float Camera_ZoomPerScroll_Temp = Save_Manager.Data.UserData.Scene.Camera.ZoomPerScroll;
                            Camera_ZoomPerScroll_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_ZoomPerScroll_Temp, 0, 1f);
                            string Camera_ZoomPerScroll_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_ZoomPerScroll_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_ZoomPerScroll_Temp = float.Parse(Camera_ZoomPerScroll_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.ZoomPerScroll = Camera_ZoomPerScroll_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Zoom Speed", Styles.Content_Text());
                            float Camera_ZoomSpeed_Temp = Save_Manager.Data.UserData.Scene.Camera.ZoomSpeed;
                            Camera_ZoomSpeed_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_ZoomSpeed_Temp, 0, 255f);
                            string Camera_ZoomSpeed_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_ZoomSpeed_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_ZoomSpeed_Temp = float.Parse(Camera_ZoomSpeed_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.ZoomSpeed = Camera_ZoomSpeed_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Default Rotation", Styles.Content_Text());
                            float Camera_DefaultRotation_Temp = Save_Manager.Data.UserData.Scene.Camera.Rotation;
                            Camera_DefaultRotation_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_DefaultRotation_Temp, -255f, 255f);
                            string Camera_DefaultRotation_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_DefaultRotation_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_DefaultRotation_Temp = float.Parse(Camera_DefaultRotation_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.Rotation = Camera_DefaultRotation_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Offset Minimum", Styles.Content_Text());
                            float Camera_OffsetMin_Temp = Save_Manager.Data.UserData.Scene.Camera.OffsetMin;
                            Camera_OffsetMin_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_OffsetMin_Temp, -255f, Save_Manager.Data.UserData.Scene.Camera.OffsetMax);
                            string Camera_OffsetMin_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_OffsetMin_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_OffsetMin_Temp = float.Parse(Camera_OffsetMin_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.OffsetMin = Camera_OffsetMin_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Offset Maximum", Styles.Content_Text());
                            float Camera_OffsetMax_Temp = Save_Manager.Data.UserData.Scene.Camera.OffsetMax;
                            Camera_OffsetMax_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_OffsetMax_Temp, Save_Manager.Data.UserData.Scene.Camera.OffsetMin, 255f);
                            string Camera_OffsetMax_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_OffsetMax_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_OffsetMax_Temp = float.Parse(Camera_OffsetMax_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.OffsetMax = Camera_OffsetMax_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Angle Minimum", Styles.Content_Text());
                            float Camera_AngleMin_Temp = Save_Manager.Data.UserData.Scene.Camera.AngleMin;
                            Camera_AngleMin_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_AngleMin_Temp, -255f, Save_Manager.Data.UserData.Scene.Camera.AngleMax);
                            string Camera_AngleMin_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_AngleMin_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_AngleMin_Temp = float.Parse(Camera_AngleMin_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.AngleMin = Camera_AngleMin_Temp;
                            section2_y += 60 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, ((section2_content_w * 70) / 100), 40), "Angle Maximum", Styles.Content_Text());
                            float Camera_AngleMax_Temp = Save_Manager.Data.UserData.Scene.Camera.AngleMax;
                            Camera_AngleMax_Temp = GUI.HorizontalSlider(new Rect(section2_x + content_margin, section2_y + 40, section2_content_w - (2 * content_margin), 20), Camera_AngleMax_Temp, Save_Manager.Data.UserData.Scene.Camera.AngleMin, 255f);
                            string Camera_AngleMax_Text = GUI.TextArea(new Rect(section2_x + ((section2_content_w * 70) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 30) / 100), 40), Camera_AngleMax_Temp.ToString(), Styles.Content_TextArea());
                            try { Camera_AngleMax_Temp = float.Parse(Camera_AngleMax_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            Save_Manager.Data.UserData.Scene.Camera.AngleMax = Camera_AngleMax_Temp;
                            section2_y += 60 + content_margin;
                            
                            if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Update Camera", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Scenes.Camera.Update(); }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Reset to Default", Managers.GUI_Manager.Styles.Content_Button()))
                            { Mods.Scenes.Camera.ResetToDefault(); }
                            section2_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Load Camera on Start", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Camera.Enable_OnLoad)))
                            { Save_Manager.Data.UserData.Scene.Camera.Enable_OnLoad = !Save_Manager.Data.UserData.Scene.Camera.Enable_OnLoad; }
                            section2_y += 40 + content_margin;

                            GUI.TextField(new Rect(section2_x, section2_y, section2_content_w, 20), "* Enable to force game to use this camera settings OnLoad", Styles.Content_Infos());
                                 
                            //Monoliths
                            scene_x += section_w + ui_margin;
                            float monoliths_h = 540 + (14 * content_margin);
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, monoliths_h), Textures.black);
                            float section3_x = scene_x + content_margin;
                            float section3_y = scene_y + content_margin;
                            float section3_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section3_x, section3_y, section3_w, 40), "Monoliths", Styles.Content_Title());
                            section3_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section3_x, section3_y, section3_w, (monoliths_h - 40 - (3 * content_margin))), Textures.grey);
                            section3_x += content_margin;
                            section3_y += content_margin;
                            float section3_content_w = section3_w - (2 * content_margin);
                                 
                            GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Max Stability", Styles.Content_Text());
                            float Stability_Temp = Save_Manager.Data.UserData.Scene.Monoliths.MaxStability;
                            Stability_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Stability_Temp, 0f, 255f);
                            string Stability_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Stability_Temp.ToString(), Styles.Content_TextArea());
                            try { Stability_Temp = float.Parse(Stability_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability; }
                            Save_Manager.Data.UserData.Scene.Monoliths.MaxStability = (int)Stability_Temp;
                            section3_y += 70 + content_margin;

                            GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Enenmy Density", Styles.Content_Text());
                            float EnemyDensity_Temp = Save_Manager.Data.UserData.Scene.Monoliths.EnemyDensity;
                            EnemyDensity_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), EnemyDensity_Temp, 0f, 255f);
                            string EnemyDensity_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), EnemyDensity_Temp.ToString(), Styles.Content_TextArea());
                            try { EnemyDensity_Temp = float.Parse(EnemyDensity_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnemyDensity)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnemyDensity = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnemyDensity; }
                            Save_Manager.Data.UserData.Scene.Monoliths.EnemyDensity = (int)EnemyDensity_Temp;
                            section3_y += 70 + content_margin;

                            GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Enemy Defeat", Styles.Content_Text());
                            float Defeat_Temp = Save_Manager.Data.UserData.Scene.Monoliths.EnnemiesDefeat;
                            Defeat_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), Defeat_Temp, 0f, 255f);
                            string Defeat_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), Defeat_Temp.ToString(), Styles.Content_TextArea());
                            try { Defeat_Temp = float.Parse(Defeat_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnnemiesDefeat)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnnemiesDefeat = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_EnnemiesDefeat; }
                            Save_Manager.Data.UserData.Scene.Monoliths.EnnemiesDefeat = (int)Defeat_Temp;
                            section3_y += 70 + content_margin;

                            GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 40) / 100) - content_margin, 40), "Blessings Slots", Styles.Content_Text());
                            float MonolithUnlockSlots_Temp = Save_Manager.Data.UserData.Scene.Monoliths.UnlockSlots;
                            MonolithUnlockSlots_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 50, section3_content_w - (2 * content_margin), 20), MonolithUnlockSlots_Temp, 3f, 5f);
                            string MonolithUnlockSlots_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 20) / 100), 40), MonolithUnlockSlots_Temp.ToString(), Styles.Content_TextArea());
                            try { MonolithUnlockSlots_Temp = float.Parse(MonolithUnlockSlots_Text, CultureInfo.InvariantCulture.NumberFormat); }
                            catch { }
                            if (GUI.Button(new Rect(section3_x + ((section3_content_w * 60) / 100), section3_y, ((section3_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_UnlockSlots)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_UnlockSlots = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_UnlockSlots; }
                            Save_Manager.Data.UserData.Scene.Monoliths.UnlockSlots = (int)MonolithUnlockSlots_Temp;
                            section3_y += 70 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Max Stability OnStart", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStart)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStart = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStart; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Max Stability OnStabilityChanged", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStabilityChanged)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStabilityChanged = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_MaxStability_OnStabilityChanged; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Objective Reveal", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_ObjectiveReveal)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_ObjectiveReveal = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_ObjectiveReveal; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Complete Objective", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_Complete_Objective)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_Complete_Objective = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_Complete_Objective; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "No lost when Die", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Monoliths.Enable_NoLostWhenDie)))
                            { Save_Manager.Data.UserData.Scene.Monoliths.Enable_NoLostWhenDie = !Save_Manager.Data.UserData.Scene.Monoliths.Enable_NoLostWhenDie; }
                            section3_y += 40 + content_margin;

                            GUI.TextField(new Rect(section3_x, section3_y, section3_content_w, 20), "* Setup before entering Monolith", Styles.Content_Infos());

                            //Dungeons                            
                            float section4_x = section1_x - content_margin;
                            float section4_y = scene_y + option_h + ui_margin;
                            float dungeons_h = 80 + (5 * content_margin);
                            GUI.DrawTexture(new Rect(section4_x, section4_y, section_w, dungeons_h), Textures.black);
                            section4_x += content_margin;
                            section4_y += content_margin;
                            float section4_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section4_x, section4_y, section4_w, 40), "Dungeons", Styles.Content_Title());
                            section4_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section4_x, section4_y, section1_w, (dungeons_h - 40 - (3 * content_margin))), Textures.grey);
                            section4_x += content_margin;
                            section4_y += content_margin;

                            if (GUI.Button(new Rect(section4_x, section4_y, section4_w - (2 * content_margin), 40), "Entering Without Key", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Dungeons.EnteringWithoutKey)))
                            { Save_Manager.Data.UserData.Scene.Dungeons.EnteringWithoutKey = !Save_Manager.Data.UserData.Scene.Dungeons.EnteringWithoutKey; }
                            section4_y += 40 + content_margin;

                            //Minimap
                            float minimap_h = 120 + (6 * content_margin);
                            float section5_x = section3_x - ( 2 *content_margin);
                            float section5_y = scene_y + monoliths_h + ui_margin;
                            GUI.DrawTexture(new Rect(section5_x, section5_y, section_w, minimap_h), Textures.black);
                            section5_x += content_margin;
                            section5_y += content_margin;
                            float section5_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section5_x, section5_y, section5_w, 40), "Minimap", Styles.Content_Title());
                            section5_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section5_x, section5_y, section5_w, (minimap_h - 40 - (3 * content_margin))), Textures.grey);
                            section5_x += content_margin;
                            section5_y += content_margin;

                            if (GUI.Button(new Rect(section5_x, section5_y, section5_w - (2 * content_margin), 40), "Max Zoom Out", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Minimap.Enable_MaxZoomOut)))
                            { Save_Manager.Data.UserData.Scene.Minimap.Enable_MaxZoomOut = !Save_Manager.Data.UserData.Scene.Minimap.Enable_MaxZoomOut; }
                            section5_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section5_x, section5_y, section5_w - (2 * content_margin), 40), "Remove Fog of war", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Scene.Minimap.Remove_FogOfWar)))
                            { Save_Manager.Data.UserData.Scene.Minimap.Remove_FogOfWar = !Save_Manager.Data.UserData.Scene.Minimap.Remove_FogOfWar; }
                            section5_y += 40 + content_margin;
                        }
                        else if (BaseMenu.ForceDrop.Show)
                        {
                            float base_h = 40 + (3 * content_margin);
                            float config_h = Mods.ForceDrop.ForceDrop.main.GetSizeH() + base_h;
                            float max_config_h = Screen.height * 80 / 100 + base_h + (2 * content_margin);
                            if (config_h  > max_config_h) { config_h = max_config_h; }
                            
                            float section_w = (mods_content_width - (2 * ui_margin)) / 3;
                            float scene_x = mods_content_x;
                            float scene_y = mods_content_y;

                            //Options
                            float section1_x = scene_x;
                            float section2_x = scene_x + section_w + ui_margin;
                            float section3_x = section2_x + section_w + ui_margin;
                            float section_y = scene_y;
                            float content_w = section_w - (2 * content_margin);

                            GUI.DrawTexture(new Rect(section1_x, section_y, section_w, config_h), Textures.black);
                            section1_x += content_margin;
                            section_y += content_margin;

                            GUI.TextField(new Rect(section1_x, section_y, content_w, 40), "ForceDrop", Styles.Content_Title());
                            section_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section1_x, section_y, content_w, (config_h - 40 - (3 * content_margin))), Textures.grey);
                                                       
                            Mods.ForceDrop.ForceDrop.main.UI(section1_x, section_y, content_w);

                            int button_h = 40;
                            int margin = 2;
                            string ForceDropSection2_text = "";                            
                            float Forcedrop_section2_size_h = 0;
                            bool ShowForceDropSection2 = false;                            
                            if (Mods.ForceDrop.ForceDrop.type.show_dropdown)
                            {
                                ForceDropSection2_text = "Type";                                
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.type.dropdown_list.Length * (button_h + margin));                                
                                if (Mods.ForceDrop.ForceDrop.type.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.type.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else if (Mods.ForceDrop.ForceDrop.rarity.show_dropdown)
                            {
                                ForceDropSection2_text = "Rarity";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.rarity.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.rarity.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.rarity.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else if (Mods.ForceDrop.ForceDrop.base_item.show_dropdown)
                            {
                                ForceDropSection2_text = "Base Item";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.base_item.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.base_item.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.base_item.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else if (Mods.ForceDrop.ForceDrop.unique.show_dropdown)
                            {
                                ForceDropSection2_text = "Unique";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.unique.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.unique.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.unique.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else if (Mods.ForceDrop.ForceDrop.set.show_dropdown)
                            {
                                ForceDropSection2_text = "Set";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.set.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.set.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.set.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else if (Mods.ForceDrop.ForceDrop.legendary.show_dropdown)
                            {
                                ForceDropSection2_text = "Legendary";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.legendary.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.legendary.dropdown_rect.height > scrollview_max_h) { Forcedrop_section2_size_h = scrollview_max_h; }
                                else { Forcedrop_section2_size_h = Mods.ForceDrop.ForceDrop.legendary.dropdown_rect.height; }
                                ShowForceDropSection2 = true;
                            }
                            else { ShowForceDropSection2 = false; }

                            if (ShowForceDropSection2)
                            {
                                GUI.DrawTexture(new Rect(Section_2_X, mods_content_y, Section_W, Forcedrop_section2_size_h + 40 + (3 * content_margin)), Textures.black);                                
                                float section2_y = mods_content_y + content_margin;
                                float section2_w = Section_W - (2 * content_margin);
                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, section2_w, 40), ForceDropSection2_text, Styles.Content_Title());
                                section2_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section2_x + content_margin, section2_y, section2_w, (Forcedrop_section2_size_h)), Textures.grey);
                            }

                            string ForceDropSection3_text = "";
                            float Forcedrop_section3_size_h = 0;
                            bool ShowForceDropSection3 = false;
                            if (Mods.ForceDrop.ForceDrop.affixs.seal.show_dropdown) //Seal
                            {
                                ForceDropSection3_text = "Seal : Prefix / Suffix";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.affixs.seal.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height > scrollview_max_h) { Forcedrop_section3_size_h = scrollview_max_h; }
                                else { Forcedrop_section3_size_h = Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height; }
                                ShowForceDropSection3 = true;
                            }
                            else if ((Mods.ForceDrop.ForceDrop.affixs.prefixs.slot_0.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.prefixs.slot_1.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.prefixs.slot_2.show_dropdown))
                            {
                                ForceDropSection3_text = "Prefix / Suffix";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.affixs.prefixs.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height > scrollview_max_h) { Forcedrop_section3_size_h = scrollview_max_h; }
                                else { Forcedrop_section3_size_h = Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height; }
                                ShowForceDropSection3 = true;
                            }
                            else if ((Mods.ForceDrop.ForceDrop.affixs.suffixs.slot_0.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.suffixs.slot_1.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.suffixs.slot_2.show_dropdown))
                            {
                                ForceDropSection3_text = "Prefix / Suffix";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.affixs.suffixs.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height > scrollview_max_h) { Forcedrop_section3_size_h = scrollview_max_h; }
                                else { Forcedrop_section3_size_h = Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height; }
                                ShowForceDropSection3 = true;
                            }
                            else if ((Mods.ForceDrop.ForceDrop.affixs.idols.slot_0.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.idols.slot_1.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.idols.slot_2.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.idols.slot_3.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.idols.slot_4.show_dropdown) ||
                                (Mods.ForceDrop.ForceDrop.affixs.idols.slot_5.show_dropdown))
                            {
                                ForceDropSection3_text = "Idols";
                                float scrollview_max_h = (Mods.ForceDrop.ForceDrop.affixs.idols.dropdown_list.Length * (button_h + margin));
                                if (Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height > scrollview_max_h) { Forcedrop_section3_size_h = scrollview_max_h; }
                                else { Forcedrop_section3_size_h = Mods.ForceDrop.ForceDrop.affixs.dropdown_rect.height; }
                                ShowForceDropSection3 = true;
                            }

                            if (ShowForceDropSection3)
                            {
                                GUI.DrawTexture(new Rect(Section_3_X, mods_content_y, Section_W, Forcedrop_section3_size_h + 40 + (3 * content_margin)), Textures.black);
                                float section2_y = mods_content_y + content_margin;
                                float section2_w = Section_W - (2 * content_margin);
                                GUI.TextField(new Rect(section3_x + content_margin, section2_y, section2_w, 40), ForceDropSection3_text, Styles.Content_Title());
                                section2_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section3_x + content_margin, section2_y, section2_w, (Forcedrop_section3_size_h)), Textures.grey);
                            }

                            Mods.ForceDrop.ForceDrop.main.dropdown.DropdownsUI();

                        }
                        else if (BaseMenu.SkillsTree.Show)
                        {
                            float options_h = 460 + (13 * content_margin);
                            if (!Mods.SkillsTree.Options.Local_Tree_Data.IsNullOrDestroyed()) { options_h += 60 + content_margin; }
                            float section_w = (mods_content_width - (2 * ui_margin)) / 3;
                            float scene_x = mods_content_x;
                            float scene_y = mods_content_y;

                            //Options                            
                            float section1_x = scene_x;
                            float section1_y = scene_y;
                            float section1_w = section_w - (2 * content_margin);

                            GUI.DrawTexture(new Rect(section1_x, section1_y, section_w, options_h), Textures.black);
                            section1_x += content_margin;
                            section1_y += content_margin;

                            GUI.TextField(new Rect(section1_x, section1_y, section1_w, 40), "Options", Styles.Content_Title());
                            section1_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section1_x, section1_y, section1_w, (options_h - 40 - (3 * content_margin))), Textures.grey);
                            section1_x += content_margin;
                            section1_y += content_margin;
                            float section1_content_w = section1_w - (2 * content_margin);

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Remove Mana Cost", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_RemoveManaCost)))
                            { Save_Manager.Data.UserData.Skills.Enable_RemoveManaCost = !Save_Manager.Data.UserData.Skills.Enable_RemoveManaCost; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Remove Channel Cost", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_RemoveChannelCost)))
                            { Save_Manager.Data.UserData.Skills.Enable_RemoveChannelCost = !Save_Manager.Data.UserData.Skills.Enable_RemoveChannelCost; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Mana Regen While Channeling", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_NoManaRegenWhileChanneling)))
                            { Save_Manager.Data.UserData.Skills.Enable_NoManaRegenWhileChanneling = !Save_Manager.Data.UserData.Skills.Enable_NoManaRegenWhileChanneling; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Don't Stop when Out of Mana", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_StopWhenOutOfMana)))
                            { Save_Manager.Data.UserData.Skills.Enable_StopWhenOutOfMana = !Save_Manager.Data.UserData.Skills.Enable_StopWhenOutOfMana; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "No Cooldown", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_RemoveCooldown)))
                            { Save_Manager.Data.UserData.Skills.Enable_RemoveCooldown = !Save_Manager.Data.UserData.Skills.Enable_RemoveCooldown; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "Unlock all Skills", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Enable_AllSkills)))
                            { Save_Manager.Data.UserData.Skills.Enable_AllSkills = !Save_Manager.Data.UserData.Skills.Enable_AllSkills; }
                            section1_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section1_x, section1_y, section1_content_w, 40), "No Node Requirement (Passive & Skills)", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Disable_NodeRequirement)))
                            { Save_Manager.Data.UserData.Skills.Disable_NodeRequirement = !Save_Manager.Data.UserData.Skills.Disable_NodeRequirement; }
                            section1_y += 40 + content_margin;
                            if (!Mods.SkillsTree.Options.Local_Tree_Data.IsNullOrDestroyed())
                            {
                                GUI.TextField(new Rect(section1_x, section1_y, ((section1_content_w * 40) / 100), 40), "Unlocked Specialization Slots", Styles.Content_Text());
                                float nb_unlocked_slot_Temp = Save_Manager.Data.UserData.Skills.SkillTree.Slots;//Mods.SkillsTree.Options.Local_Tree_Data.numberOfUnlockedSlots;
                                nb_unlocked_slot_Temp = GUI.HorizontalSlider(new Rect(section1_x + content_margin, section1_y + 40, section1_content_w - (2 * content_margin), 20), nb_unlocked_slot_Temp, 0f, 5f);
                                GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 30) / 100), 40), nb_unlocked_slot_Temp.ToString(), Styles.ContentR_Text());                                
                                if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.SkillTree.Enable_Slots)))
                                { Save_Manager.Data.UserData.Skills.SkillTree.Enable_Slots = !Save_Manager.Data.UserData.Skills.SkillTree.Enable_Slots; }
                                Save_Manager.Data.UserData.Skills.SkillTree.Slots = (byte)nb_unlocked_slot_Temp;
                                //Mods.SkillsTree.Options.Local_Tree_Data.numberOfUnlockedSlots = (byte)nb_unlocked_slot_Temp;
                                section1_y += 60 + content_margin;
                            }

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Skill Level", Styles.Content_Text());
                            float SkillLevel_Temp = Save_Manager.Data.UserData.Skills.SkillTree.Level;
                            SkillLevel_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), SkillLevel_Temp, 0f, 255f);                            
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), SkillLevel_Temp.ToString(), Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.SkillTree.Enable_Level)))
                            { Save_Manager.Data.UserData.Skills.SkillTree.Enable_Level = !Save_Manager.Data.UserData.Skills.SkillTree.Enable_Level; }
                            Save_Manager.Data.UserData.Skills.SkillTree.Level = (byte)SkillLevel_Temp;
                            section1_y += 70 + content_margin;

                            GUI.TextField(new Rect(section1_x + content_margin, section1_y, ((section1_content_w * 40) / 100) - content_margin, 40), "Passive Points", Styles.Content_Text());
                            float multiplier = ushort.MaxValue / 255;
                            float PassiveLevel_Temp = Save_Manager.Data.UserData.Skills.PassiveTree.PointsEarnt / multiplier;
                            PassiveLevel_Temp = GUI.HorizontalSlider(new Rect(section1_x + (2 * content_margin), section1_y + 50, section1_content_w - (2 * content_margin), 20), PassiveLevel_Temp, 0f, 255f);
                            GUI.TextField(new Rect(section1_x + ((section1_content_w * 40) / 100) - (2 * content_margin), section1_y, ((section1_content_w * 20) / 100), 40), (PassiveLevel_Temp * multiplier).ToString(), Styles.ContentR_Text());
                            if (GUI.Button(new Rect(section1_x + ((section1_content_w * 60) / 100), section1_y + content_margin, ((section1_content_w * 40) / 100), 40), "Enable / Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.PassiveTree.Enable_PointsEarnt)))
                            { Save_Manager.Data.UserData.Skills.PassiveTree.Enable_PointsEarnt = !Save_Manager.Data.UserData.Skills.PassiveTree.Enable_PointsEarnt; }
                            Save_Manager.Data.UserData.Skills.PassiveTree.PointsEarnt = (ushort)(PassiveLevel_Temp * multiplier);
                            section1_y += 70 + content_margin;


                            //Movements
                            float movements_h = 160 + (7 * content_margin);
                            float section_movements_x = scene_x;
                            float section_movements_y = scene_y + options_h + ui_margin;
                            float section_movements_w = section_w - (2 * content_margin);
                            float section_movements_content_w = section_movements_w - (2 * content_margin);

                            GUI.DrawTexture(new Rect(section_movements_x, section_movements_y, section_w, movements_h), Textures.black);
                            section_movements_x += content_margin;
                            section_movements_y += content_margin;

                            GUI.TextField(new Rect(section_movements_x, section_movements_y, section_movements_w, 40), "Movements", Styles.Content_Title());
                            section_movements_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section_movements_x, section_movements_y, section_movements_w, (movements_h - 40 - (3 * content_margin))), Textures.grey);
                            section_movements_x += content_margin;
                            section_movements_y += content_margin;

                            if (GUI.Button(new Rect(section_movements_x, section_movements_y, section_movements_content_w, 40), "No Target", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.MovementSkills.Enable_NoTarget)))
                            { Save_Manager.Data.UserData.Skills.MovementSkills.Enable_NoTarget = !Save_Manager.Data.UserData.Skills.MovementSkills.Enable_NoTarget; }
                            section_movements_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section_movements_x, section_movements_y, section_movements_content_w, 40), "Immune during Movement", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.MovementSkills.Enable_ImmuneDuringMovement)))
                            { Save_Manager.Data.UserData.Skills.MovementSkills.Enable_ImmuneDuringMovement = !Save_Manager.Data.UserData.Skills.MovementSkills.Enable_ImmuneDuringMovement; }
                            section_movements_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section_movements_x, section_movements_y, section_movements_content_w, 40), "Disable Simple Path", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.MovementSkills.Disable_SimplePath)))
                            { Save_Manager.Data.UserData.Skills.MovementSkills.Disable_SimplePath = !Save_Manager.Data.UserData.Skills.MovementSkills.Disable_SimplePath; }
                            section_movements_y += 40 + content_margin;

                            //
                            int char_class = Functions.GetCharacterClass();
                            if (char_class == 3) //Acolyte (Minions)
                            {
                                //Skeletons
                                scene_x += section_w + ui_margin;
                                float minions_h = 480 + (12 * content_margin);
                                GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, minions_h), Textures.black);
                                float section2_x = scene_x + content_margin;
                                float section2_y = scene_y + content_margin;
                                float section2_w = section_w - (2 * content_margin);
                                GUI.TextField(new Rect(section2_x, section2_y, section2_w, 40), "Skeletons", Styles.Content_Title());
                                section2_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section2_x, section2_y, section2_w, (minions_h - 40 - (3 * content_margin))), Textures.grey);
                                section2_x += content_margin;
                                section2_y += content_margin;
                                float section2_content_w = section2_w - (2 * content_margin);
                                
                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "additional from passive", Styles.Content_Text());
                                float Skeletons_from_passive = Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsFromPassives;
                                Skeletons_from_passive = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Skeletons_from_passive, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Skeletons_from_passive.ToString(), Styles.Content_Text());                                
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromPassives)))
                                { Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromPassives = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromPassives; }
                                Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsFromPassives = (int)Skeletons_from_passive;
                                section2_y += 70 + content_margin;                                

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "additional from skills", Styles.Content_Text());
                                float Skeletons_from_skills = Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsFromSkillTree;
                                Skeletons_from_skills = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Skeletons_from_skills, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Skeletons_from_skills.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromSkillTree)))
                                { Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromSkillTree = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsFromSkillTree; }
                                Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsFromSkillTree = (int)Skeletons_from_skills;
                                section2_y += 70 + content_margin;

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Summon per cast", Styles.Content_Text());
                                float Skeletons_per_cast = Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsPerCast;
                                Skeletons_per_cast = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Skeletons_per_cast, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Skeletons_per_cast.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsPerCast)))
                                { Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsPerCast = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_additionalSkeletonsPerCast; }
                                Save_Manager.Data.UserData.Skills.Minions.Skeletons.additionalSkeletonsPerCast = (int)Skeletons_per_cast;
                                section2_y += 70 + content_margin;

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Chance to resummon OnDeath", Styles.Content_Text());
                                float Skeletons_resummon_ondeath = Save_Manager.Data.UserData.Skills.Minions.Skeletons.chanceToResummonOnDeath;
                                Skeletons_resummon_ondeath = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Skeletons_resummon_ondeath, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Skeletons_resummon_ondeath.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_chanceToResummonOnDeath)))
                                { Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_chanceToResummonOnDeath = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_chanceToResummonOnDeath; }
                                Save_Manager.Data.UserData.Skills.Minions.Skeletons.chanceToResummonOnDeath = Skeletons_resummon_ondeath;
                                section2_y += 70 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Force Archer", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher;
                                }
                                section2_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Force Warrior", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior;
                                }
                                section2_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Force Rogue", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue;
                                }
                                section2_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Force Brawler", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceArcher = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceRogue = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceWarrior = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler = !Save_Manager.Data.UserData.Skills.Minions.Skeletons.Enable_forceBrawler;
                                }
                                section2_y += 40 + content_margin;

                                //Wraiths
                                float wraiths_h = 330 + (9 * content_margin);
                                section2_x -= (2 * content_margin);
                                section2_y += ui_margin;
                                GUI.DrawTexture(new Rect(section2_x, section2_y, section_w, wraiths_h), Textures.black);
                                section2_x += content_margin;
                                section2_y += content_margin;
                                GUI.TextField(new Rect(section2_x, section2_y, section2_w, 40), "Wraiths", Styles.Content_Title());
                                
                                section2_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section2_x, section2_y, section2_w, (wraiths_h - 40 - (3 * content_margin))), Textures.grey);
                                section2_x += content_margin;
                                section2_y += content_margin;

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Additional Max", Styles.Content_Text());
                                float Wratihs_Additional = Save_Manager.Data.UserData.Skills.Minions.Wraiths.additionalMaxWraiths;
                                Wratihs_Additional = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Wratihs_Additional, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Wratihs_Additional.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_additionalMaxWraiths)))
                                { Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_additionalMaxWraiths = !Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_additionalMaxWraiths; }
                                Save_Manager.Data.UserData.Skills.Minions.Wraiths.additionalMaxWraiths = (int)Wratihs_Additional;
                                section2_y += 70 + content_margin;

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Cast Speed", Styles.Content_Text());
                                float Wratihs_CastSpeed = Save_Manager.Data.UserData.Skills.Minions.Wraiths.increasedCastSpeed;
                                Wratihs_CastSpeed = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Wratihs_CastSpeed, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Wratihs_CastSpeed.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_increasedCastSpeed)))
                                { Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_increasedCastSpeed = !Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_increasedCastSpeed; }
                                Save_Manager.Data.UserData.Skills.Minions.Wraiths.increasedCastSpeed = Wratihs_CastSpeed;
                                section2_y += 70 + content_margin;

                                GUI.TextField(new Rect(section2_x + content_margin, section2_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Delayed", Styles.Content_Text());
                                float Wratihs_Delay = Save_Manager.Data.UserData.Skills.Minions.Wraiths.delayedWraiths;
                                Wratihs_Delay = GUI.HorizontalSlider(new Rect(section2_x + (2 * content_margin), section2_y + 50, section2_content_w - (2 * content_margin), 20), Wratihs_Delay, 0f, 255f);
                                GUI.TextField(new Rect(section2_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section2_y, ((section2_content_w * 20) / 100), 40), Wratihs_Delay.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section2_x + ((section2_content_w * 60) / 100), section2_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_delayedWraiths)))
                                { Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_delayedWraiths = !Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_delayedWraiths; }
                                Save_Manager.Data.UserData.Skills.Minions.Wraiths.delayedWraiths = (int)Wratihs_Delay;
                                section2_y += 70 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "Remove Limit to 2 Wraiths", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_limitedTo2Wraiths)))
                                { Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_limitedTo2Wraiths = !Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_limitedTo2Wraiths; }
                                section2_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section2_x, section2_y, section2_content_w, 40), "No decay", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_wraithsDoNotDecay)))
                                { Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_wraithsDoNotDecay = !Save_Manager.Data.UserData.Skills.Minions.Wraiths.Enable_wraithsDoNotDecay; }
                                section2_y += 40 + content_margin;

                                //Mages
                                float mages_h = 630 + (15 * content_margin);
                                GUI.DrawTexture(new Rect(Section_3_X, mods_content_y, section_w, mages_h), Textures.black);
                                float section3_x = Section_3_X + content_margin;
                                float section3_y = mods_content_y + content_margin;
                                float section3_w = section_w - (2 * content_margin);
                                GUI.TextField(new Rect(section3_x, section3_y, section3_w, 40), "Mages", Styles.Content_Title());
                                section3_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section3_x, section3_y, section3_w, (mages_h - 40 - (3 * content_margin))), Textures.grey);
                                section3_x += content_margin;
                                section3_y += content_margin;

                                GUI.TextField(new Rect(section3_x + content_margin, section3_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Additional from Items", Styles.Content_Text());
                                float Mage_AdditionalItems = Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromItems;
                                Mage_AdditionalItems = GUI.HorizontalSlider(new Rect(section3_x + (2 * content_margin), section3_y + 50, section2_content_w - (2 * content_margin), 20), Mage_AdditionalItems, 0f, 255f);
                                GUI.TextField(new Rect(section3_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section2_content_w * 20) / 100), 40), Mage_AdditionalItems.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section3_x + ((section2_content_w * 60) / 100), section3_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromItems)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromItems = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromItems; }
                                Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromItems = (int)Mage_AdditionalItems;
                                section3_y += 70 + content_margin;

                                GUI.TextField(new Rect(section3_x + content_margin, section3_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Additional from Passives", Styles.Content_Text());
                                float Mage_AdditionalPassives = Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromPassives;
                                Mage_AdditionalPassives = GUI.HorizontalSlider(new Rect(section3_x + (2 * content_margin), section3_y + 50, section2_content_w - (2 * content_margin), 20), Mage_AdditionalPassives, 0f, 255f);
                                GUI.TextField(new Rect(section3_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section2_content_w * 20) / 100), 40), Mage_AdditionalPassives.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section3_x + ((section2_content_w * 60) / 100), section3_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromPassives)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromPassives = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromPassives; }
                                Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromPassives = (int)Mage_AdditionalPassives;
                                section3_y += 70 + content_margin;

                                GUI.TextField(new Rect(section3_x + content_margin, section3_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Additional from Skills", Styles.Content_Text());
                                float Mage_AdditionalSkills = Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromSkillTree;
                                Mage_AdditionalSkills = GUI.HorizontalSlider(new Rect(section3_x + (2 * content_margin), section3_y + 50, section2_content_w - (2 * content_margin), 20), Mage_AdditionalSkills, 0f, 255f);
                                GUI.TextField(new Rect(section3_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section2_content_w * 20) / 100), 40), Mage_AdditionalSkills.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section3_x + ((section2_content_w * 60) / 100), section3_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromSkillTree)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromSkillTree = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsFromSkillTree; }
                                Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsFromSkillTree = (int)Mage_AdditionalSkills;
                                section3_y += 70 + content_margin;

                                GUI.TextField(new Rect(section3_x + content_margin, section3_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Summon per Cast", Styles.Content_Text());
                                float Mage_AdditionalPerCast = Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsPerCast;
                                Mage_AdditionalPerCast = GUI.HorizontalSlider(new Rect(section3_x + (2 * content_margin), section3_y + 50, section2_content_w - (2 * content_margin), 20), Mage_AdditionalPerCast, 0f, 255f);
                                GUI.TextField(new Rect(section3_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section2_content_w * 20) / 100), 40), Mage_AdditionalPerCast.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section3_x + ((section2_content_w * 60) / 100), section3_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsPerCast)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsPerCast = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_additionalSkeletonsPerCast; }
                                Save_Manager.Data.UserData.Skills.Minions.Mages.additionalSkeletonsPerCast = (int)Mage_AdditionalPerCast;
                                section3_y += 70 + content_margin;

                                GUI.TextField(new Rect(section3_x + content_margin, section3_y, ((section2_content_w * 40) / 100) - content_margin, 40), "Chance for 2 Extra projectiles", Styles.Content_Text());
                                float Mage_Chance2Projectile = Save_Manager.Data.UserData.Skills.Minions.Mages.chanceForTwoExtraProjectiles;
                                Mage_Chance2Projectile = GUI.HorizontalSlider(new Rect(section3_x + (2 * content_margin), section3_y + 50, section2_content_w - (2 * content_margin), 20), Mage_Chance2Projectile, 0f, 255f);
                                GUI.TextField(new Rect(section3_x + ((section2_content_w * 40) / 100) - (2 * content_margin), section3_y, ((section2_content_w * 20) / 100), 40), Mage_Chance2Projectile.ToString(), Styles.Content_Text());
                                if (GUI.Button(new Rect(section3_x + ((section2_content_w * 60) / 100), section3_y + content_margin, ((section2_content_w * 40) / 100), 40), "Enable/Disable", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_chanceForTwoExtraProjectiles)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_chanceForTwoExtraProjectiles = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_chanceForTwoExtraProjectiles; }
                                Save_Manager.Data.UserData.Skills.Minions.Mages.chanceForTwoExtraProjectiles = Mage_Chance2Projectile;
                                section3_y += 70 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Remove only one Mage", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_onlySummonOneMage)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_onlySummonOneMage = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_onlySummonOneMage; }
                                section3_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Remove Single Summon", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_singleSummon)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_singleSummon = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_singleSummon; }
                                section3_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Force Cryomancer", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceCryomancer)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceDeathKnight = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forcePyromancer = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceCryomancer = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceCryomancer;
                                }
                                section3_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Force Pyromancer", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forcePyromancer)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceDeathKnight = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceCryomancer = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forcePyromancer = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forcePyromancer;
                                }
                                section3_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Force DeathKnight", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceDeathKnight)))
                                {
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceCryomancer = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forcePyromancer = false;
                                    Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceDeathKnight = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_forceDeathKnight;
                                }
                                section3_y += 40 + content_margin;

                                if (GUI.Button(new Rect(section3_x, section3_y, section2_content_w, 40), "Double Projectile", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_doubleProjectiles)))
                                { Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_doubleProjectiles = !Save_Manager.Data.UserData.Skills.Minions.Mages.Enable_doubleProjectiles; }
                                section3_y += 40 + content_margin;

                                //

                            }
                            if (char_class == 0) //Primalist (Companions)
                            {
                                //Companions
                                float Companions_h = 110 + (4 * content_margin);
                                float section_companions_x = scene_x;
                                float section_companions_y = scene_y + options_h + movements_h + (2 * ui_margin);
                                GUI.DrawTexture(new Rect(section_companions_x, section_companions_y, section_w, Companions_h), Textures.black);
                                section_companions_x += content_margin;
                                section_companions_y += content_margin;
                                float section_companions_w = section_w - (2 * content_margin);
                                GUI.TextField(new Rect(section_companions_x, section_companions_y, section_companions_w, 40), "Companions", Styles.Content_Title());
                                section_companions_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section_companions_x, section_companions_y, section_companions_w, (Companions_h - 40 - (3 * content_margin))), Textures.grey);
                                section_companions_x += content_margin;
                                section_companions_y += content_margin;
                                float section_companions_content_w = section_companions_w - (2 * content_margin);

                                GUI.TextField(new Rect(section_companions_x + content_margin, section_companions_y, ((section_companions_content_w * 40) / 100) - content_margin, 40), "Maximum Companions", Styles.Content_Text());
                                float Companions_Limit_Temp = Save_Manager.Data.UserData.Skills.Companion.Limit;
                                Companions_Limit_Temp = GUI.HorizontalSlider(new Rect(section_companions_x + (2 * content_margin), section_companions_y + 50, section_companions_content_w - (2 * content_margin), 20), Companions_Limit_Temp, 0f, 255f);
                                GUI.TextArea(new Rect(section_companions_x + ((section_companions_content_w * 40) / 100) - (2 * content_margin), section_companions_y, ((section_companions_content_w * 20) / 100), 40), Companions_Limit_Temp.ToString(), Styles.Content_TextArea());
                                if (GUI.Button(new Rect(section_companions_x + ((section_companions_content_w * 60) / 100), section_companions_y + content_margin, ((section_companions_content_w * 40) / 100), 40), "Enable/Disable", Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Companion.Enable_Limit)))
                                { Save_Manager.Data.UserData.Skills.Companion.Enable_Limit = !Save_Manager.Data.UserData.Skills.Companion.Enable_Limit; }
                                Save_Manager.Data.UserData.Skills.Companion.Limit = (int)Companions_Limit_Temp;
                                section_companions_y += 70 + content_margin;

                                //Wolfs
                                float Wolf_x = scene_x + section_w + ui_margin;
                                float Wolfs_y = scene_y;
                                float Wolfs_h = 190 + (7 * content_margin);
                                GUI.DrawTexture(new Rect(Wolf_x, Wolfs_y, section_w, Wolfs_h), Textures.black);
                                float section_wolfs_x = Wolf_x + content_margin;
                                float section_wolfs_y = Wolfs_y + content_margin;
                                float section_wolfs_w = section_w - (2 * content_margin);
                                GUI.TextField(new Rect(section_wolfs_x, section_wolfs_y, section_wolfs_w, 40), "Wolfs", Styles.Content_Title());
                                section_wolfs_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section_wolfs_x, section_wolfs_y, section_wolfs_w, (Wolfs_h - 40 - (3 * content_margin))), Textures.grey);
                                section_wolfs_x += content_margin;
                                section_wolfs_y += content_margin;
                                float section_wolfs_content_w = section_wolfs_w - (2 * content_margin);
                                
                                if (GUI.Button(new Rect(section_wolfs_x, section_wolfs_y, section_wolfs_content_w, 40), "Can summon Up To Max Companions", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonMax)))
                                { Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonMax = !Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonMax; }
                                section_wolfs_y += 40 + content_margin;

                                GUI.TextField(new Rect(section_wolfs_x + content_margin, section_wolfs_y, ((section_wolfs_content_w * 40) / 100) - content_margin, 40), "Summon Limit", Styles.Content_Text());
                                float Wolf_Summon_Limit_Temp = Save_Manager.Data.UserData.Skills.Companion.Wolf.SummonLimit;
                                Wolf_Summon_Limit_Temp = GUI.HorizontalSlider(new Rect(section_wolfs_x + (2 * content_margin), section_wolfs_y + 50, section_wolfs_content_w - (2 * content_margin), 20), Wolf_Summon_Limit_Temp, 0f, 255f);
                                GUI.TextArea(new Rect(section_wolfs_x + ((section_wolfs_content_w * 40) / 100) - (2 * content_margin), section_wolfs_y, ((section_wolfs_content_w * 20) / 100), 40), Wolf_Summon_Limit_Temp.ToString(), Styles.Content_TextArea());                                
                                if (GUI.Button(new Rect(section_wolfs_x + ((section_wolfs_content_w * 60) / 100), section_wolfs_y + content_margin, ((section_wolfs_content_w * 40) / 100), 40), "Enable/Disable", Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonLimit)))
                                { Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonLimit = !Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_SummonLimit; }
                                Save_Manager.Data.UserData.Skills.Companion.Wolf.SummonLimit = (int)Wolf_Summon_Limit_Temp;
                                section_wolfs_y += 70 + content_margin;

                                if (GUI.Button(new Rect(section_wolfs_x, section_wolfs_y, section_wolfs_content_w, 40), "Stun Immunity", Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_StunImmunity)))
                                { Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_StunImmunity = !Save_Manager.Data.UserData.Skills.Companion.Wolf.Enable_StunImmunity; }
                                section_wolfs_y += 40 + content_margin;

                                //Show_Scorpion
                                float Scorpions_x = scene_x + section_w + ui_margin;
                                float Scorpions_y = scene_y + Wolfs_h + ui_margin;
                                float Scorpions_h = 110 + (4 * content_margin);
                                GUI.DrawTexture(new Rect(Scorpions_x, Scorpions_y, section_w, Scorpions_h), Textures.black);
                                float section_Scorpions_x = Scorpions_x + content_margin;
                                float section_Scorpions_y = Scorpions_y + content_margin;
                                float section_Scorpions_w = section_w - (2 * content_margin);
                                GUI.TextField(new Rect(section_Scorpions_x, section_Scorpions_y, section_Scorpions_w, 40), "Scorpions", Styles.Content_Title());
                                section_Scorpions_y += 40 + content_margin;
                                GUI.DrawTexture(new Rect(section_Scorpions_x, section_Scorpions_y, section_Scorpions_w, (Scorpions_h - 40 - (3 * content_margin))), Textures.grey);
                                section_Scorpions_x += content_margin;
                                section_Scorpions_y += content_margin;
                                float section_Scorpions_content_w = section_Scorpions_w - (2 * content_margin);

                                GUI.TextField(new Rect(section_Scorpions_x + content_margin, section_Scorpions_y, ((section_Scorpions_content_w * 40) / 100) - content_margin, 40), "Summon Limit", Styles.Content_Text());
                                float Scorpions_Summon_Baby_Limit_Temp = Save_Manager.Data.UserData.Skills.Companion.Scorpion.BabyQuantity;
                                Scorpions_Summon_Baby_Limit_Temp = GUI.HorizontalSlider(new Rect(section_Scorpions_x + (2 * content_margin), section_Scorpions_y + 50, section_Scorpions_content_w - (2 * content_margin), 20), Scorpions_Summon_Baby_Limit_Temp, 0f, 255f);
                                GUI.TextArea(new Rect(section_Scorpions_x + ((section_Scorpions_content_w * 40) / 100) - (2 * content_margin), section_Scorpions_y, ((section_Scorpions_content_w * 20) / 100), 40), Scorpions_Summon_Baby_Limit_Temp.ToString(), Styles.Content_TextArea());
                                if (GUI.Button(new Rect(section_Scorpions_x + ((section_Scorpions_content_w * 60) / 100), section_Scorpions_y + content_margin, ((section_Scorpions_content_w * 40) / 100), 40), "Enable/Disable", Styles.Content_Enable_Button(Save_Manager.Data.UserData.Skills.Companion.Scorpion.Enable_BabyQuantity)))
                                { Save_Manager.Data.UserData.Skills.Companion.Scorpion.Enable_BabyQuantity = !Save_Manager.Data.UserData.Skills.Companion.Scorpion.Enable_BabyQuantity; }
                                Save_Manager.Data.UserData.Skills.Companion.Scorpion.BabyQuantity = (int)Scorpions_Summon_Baby_Limit_Temp;
                                section_Scorpions_y += 70 + content_margin;
                            }
                        }
                        else if (BaseMenu.Headhunter.Show)
                        {
                            float section_w = (mods_content_width - (2 * ui_margin)) / 3;
                            float scene_x = mods_content_x;
                            float scene_y = mods_content_y;

                            float Items_Headhunter_h = 200 + (8 * content_margin); //A Verifier
                            string random = "Seal Buffs";
                            if (Save_Manager.Data.UserData.Items.Headhunter.random)
                            {
                                random = "Random Buffs";
                                Items_Headhunter_h += 300 + (4 * content_margin); //A changer
                            }
                            GUI.DrawTexture(new Rect(scene_x, scene_y, section_w, Items_Headhunter_h), Textures.black);
                            float section3_x = scene_x + content_margin;
                            float section3_y = scene_y + content_margin;
                            float section3_w = section_w - (2 * content_margin);
                            GUI.TextField(new Rect(section3_x, section3_y, section3_w, 40), "Headhunter", Styles.Content_Title());
                            section3_y += 40 + content_margin;
                            GUI.DrawTexture(new Rect(section3_x, section3_y, section3_w, (Items_Headhunter_h - 40 - (3 * content_margin))), Textures.grey);
                            section3_x += content_margin;
                            section3_y += content_margin;
                            float section3_content_w = section3_w - (2 * content_margin);

                            string legendary_txt = "Legendary Potencial";
                            if (Save_Manager.Data.UserData.Items.Headhunter.weaverwill) { legendary_txt = "Weaver Will"; }
                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), legendary_txt, Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Headhunter.weaverwill)))
                            { Save_Manager.Data.UserData.Items.Headhunter.weaverwill = !Save_Manager.Data.UserData.Items.Headhunter.weaverwill; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), random, Managers.GUI_Manager.Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Headhunter.random)))
                            { Save_Manager.Data.UserData.Items.Headhunter.random = !Save_Manager.Data.UserData.Items.Headhunter.random; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Base can Drop", Styles.Content_Enable_Button(!Save_Manager.Data.UserData.Items.Headhunter.base_item_cannotDrop)))
                            { Save_Manager.Data.UserData.Items.Headhunter.base_item_cannotDrop = !Save_Manager.Data.UserData.Items.Headhunter.base_item_cannotDrop; }
                            section3_y += 40 + content_margin;

                            if (GUI.Button(new Rect(section3_x, section3_y, section3_w - (2 * content_margin), 40), "Unique can Drop", Styles.Content_Enable_Button(Save_Manager.Data.UserData.Items.Headhunter.unique_item_canDropRandomly)))
                            { Save_Manager.Data.UserData.Items.Headhunter.unique_item_canDropRandomly = !Save_Manager.Data.UserData.Items.Headhunter.unique_item_canDropRandomly; }
                            section3_y += 40 + content_margin;

                            if (Save_Manager.Data.UserData.Items.Headhunter.random)
                            {
                                GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 70) / 100), 40), "Min Generated", Styles.Content_Text());
                                float HH_MinGenerated_Temp = Save_Manager.Data.UserData.Items.Headhunter.Min_Generated;
                                HH_MinGenerated_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 40, section3_content_w - (2 * content_margin), 20), HH_MinGenerated_Temp, 0, 100);
                                string HH_MinGenerated_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 70) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 30) / 100), 40), HH_MinGenerated_Temp.ToString(), Styles.Content_TextArea());
                                try { HH_MinGenerated_Temp = float.Parse(HH_MinGenerated_Text, CultureInfo.InvariantCulture.NumberFormat); }
                                catch { }
                                Save_Manager.Data.UserData.Items.Headhunter.Min_Generated = (int)HH_MinGenerated_Temp;
                                section3_y += 60 + content_margin;

                                GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 70) / 100), 40), "Max Generated", Styles.Content_Text());
                                float HH_MaxGenerated_Temp = Save_Manager.Data.UserData.Items.Headhunter.Max_Generated;
                                HH_MaxGenerated_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 40, section3_content_w - (2 * content_margin), 20), HH_MaxGenerated_Temp, 0, 100);
                                string HH_MaxGenerated_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 70) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 30) / 100), 40), HH_MaxGenerated_Temp.ToString(), Styles.Content_TextArea());
                                try { HH_MaxGenerated_Temp = float.Parse(HH_MaxGenerated_Text, CultureInfo.InvariantCulture.NumberFormat); }
                                catch { }
                                Save_Manager.Data.UserData.Items.Headhunter.Max_Generated = (int)HH_MaxGenerated_Temp;
                                section3_y += 60 + content_margin;

                                GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 70) / 100), 40), "Buffs Duration", Styles.Content_Text());
                                float HH_BuffDuration_Temp = Save_Manager.Data.UserData.Items.Headhunter.BuffDuration;
                                HH_BuffDuration_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 40, section3_content_w - (2 * content_margin), 20), HH_BuffDuration_Temp, 0, 100);
                                string HH_BuffDuration_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 70) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 30) / 100), 40), HH_BuffDuration_Temp.ToString(), Styles.Content_TextArea());
                                try { HH_BuffDuration_Temp = float.Parse(HH_BuffDuration_Text, CultureInfo.InvariantCulture.NumberFormat); }
                                catch { }
                                Save_Manager.Data.UserData.Items.Headhunter.BuffDuration = HH_BuffDuration_Temp;
                                section3_y += 60 + content_margin;

                                GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 70) / 100), 40), "Added Value", Styles.Content_Text());
                                float HH_AddValue_Temp = Save_Manager.Data.UserData.Items.Headhunter.addvalue;
                                HH_AddValue_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 40, section3_content_w - (2 * content_margin), 20), HH_AddValue_Temp, 0, 100);
                                string HH_addValue_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 70) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 30) / 100), 40), HH_AddValue_Temp.ToString(), Styles.Content_TextArea());
                                try { HH_AddValue_Temp = float.Parse(HH_addValue_Text, CultureInfo.InvariantCulture.NumberFormat); }
                                catch { }
                                Save_Manager.Data.UserData.Items.Headhunter.addvalue = HH_AddValue_Temp;
                                section3_y += 60 + content_margin;

                                GUI.TextField(new Rect(section3_x, section3_y, ((section3_content_w * 70) / 100), 40), "Increased Value", Styles.Content_Text());
                                float HH_IncreaseValue_Temp = Save_Manager.Data.UserData.Items.Headhunter.increasedvalue;
                                HH_IncreaseValue_Temp = GUI.HorizontalSlider(new Rect(section3_x + content_margin, section3_y + 40, section3_content_w - (2 * content_margin), 20), HH_IncreaseValue_Temp, 0, 100);
                                string HH_IncreaseValue_Text = GUI.TextArea(new Rect(section3_x + ((section3_content_w * 70) / 100) - (2 * content_margin), section3_y, ((section3_content_w * 30) / 100), 40), HH_IncreaseValue_Temp.ToString(), Styles.Content_TextArea());
                                try { HH_IncreaseValue_Temp = float.Parse(HH_IncreaseValue_Text, CultureInfo.InvariantCulture.NumberFormat); }
                                catch { }
                                Save_Manager.Data.UserData.Items.Headhunter.increasedvalue = HH_IncreaseValue_Temp;
                                section3_y += 60 + content_margin;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //Functions.HideAllContent();
                    }
                }

                public class DefaultBtns
                {
                    public static void Resume_Click()
                    {
                        Refs.Resume_Btn.onClick.Invoke();
                    }
                    public static void Settings_Click()
                    {
                        Refs.Settings_Btn.onClick.Invoke();
                    }
                    public static void GameGuide_Click()
                    {
                        Refs.GameGuide_Btn.onClick.Invoke();
                    }
                    public static void LeaveGame_Click()
                    {
                        Functions.HideAllContent();
                        Refs.Leave_Btn.onClick.Invoke();
                    }
                    public static void ExitDesktop_Click()
                    {
                        Functions.HideAllContent();
                        Refs.Exit_Btn.onClick.Invoke();
                    }
                }
                public class BaseMenu
                {
                    public static Vector2 scrollview_base_menu = Vector2.zero;
                    public class Character
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Items.Show = false;
                            Scenes.Show = false;
                            ForceDrop.Show = false;
                            SkillsTree.Show = false;
                            Headhunter.Show = false;
                            Show = !Show;
                        }
                    }
                    public class Items
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Character.Show = false;
                            Scenes.Show = false;
                            ForceDrop.Show = false;
                            SkillsTree.Show = false;
                            Headhunter.Show = false;
                            Show = !Show;
                        }
                    }
                    public class Scenes
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Items.Show = false;
                            Character.Show = false;
                            ForceDrop.Show = false;
                            SkillsTree.Show = false;
                            Headhunter.Show = false;
                            Show = !Show;
                        }
                    }
                    public class ForceDrop
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Items.Show = false;
                            Character.Show = false;
                            Scenes.Show = false;
                            SkillsTree.Show = false;
                            Headhunter.Show = false;
                            Show = !Show;
                        }
                    }
                    public class SkillsTree
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Items.Show = false;
                            Character.Show = false;
                            Scenes.Show = false;
                            ForceDrop.Show= false;
                            Headhunter.Show = false;
                            Show = !Show;
                        }
                    }
                    public class Headhunter
                    {
                        public static bool Show = false;
                        public static void Btn_Click()
                        {
                            Items.Show = false;
                            Character.Show = false;
                            Scenes.Show = false;
                            ForceDrop.Show = false;
                            SkillsTree.Show = false;
                            Show = !Show;
                        }
                    }
                }
                public class CustomControls
                {
                    public delegate void DelegateFunction();

                    public static float Toggle(float pos_x, float pos_y, float size_w, string text, ref bool toggle)
                    {
                        if (GUI.Button(new Rect(pos_x, pos_y, size_w, 40), text, Styles.Content_Enable_Button(toggle))) { toggle = !toggle; }

                        return 40 + content_margin;
                    }
                    public static float Function(float pos_x, float pos_y, float size_w, string text, DelegateFunction function)
                    {
                        if (GUI.Button(new Rect(pos_x, pos_y, size_w, 40), text, Styles.Content_Button())) { function(); }

                        return 40 + content_margin;
                    }
                    public static float Toggle_FloatValue(float pos_x, float pos_y, float size_w, string text, ref float value, ref bool toggle)
                    {
                        GUI.TextField(new Rect(pos_x, pos_y, ((size_w * 40) / 100) - content_margin, 40), text, Styles.Content_Text());
                        GUI.TextField(new Rect(pos_x + ((size_w * 40) / 100) - (2 * content_margin), pos_y, ((size_w * 20) / 100), 40), value.ToString(), Styles.ContentR_Text());
                        if (GUI.Button(new Rect(pos_x + ((size_w * 60) / 100), pos_y, ((size_w * 40) / 100), 40), "Enable/Disable", Styles.Content_Enable_Button(toggle))) { toggle = !toggle; }
                        value = GUI.HorizontalSlider(new Rect(pos_x + content_margin, pos_y + 50, size_w - (2 * content_margin), 20), value, 0f, 255f);

                        return 70 + content_margin;
                    }
                    public static float Toggle_FloatPercent(float pos_x, float pos_y, float size_w, string text, float min, float max, bool multiply, ref float value, ref bool toggle)
                    {
                        GUI.TextField(new Rect(pos_x, pos_y, ((size_w * 40) / 100) - content_margin, 40), text, Styles.Content_Text());                        
                        string TempText = "";
                        if (!multiply) { TempText = System.Convert.ToInt32(value / 255 * 100) + " %"; }
                        else { TempText = (value * 100) + " %"; }
                        GUI.TextField(new Rect(pos_x + ((size_w * 40) / 100) - (2 * content_margin), pos_y, ((size_w * 20) / 100), 40), TempText, Styles.ContentR_Text());
                        if (GUI.Button(new Rect(pos_x + ((size_w * 60) / 100), pos_y, ((size_w * 40) / 100), 40), "Enable/Disable", Styles.Content_Enable_Button(toggle))) { toggle = !toggle; }
                        value = GUI.HorizontalSlider(new Rect(pos_x + content_margin, pos_y + 50, size_w - (2 * content_margin), 20), value, 0f, 255f);
                        
                        return 70 + content_margin;
                    }
                }
            }
            public class Functions
            {
                public static void Init()
                {
                    try
                    {
                        if (Refs.PauseMenu.IsNullOrDestroyed())
                        {
                            GameObject Draw_over_login_canvas = GeneralFunctions.GetChild(Base.Refs.GameObject_GUI, "Draw Over Login Canvas");
                            if (!Draw_over_login_canvas.IsNullOrDestroyed())
                            {
                                Refs.PauseMenu = GeneralFunctions.GetChild(Draw_over_login_canvas, "Menu");
                            }
                        }
                    }
                    catch { Main.logger_instance.Error("PauseMenu:Init -> Canvas Error"); }
                    try
                    {
                        if ((!Refs.PauseMenu.IsNullOrDestroyed()) && (Refs.Default_PauseMenu_Btns.IsNullOrDestroyed()))
                        {
                            Refs.Default_PauseMenu_Btns = GeneralFunctions.GetChild(Refs.PauseMenu, "Menu Image");
                        }
                    }
                    catch { Main.logger_instance.Error("PauseMenu:Init -> Defaults Buttons Panel Error"); }
                    try
                    {
                        if ((!Refs.PauseMenu.IsNullOrDestroyed()) && ((Refs.Resume_Btn.IsNullOrDestroyed()) ||
                            (Refs.Settings_Btn.IsNullOrDestroyed()) || (Refs.GameGuide_Btn.IsNullOrDestroyed()) ||
                            (Refs.Leave_Btn.IsNullOrDestroyed()) || (Refs.Exit_Btn.IsNullOrDestroyed())))
                        {
                            GameObject Btns = GeneralFunctions.GetChild(Refs.Default_PauseMenu_Btns, "Buttons");
                            if (!Btns.IsNullOrDestroyed())
                            {
                                Refs.Resume_Btn = GeneralFunctions.GetChild(Btns, "ResumeButton (1)").GetComponent<Button>();
                                Refs.Settings_Btn = GeneralFunctions.GetChild(Btns, "SettingsButton").GetComponent<Button>();
                                Refs.GameGuide_Btn = GeneralFunctions.GetChild(Btns, "GameButton").GetComponent<Button>();
                                Refs.Leave_Btn = GeneralFunctions.GetChild(Btns, "ExitToCharacterSelectButton").GetComponent<Button>();
                                Refs.Exit_Btn = GeneralFunctions.GetChild(Btns, "ExitGameButton").GetComponent<Button>();

                                Functions.ShowHide_DefaultPauseMenu(false);
                                Main.logger_instance.Msg("PauseMenu Initialized");
                                Initialized = true;
                            }
                        }
                    }
                    catch { Main.logger_instance.Error("PauseMenu:Init -> Defaults Buttons Initialize Error"); }
                }
                public static void Update()
                {
                    if ((Refs.PauseMenu.IsNullOrDestroyed()) || (Refs.Default_PauseMenu_Btns.IsNullOrDestroyed()) ||
                        (Refs.Resume_Btn.IsNullOrDestroyed()) || (Refs.Settings_Btn.IsNullOrDestroyed()) ||
                        (Refs.GameGuide_Btn.IsNullOrDestroyed()) || (Refs.Leave_Btn.IsNullOrDestroyed()) ||
                        (Refs.Exit_Btn.IsNullOrDestroyed()))
                    {
                        Initialized = false;
                    }
                    if (!Initialized) { Init(); }
                }
                public static bool IsPauseMenuOpen()
                {
                    bool result = false;
                    try
                    {
                        if (!Refs.PauseMenu.IsNullOrDestroyed())
                        {
                            {
                                if (Refs.PauseMenu.active) { result = true; }
                            }
                        }
                    }
                    catch { Main.logger_instance.Error("PauseMenu:IsMenuOpen"); }

                    return result;
                }
                public static void ShowHide_DefaultPauseMenu(bool show)
                {
                    Refs.Default_PauseMenu_Btns.active = show;
                }
                public static void HideAllContent()
                {
                    UI.BaseMenu.Character.Show = false;
                    UI.BaseMenu.Items.Show = false;
                    UI.BaseMenu.Scenes.Show = false;
                    UI.BaseMenu.ForceDrop.Show = false;
                    UI.BaseMenu.SkillsTree.Show = false;
                    UI.BaseMenu.Headhunter.Show = false;
                }
                public static void Close()
                {
                    Base.Refs.Game_UIBase.closeMenu();
                }
                public static int GetCharacterClass()
                {
                    int result = 0;
                    try
                    {
                        result = PlayerFinder.getPlayerData().CharacterClass;
                    }
                    catch { }

                    return result;
                }
            }
        }
        public class InventoryPanel
        {
            public static bool Initialized = false;

            public class Refs
            {
                public static InventoryPanelUI InventoryPanelUI;
            }
            public class Functions
            {
                public static void Init()
                {
                    try
                    {
                        if (!Base.Refs.Game_UIBase.IsNullOrDestroyed())
                        {
                            Refs.InventoryPanelUI = Base.Refs.Game_UIBase.inventoryPanel.instance.gameObject.GetComponent<InventoryPanelUI>();
                            Initialized = true;
                        }
                    }
                    catch { Main.logger_instance.Error("InventoryPanel:Init"); }
                }
                public static void Update()
                {
                    if (Refs.InventoryPanelUI.IsNullOrDestroyed()) { Initialized = false; }
                    if (!Initialized) { Init(); }
                }
            }
        }
        public class BlessingsPanel
        {
            public static bool Initialized = false;

            public class Refs
            {
                public static GameObject BlessingsPanel_GameObject = null;
            }            
            public class Functions
            {
                public static void Init()
                {                    
                    try
                    {
                        if (!InventoryPanel.Refs.InventoryPanelUI.IsNullOrDestroyed())
                        {
                            Refs.BlessingsPanel_GameObject = InventoryPanel.Refs.InventoryPanelUI.blessingPanel;
                            Initialized = true;
                        }
                    }
                    catch { Main.logger_instance.Error("BlessingsPanel:Init"); }
                }
                public static void Update()
                {
                    if (!InventoryPanel.Refs.InventoryPanelUI.IsNullOrDestroyed())
                    {
                        if (Refs.BlessingsPanel_GameObject.IsNullOrDestroyed()) { Initialized = false; }
                        if (!Initialized) { Init(); }
                    }
                }
                public static bool IsBlessingOpen()
                {
                    bool result = false;
                    try
                    {
                        if (!Refs.BlessingsPanel_GameObject.IsNullOrDestroyed())
                        {
                            result = Refs.BlessingsPanel_GameObject.active;
                        }
                    }
                    catch { }

                    return result;
                }
            }
        }        
        
        public class GeneralFunctions
        {
            public static GameObject GetChild(GameObject obj, string name)
            {
                GameObject result = null;
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    string obj_name = obj.transform.GetChild(i).gameObject.name;
                    if (obj_name == name)
                    {
                        result = obj.transform.GetChild(i).gameObject;
                        break;
                    }
                }

                return result;
            }
            public static Texture2D MakeTextureFromColor(Color color)
            {
                Texture2D texture = new Texture2D(2, 2);
                Color[] pixels = new Color[2 * 2];
                for (int i = 0; i < pixels.Length; i++) { pixels[i] = color; }
                texture.SetPixels(pixels);
                texture.Apply();

                return texture;
            }
            public static Texture2D MakeTextureFromRGBAColor(float r, float g, float b, float a)
            {
                float new_r = r / 255;
                float new_g = g / 255;
                float new_b = b / 255;
                UnityEngine.Color color = new Color(new_r, new_g, new_b, a);

                Texture2D texture = new Texture2D(2, 2);
                Color[] pixels = new Color[2 * 2];
                for (int i = 0; i < pixels.Length; i++) { pixels[i] = color; }
                texture.SetPixels(pixels);
                texture.Apply();

                return texture;
            }
            public static string AffixRollPercent(byte affix_roll)
            {
                string affix_roll_percent = "Error";
                try
                {
                    double roll_value = (double)affix_roll;
                    double roll_double = roll_value / 255.00 * 100.00;
                    int roll_int = (int)roll_double;
                    affix_roll_percent = roll_int + " %";
                }
                catch { }

                return affix_roll_percent;
            }
            public static void SaveConfig()
            {
                if (!Save_Manager.Data.UserData.Equals(Save_Manager.Data.UserData_duplicate))
                {
                    Save_Manager.Data.UserData_duplicate = Save_Manager.Data.UserData;
                    Save_Manager.Save.Mods();
                }
            }
        }
        public class Textures
        {
            public static Texture2D PauseMenu_Btns_Enable = null;
            public static Texture2D PauseMenu_Btns_Disable = null;
            public static Texture2D Btns_Enable = null;
            public static Texture2D Btns_Disable = null;

            public static Texture2D windowBackground = null;
            public static Texture2D texture_grey = null;
            public static Texture2D texture_green = null;
            public static Texture2D texture_unique = null;
            public static Texture2D texture_set = null;
            public static Texture2D texture_affix_idol = null;
            public static Texture2D texture_affix_prefix = null;
            public static Texture2D texture_affix_suffix = null;

            public static Texture2D black = null;
            public static Texture2D gray = null;
            public static Texture2D grey = null;
            public static Texture2D green = null;
            public static Texture2D red = null;


            public class PauseMenu
            {
                public static Texture2D Menu = null;
                public static Texture2D Bottom = null;
            }

            public static bool IsInitialized = false;
            public static void OnSceneWasInitialized()
            {
                PauseMenu_Btns_Enable = GeneralFunctions.MakeTextureFromRGBAColor(150, 112, 42, 1);
                PauseMenu_Btns_Disable = GeneralFunctions.MakeTextureFromRGBAColor(83, 91, 92, 1);
                Btns_Enable = GeneralFunctions.MakeTextureFromRGBAColor(5, 68, 15, 1);
                Btns_Disable = GeneralFunctions.MakeTextureFromRGBAColor(121, 28, 3, 1);

                windowBackground = GeneralFunctions.MakeTextureFromColor(Color.black);
                texture_grey = GeneralFunctions.MakeTextureFromColor(Color.grey);
                texture_green = GeneralFunctions.MakeTextureFromColor(Color.green);
                texture_unique = GeneralFunctions.MakeTextureFromColor(Color.grey);
                texture_set = GeneralFunctions.MakeTextureFromColor(Color.green);
                texture_affix_idol = GeneralFunctions.MakeTextureFromColor(Color.blue);
                texture_affix_prefix = GeneralFunctions.MakeTextureFromColor(Color.grey);
                texture_affix_suffix = GeneralFunctions.MakeTextureFromColor(Color.yellow);
                black = GeneralFunctions.MakeTextureFromColor(Color.black);
                grey = GeneralFunctions.MakeTextureFromColor(Color.grey);
                gray = GeneralFunctions.MakeTextureFromColor(Color.gray);
                green = GeneralFunctions.MakeTextureFromColor(Color.green);
                red = GeneralFunctions.MakeTextureFromColor(Color.red);

                //PauseMenu
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                Properties.Resources.PauseMenu_Menu.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                PauseMenu.Menu = new Texture2D(1, 1);
                ImageConversion.LoadImage(PauseMenu.Menu, stream.ToArray(), true);

                stream = new System.IO.MemoryStream();
                Properties.Resources.PauseMenu_Bottom.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                PauseMenu.Bottom = new Texture2D(1, 1);
                ImageConversion.LoadImage(PauseMenu.Bottom, stream.ToArray(), true);

                IsInitialized = true;
            }
        }
        public class Styles
        {
            public static GUIStyle PauseMenu_BaseButton(bool select)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (select) { style.normal.background = Textures.PauseMenu_Btns_Enable; }
                else { style.normal.background = Textures.PauseMenu_Btns_Disable; }
                style.normal.textColor = Color.white;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 22;

                return style;
            }
            public static GUIStyle Content_Title()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.PauseMenu_Btns_Disable;
                style.normal.textColor = Color.white;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.focused.background = style.normal.background;
                style.focused.textColor = style.normal.textColor;
                style.active.background = style.normal.background;
                style.active.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 24;

                return style;
            }
            public static GUIStyle Content_Text()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleLeft;
                style.fontSize = 16;

                return style;
            }
            public static GUIStyle ContentR_Text()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleRight;
                style.fontSize = 16;

                return style;
            }
            public static GUIStyle Content_TextArea()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleRight;
                style.fontSize = 16;

                return style;
            }
            public static GUIStyle Content_Infos()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.red;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.focused.background = style.normal.background;
                style.focused.textColor = style.normal.textColor;
                style.active.background = style.normal.background;
                style.active.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleLeft;
                style.fontSize = 14;

                return style;
            }
            public static GUIStyle Content_Button()
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.normal.background = Textures.black;
                style.normal.textColor = Color.grey;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 16;

                return style;
            }
            public static GUIStyle Content_Enable_Button(bool select)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (select) { style.normal.background = Textures.Btns_Enable; }
                else { style.normal.background = Textures.Btns_Disable; }
                style.normal.textColor = Color.white;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 16;

                return style;
            }




            public static GUIStyle Label_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.gray;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.gray;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.gray;
                style.focused.textColor = Color.black;
                style.active.background = Textures.gray;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
            public static GUIStyle Text_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleLeft;

                return style;
            }
            public static GUIStyle Button_Style(bool select)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (select) { style.normal.background = Textures.green; }
                else { style.normal.background = Textures.grey; }
                style.normal.textColor = Color.black;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
            public static GUIStyle TextArea_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.textArea);
                style.normal.background = Textures.grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.grey;
                style.hover.textColor = Color.black;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
            public static GUIStyle Window_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.window);
                float alpha = 0f;
                Color new_color = Color.black;
                Color transparent_color = new Color(new_color.r, new_color.g, new_color.b, alpha);
                style.normal.background = GeneralFunctions.MakeTextureFromColor(transparent_color);
                style.normal.textColor = Color.white;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;

                return style;
            }
            public static GUIStyle Title_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.textField);
                style.normal.background = Textures.windowBackground;
                style.normal.textColor = Color.white;
                style.hover.background = Textures.windowBackground;
                style.hover.textColor = Color.white;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
            public static GUIStyle Infos_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.textField);
                style.normal.background = Textures.texture_grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.texture_grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.texture_grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.texture_grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleLeft;
                style.fontSize = 14;

                return style;
            }
            public static GUIStyle TextField_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = Textures.texture_grey;
                style.normal.textColor = Color.black;
                style.hover.background = Textures.texture_grey;
                style.hover.textColor = Color.black;
                style.focused.background = Textures.texture_grey;
                style.focused.textColor = Color.black;
                style.active.background = Textures.texture_grey;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleLeft;

                return style;
            }
            public static GUIStyle DropdownLabelMidle_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.normal.background = null;
                style.normal.textColor = Color.black;
                style.hover.background = null;
                style.hover.textColor = Color.black;
                style.focused.background = null;
                style.focused.textColor = Color.black;
                style.active.background = null;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
            public static GUIStyle DropdownLabelLeft_Style()
            {
                GUIStyle style = new GUIStyle(GUI.skin.textField);
                style.normal.background = null;
                style.normal.textColor = Color.black;
                style.hover.background = null;
                style.hover.textColor = Color.black;
                style.focused.background = null;
                style.focused.textColor = Color.black;
                style.active.background = null;
                style.active.textColor = Color.black;
                style.alignment = TextAnchor.MiddleLeft;

                return style;
            }
            public static GUIStyle Unique_Style(bool IsSet)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (IsSet) { style.normal.background = Textures.texture_set; }
                else { style.normal.background = Textures.texture_unique; }
                style.normal.textColor = Color.black;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleLeft;

                return style;
            }
            public static GUIStyle Affixs_Style(bool Idol, bool prefix)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (Idol) { style.normal.background = Textures.texture_affix_idol; }
                else
                {
                    if (prefix) { style.normal.background = Textures.texture_affix_prefix; }
                    else { style.normal.background = Textures.texture_affix_suffix; }
                }
                style.normal.textColor = Color.black;
                style.hover.background = style.normal.background;
                style.hover.textColor = style.normal.textColor;
                style.alignment = TextAnchor.MiddleLeft;

                return style;
            }
        }
        public class CustomControls
        {
            public delegate void DelegateFunction();
            public static void Title(string text, float pos_x, float pos_y)
            {
                GUI.TextField(new Rect(pos_x, pos_y, 200, 40), text, Styles.TextField_Style());
            }
            public static void EnableButton(string text, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                GUI.Label(new Rect(pos_x, pos_y, 5, 40), "", Styles.TextField_Style());
                GUI.Label(new Rect(pos_x + 5, pos_y, 135, 40), text, Styles.TextField_Style());
                string btn_enable = "Enable";
                if (enable) { btn_enable = "Disable"; }
                if (GUI.Button(new Rect((pos_x + 140), pos_y, 60, 40), btn_enable, Styles.Button_Style(enable))) { function(); }
            }
           



            public static void RarityInfos(float pos_x, float pos_y)
            {
                float w = 100f;
                float h = 20f;
                GUI.TextField(new Rect(pos_x, pos_y, w, h), "Basic : 0", Styles.Infos_Style());
                GUI.TextField(new Rect(pos_x + 100, pos_y, w, h), "Unique : 7", Styles.Infos_Style());
                pos_y += h;
                GUI.TextField(new Rect(pos_x, pos_y, w, h), "Magic : 1-2", Styles.Infos_Style());
                GUI.TextField(new Rect(pos_x + 100, pos_y, w, h), "Set : 8", Styles.Infos_Style());
                pos_y += h;
                GUI.TextField(new Rect(pos_x, pos_y, w, h), "Rare : 3-4", Styles.Infos_Style());
                GUI.TextField(new Rect(pos_x + 100, pos_y, w, h), "Legendary : 9", Styles.Infos_Style());
            }
            
            public static int IntValue(string text, int minvalue, int maxvalue, int value, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                float multiplier = maxvalue / 255;
                EnableButton(text, pos_x, pos_y, enable, function);
                GUI.DrawTexture(new Rect(pos_x, (pos_y + 40), 140, 40), Textures.texture_grey);

                int result; // = value;
                float min = minvalue / multiplier;
                float max = maxvalue / multiplier;
                if (max > 255) { max = 255; }
                float temp_value = value / multiplier;
                temp_value = (GUI.HorizontalSlider(new Rect(pos_x + 5, (pos_y + 40 + 20), 135, 20), temp_value, min, max) * multiplier);
                string value_str = GUI.TextArea(new Rect((pos_x + 140), (pos_y + 40), 60, 40), (temp_value * multiplier).ToString(), Styles.TextArea_Style());
                try
                {
                    int str = int.Parse(value_str, CultureInfo.InvariantCulture.NumberFormat);
                    temp_value = str / multiplier;
                }
                catch { }
                result = (int)(temp_value * multiplier);

                return result;
            }
            public static float FloatValue(string text, float minvalue, float maxvalue, float value, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                EnableButton(text, pos_x, pos_y, enable, function);
                float temp_value = value;
                GUI.DrawTexture(new Rect(pos_x, (pos_y + 40), 140, 40), Textures.texture_grey);
                temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, (pos_y + 40 + 20), 135, 20), temp_value, minvalue, maxvalue);
                string value_str = GUI.TextArea(new Rect((pos_x + 140), (pos_y + 40), 60, 40), temp_value.ToString(), Styles.TextArea_Style());
                try { temp_value = float.Parse(value_str, CultureInfo.InvariantCulture.NumberFormat); }
                catch { }

                return temp_value;
            }
            public static long LongValue(string text, long minvalue, long maxvalue, long value, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                long result = value;
                EnableButton(text, pos_x, pos_y, enable, function);
                float temp_value = System.Convert.ToSingle(value);
                GUI.DrawTexture(new Rect(pos_x, (pos_y + 40), 140, 40), Textures.texture_grey);
                float min = System.Convert.ToSingle(minvalue);
                float max = System.Convert.ToSingle(maxvalue);
                temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, (pos_y + 40 + 20), 135, 20), temp_value, min, max);
                result = System.Convert.ToInt64(temp_value);
                string value_str = GUI.TextArea(new Rect((pos_x + 140), (pos_y + 40), 60, 40), temp_value.ToString(), Styles.TextArea_Style());
                try
                {
                    temp_value = float.Parse(value_str, CultureInfo.InvariantCulture.NumberFormat);
                    result = System.Convert.ToInt64(temp_value);
                }
                catch { }

                return result;
            }
            public static byte ByteValue(string text, byte minvalue, byte maxvalue, byte value, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                byte result = value;
                EnableButton(text, pos_x, pos_y, enable, function);
                float temp_value = System.Convert.ToSingle(value);
                GUI.DrawTexture(new Rect(pos_x, (pos_y + 40), 140, 40), Textures.texture_grey);
                float min = System.Convert.ToSingle(minvalue);
                float max = System.Convert.ToSingle(maxvalue);
                temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, (pos_y + 40 + 20), 135, 20), temp_value, min, max);
                result = (byte)temp_value;
                string value_str = GUI.TextArea(new Rect((pos_x + 140), (pos_y + 40), 60, 40), temp_value.ToString(), Styles.TextArea_Style());
                try
                {
                    temp_value = float.Parse(value_str, CultureInfo.InvariantCulture.NumberFormat);
                    result = (byte)temp_value;
                }
                catch { }

                return result;
            }
            public static ushort UshortValue(string text, int minvalue, int maxvalue, ushort value, float pos_x, float pos_y, bool enable, DelegateFunction function)
            {
                ushort result = value;
                float multiplier = maxvalue / 255;
                EnableButton(text, pos_x, pos_y, enable, function);
                float temp_value = value / multiplier;//System.Convert.ToSingle(value / multiplier);
                GUI.DrawTexture(new Rect(pos_x, (pos_y + 40), 140, 40), Textures.texture_grey);
                float min = 0;
                float max = 255;
                temp_value = GUI.HorizontalSlider(new Rect(pos_x + 5, (pos_y + 40 + 20), 135, 20), temp_value, min, max);
                result = (ushort)(temp_value * multiplier);
                string value_str = GUI.TextArea(new Rect((pos_x + 140), (pos_y + 40), 60, 40), (temp_value * multiplier).ToString(), Styles.TextArea_Style());
                try { result = ushort.Parse(value_str, CultureInfo.InvariantCulture.NumberFormat); }
                catch { }

                return result;
            }
        }
    }
}