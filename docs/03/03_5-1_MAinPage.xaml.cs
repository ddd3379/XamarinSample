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
    public partial class MainPage : ContentPage
    {
        //編集モードかどうかの状態を保持
        private bool IsEditMode = false;

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


        /// <summary>
        /// 編集モードを変更する
        /// </summary>
        private async void ChangeMode()
        {
            // データベースのデータ取得
            List<Diary> temp = await App.diaryDAO.GetDiaryAsyncSorted();

            // IsEditModeを変更する
            IsEditMode = !IsEditMode;
            // IsEditModeに応じて、表示内容を変更
            DeleteButton.IsVisible = IsEditMode;
            EditButton.Text = IsEditMode ? "戻る" : "編集";

            // 各データにeditModeおよびisCheckedを反映
            foreach (var a in temp)
            {
                a.editMode = IsEditMode;

                if (!IsEditMode)
                {
                    a.isChecked = false;
                }
            }

            // 表示
            diaryList.ItemsSource = temp;
        }

        /// <summary>
        /// 編集ボタン(or戻るボタン)が押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditButtonClicked(object sender, EventArgs e)
        {
            // 編集モードを変更する
            ChangeMode();
        }
        /// <summary>
        /// 削除ボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // 確認ダイアログの表示
            bool isDelete = await DisplayAlert("削除の確認", "選択した項目を削除してよいですか？", "削除する", "キャンセル");

            // 「削除する」が選択された場合は削除処理の実行
            if (isDelete)
            {
                // 現在の表示されているdiaryListを取得
                List<Diary> temp = (List<Diary>)diaryList.ItemsSource;

                //チェックが入っている要素を削除 
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].isChecked)
                    {
                        await App.diaryDAO.DeleteDiaryAsync(temp[i]);
                    }
                }

                // 削除後のデータベース情報を取得し、反映
                temp = await App.diaryDAO.GetDiaryAsyncSorted();
                diaryList.ItemsSource = temp;

                // 編集モードを変更する
                ChangeMode();
            }
        }
    }
}