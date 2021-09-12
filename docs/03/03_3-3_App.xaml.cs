using Xamarin.Forms;
using XamarinTest.DB;
using System;
using System.IO;

namespace XamarinTest
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// 日記データベースを開くソース
        /// </summary>
        static string diaryDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "diary.db3");
        static DiaryDAO diaryDatabase;
        public static DiaryDAO diaryDAO
        {
            get
            {
                if (diaryDatabase == null)
                {
                    diaryDatabase = new DiaryDAO(diaryDbPath);
                }
                return diaryDatabase;
            }
        }
    }
}
