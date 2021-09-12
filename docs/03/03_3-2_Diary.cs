using SQLite;
using System;

namespace XamarinTest.DB
{
    public class Diary
    {
        [PrimaryKey, AutoIncrement]
        public int DiaryID { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Tag1 { get; set; }
        public int Tag2 { get; set; }
        public int Tag3 { get; set; }
        public int Tag4 { get; set; }
        public int Tag5 { get; set; }
        public int Tag6 { get; set; }

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
