using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.IO;
using Microsoft.Web.Helpers;
namespace QuintessenceDataServer.Models {
    public class Item {
        public struct SimpleItem {
            public int ItemID;
            public string Name;
            public string Description;
        }
        [Serializable]
        public struct SlotEnumStruct {
            public int SlotID;
            public int? ParentSlot;
            public string Name;
        }

        public int ItemID;
        public string Name;
        public string Description;
        public int? StackSize = null;
        public Equipment Equipment = null;
        public List<Tool> Tools = null;
        public List<SkillRequirement> Skills = null;
        public static void Delete(int ItemID) {
            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                //get tool data
                SqlCommand cmd = new SqlCommand("DeleteItem", conn);
                cmd.Parameters.AddWithValue("@ItemID",ItemID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
            }

        }
        private void LoadToolData() {

            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                //get tool data
                SqlCommand cmd = new SqlCommand("SelectItemTool", conn);
                cmd.Parameters.AddWithValue("@ItemID",ItemID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    Tools = new List<Tool>();
                    while (reader.Read()) {
                        Tools.Add(new Tool(reader.GetAsValue<int>("ToolID"),reader.GetAsValue<int>("ItemID"),reader.GetAsValue<int>("ToolLevel"),
                                    reader.GetAsValue<int>("ToolType"), reader.GetAsValue<double>("Efficiency"), reader.GetAsValue<bool>("IsModifier")));
                    }
                }
                reader.Close();
            }
        }
        private void LoadSkillData() {
            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                //get skill data
                SqlCommand cmd = new SqlCommand("SelectItemSkill", conn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@ItemID";
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = ItemID;
                cmd.Parameters.Add(param1);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    Skills = new List<SkillRequirement>();
                    while (reader.Read()) {
                        Skills.Add(new SkillRequirement((int)reader["SkillID"], (int)reader["ItemID"], 
                                    (int)reader["Skill"], (int)reader["Level"], (bool)reader["IsModifier"]));
                    }
                }
                reader.Close();
            }

        }
        private void LoadItem() {
            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            //string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SelectEquipmentItem", conn);
                cmd.Parameters.AddWithValue("@ItemID",ItemID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read()) {
                    Name = reader.GetAsValue<string>("Name");
                    Description = reader.GetAsValue<string>("Description");
                    StackSize = reader.GetAsNullInt("StackSize");
                    //Has equipment
                    if (reader["Slot"] != DBNull.Value) {
                        Equipment = new Equipment(ItemID, (int)reader["Slot"],reader["SlotName"].ToString(),
                            reader.GetAsNullInt("ParentSlot"), reader["ParentSlotName"].ToString(), reader.GetAsNullInt("AvailableSlots"));
                    }
                }
                reader.Close();
            }
        }
        public Item(int ID) {
            ItemID = ID;
            LoadItem();
            LoadToolData();
            LoadSkillData();
        }
        public Item() {

        }
        public string LoadFromCollection(int id, string json) {
            if (string.IsNullOrEmpty(json))
                return "No save data passed";
            ItemID = id;
            var reader = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Name = reader["Name"].Trim();
            Description = reader["Description"];
            if (string.IsNullOrEmpty(Name)) {
                return "Item name is empty";
            }
            if (string.IsNullOrEmpty(Description)) {
                return "Item description is empty";
            }
            int stack;
            if (int.TryParse(reader["StackSize"], out stack)) {
                StackSize = stack;
            }
            foreach(var current in JsonConvert.DeserializeObject<string[]>(reader["ToolData"])) {
                
                //TODO:
            }
            return "";
        }
        public string GetToolsAsJson() {
            return JsonConvert.SerializeObject(Tools.ToArray());
        }
        public string GetSkillsAsJson() {
            return JsonConvert.SerializeObject(Skills.ToArray());
        }
        public static List<SlotEnumStruct> GetSlotEnum(int? BaseSlot) {
            List<SlotEnumStruct> retVar = new List<SlotEnumStruct>();
            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SelectAllSlotEnums", conn);
                if (BaseSlot.HasValue && BaseSlot !=  0) {
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@ParentSlot";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = BaseSlot;
                    cmd.Parameters.Add(param1);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        SlotEnumStruct tmp = new SlotEnumStruct();
                        if (!reader.IsDBNull(0))
                            tmp.SlotID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            tmp.ParentSlot = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            tmp.Name = reader.GetString(2).Trim();
                        retVar.Add(tmp);
                    }
                }
                reader.Close();
            }
            return retVar;
        }
        public static List<SimpleItem> GetItemPage(int page, int pageSize = 20) {
            List<SimpleItem> retVal = new List<SimpleItem>();
            //TODO: Connection string
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SelectItemPage", conn);
                cmd.Parameters.AddWithValue("@page",page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        retVal.Add(new SimpleItem { ItemID = reader.GetAsValue<int>("ItemID"), 
                            Name = reader.GetAsValue<string>("Name"), Description = reader.GetAsValue<string>("Description") });
                    }
                }
                reader.Close();
            }
            return retVal;
        }
    }
    public static class DatabaseExtensions {

        public static int? GetAsNullInt(this SqlDataReader reader, string row) {
            int? retVar = null;
            int value;
            try {
                if(int.TryParse(reader[row].ToString(), out value))
                    retVar = value;
            }
            catch { }
            return retVar;
        }
        public static T GetAsValue<T>(this SqlDataReader reader, string row) {
            return (T)reader[row];
        }
        public static int? toNullable(this int value) {
            int? retVar = null;
            retVar = value;
            return value;
        }
    }
}