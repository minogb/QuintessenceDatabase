using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuintessenceDataServer.Models {
    public class Equipment {
        public int ItemID;
        public int Slot;
        public string SlotName;
        public int? ParentSlot = null;
        public string ParentSlotName = null;
        public int? AvailableSlots;
        public List<string> AvailableSlotNames = new List<string>();
        public void LoadSlots() {
            if (!AvailableSlots.HasValue)
                return;
            string ConnectionString = @"Data Source=WAKAWAKA\SQLEXPRESS;Initial Catalog = QuintessenceDatabase;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SelectSubSlotEnums", conn);
                SqlParameter param1 = new SqlParameter();

                param1.ParameterName = "@ParentSlot";
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = Slot;
                cmd.Parameters.Add(param1);

                param1 = new SqlParameter();
                param1.ParameterName = "@Flags";
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = AvailableSlots.Value;
                cmd.Parameters.Add(param1);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        AvailableSlotNames.Add(reader.GetString(2).Trim());
                    }
                }
                reader.Close();
            }
        }
        public Equipment(int ItemID) {

        }
        public Equipment(int ItemID, int Slot, string SlotName, int? ParentSlot, string ParentSlotName, int? AvailableSlots) {
            this.ItemID = ItemID;
            this.Slot = Slot;
            this.SlotName = SlotName.Trim();
            this.ParentSlot = ParentSlot;
            this.ParentSlotName = ParentSlotName.Trim();
            this.AvailableSlots = AvailableSlots;
            LoadSlots();
        }
    }
}
