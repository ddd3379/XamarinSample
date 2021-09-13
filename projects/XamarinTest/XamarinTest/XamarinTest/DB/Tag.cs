using SQLite;
using System;

namespace XamarinTest.DB
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int TagID { get; set; }
        public string Text { get; set; }

        public string freeText01 { get; set; }
        public string freeText02 { get; set; }
        public string freeText03 { get; set; }
        public string freeText04 { get; set; }
        public string freeText05 { get; set; }
        public string freeText06 { get; set; }
        public string freeText07 { get; set; }
        public string freeText08 { get; set; }
        public string freeText09 { get; set; }
        public string freeText10 { get; set; }

        // list削除用のフラグ
        public bool isChecked { get; set; }
        public bool editMode { get; set; }
    }
}
