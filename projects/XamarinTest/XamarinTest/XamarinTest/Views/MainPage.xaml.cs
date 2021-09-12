using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ページが表示される直前の動作。
        /// データベースのデータ取得
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // ソートされた日記一覧をdiaryListのItemSourceに設定
            diaryList.ItemsSource = await App.diaryDAO.GetDiaryAsyncSorted();
        }


        /// <summary>
        /// 日記新規作成ボタン
        /// クリックされることで、日記作成画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewDiaryButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateDiaryPage());
        }

        /// <summary>
        /// 過去の日記データが選択されたとき、その日記データを編集する画面へ遷移する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TappedDiaryListItem(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as DB.Diary;

            Navigation.PushModalAsync(new CreateDiaryPage(item));
        }
    }
}