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
    public partial class CreateDiaryPage : ContentPage
    {
        public CreateDiaryPage()
        {
            InitializeComponent();
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
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // 保存の前段階として、入力された情報を表示する機能を実装。

            // 入力されたテキストの取得
            DateTime dateTime = Date.Date;  // 日付（型：DateTime）
            string titleText = Title.Text;  // タイトル
            string detailText = Detail.Text;    // 詳細

            // 入力された内容を表示
            var result = DisplayAlert(titleText, detailText +"\n\n日付:"+dateTime.ToString("yyyy/MM/dd"), "OK", "キャンセル");
        }
    }
}