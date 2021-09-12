using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinTest.DB;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDiaryPage : ContentPage
    {
        // 選択された日記のID格納（新規作成は0）
        private int _checkedDiaryID = 0;

        public CreateDiaryPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 過去の日記データが渡された場合、それらのデータを作成画面に反映
        /// </summary>
        /// <param name="diary"></param>
        public CreateDiaryPage(Diary diary)
        {
            InitializeComponent();
            SetDiaryInfo(diary);
        }
        private void SetDiaryInfo(Diary diary)
        {
            _checkedDiaryID = diary.DiaryID;
            Date.Date = diary.Date;
            Title.Text = diary.Title;
            Detail.Text = diary.Detail;          
        }

        /// <summary>
        /// 戻るボタンが押された時に呼び出し。
        /// 元の画面に戻る。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// 保存ボタンが押されたときに呼び出し。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // 保存の前段階として、入力された情報を表示する機能を実装。

            // 入力されたテキストの取得
            DateTime dateTime = Date.Date;  // 日付（型：DateTime）
            string titleText = Title.Text;  // タイトル
            string detailText = Detail.Text;    // 詳細

            // 入力された内容を表示
            var result = await DisplayAlert(titleText+"("+ dateTime.ToString("yyyy/MM/dd") + ")", detailText, "OK", "キャンセル");
            // OKが押された場合のみ保存
            if (result)
            {
                // DAOを用いて入力されたデータをDBに保存する
                await App.diaryDAO.SaveDiaryAsync(
                    new DB.Diary { DiaryID = _checkedDiaryID,
                                   Date = Date.Date,
                                   Title = Title.Text,
                                   Detail = Detail.Text});

                // セーブが完了したら、元画面に自動的に戻る
                await Navigation.PopModalAsync();
            }
        }
    }
}