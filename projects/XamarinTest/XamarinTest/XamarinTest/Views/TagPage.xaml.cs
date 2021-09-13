using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

using XamarinTest.DB;
using System.Collections.ObjectModel;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagPage : ContentPage
    {
        //編集モードかどうかの状態を保持
        private bool IsEditMode = false;

        public TagPage()
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
            // タグリストの取得
            tagList.ItemsSource = await App.tagDAO.GetTagAsync();
        }

        /// <summary>
        /// 編集モードの変更
        /// </summary>
        private async void ChangeMode()
        {
            List<Tag> temp = await App.tagDAO.GetTagAsync();

            // 編集モードを変更
            IsEditMode = !IsEditMode;
            // 表示を変更
            DeleteButton.IsVisible = IsEditMode;

            // テキストを変更
            AddButton.Text = IsEditMode ? "更新" : "追加";
            EditButton.Text = IsEditMode ? "戻る" : "編集";

            // チェック状態を初期化
            foreach (var a in temp)
            {
                a.editMode = IsEditMode;

                if (!IsEditMode)
                {
                    a.isChecked = false;
                }
            }

            // 表示に反映
            tagList.ItemsSource = temp;
        }
        /// <summary>
        /// 編集ボタンが選択されたとき
        /// 編集モードを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditButtonClicked(object sender, EventArgs e)
        {
            ChangeMode();
        }
        /// <summary>
        /// 追加ボタンが押されたときの処理
        /// IsEditModeにより、更新か追加を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            // 更新
            if (IsEditMode)
            {
                if (inputText.Text != string.Empty && inputText.Text != "")
                {
                    bool isUpdate = await DisplayAlert("更新の確認", "選択した項目を更新してよいですか？", "更新する", "キャンセル");
                    if (isUpdate)
                    {
                        List<Tag> temp = (List<Tag>)tagList.ItemsSource;

                        //チェックが入っている要素を削除 
                        bool flag = false;
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp[i].isChecked)
                            {
                                flag = true;
                                temp[i].Text = inputText.Text;
                                temp[i].isChecked = false;
                                temp[i].editMode = false;
                                await App.tagDAO.SaveTagAsync(temp[i]);
                            }
                        }

                        // 削除されていれば、表示を更新
                        if (flag)
                        {
                            temp = await App.tagDAO.GetTagAsync();
                            tagList.ItemsSource = temp;
                            ChangeMode();
                        }
                    }
                }
            }
            // 追加
            else
            {
                if (inputText.Text != string.Empty)
                {
                    // 同名タグがあるか確認する
                    List<Tag> tagListDB = await App.tagDAO.GetTagListAsyncText(inputText.Text);
                    if (tagListDB.Count > 0)
                    {
                        bool isDelete = await DisplayAlert("Alert", "同じ名前のタグは設定できません。", null, "閉じる");
                    }
                    else
                    {
                        // 新規で保存する
                        await App.tagDAO.SaveTagAsync(new DB.Tag
                        {
                            Text = inputText.Text,
                            isChecked = false,
                            editMode = IsEditMode
                        });
                        inputText.Text = string.Empty;

                        List<Tag> temp = await App.tagDAO.GetTagAsync();
                        tagList.ItemsSource = temp;
                    }
                }
            }
        }
        /// <summary>
        /// 削除ボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool isDelete = await DisplayAlert("削除の確認", "選択した項目を削除してよいですか？", "削除する", "キャンセル");
            if (isDelete)
            {
                List<Tag> temp = (List<Tag>)tagList.ItemsSource;

                //チェックが入っている要素を削除 
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].isChecked)
                    {
                        await App.tagDAO.DeleteTagAsync(temp[i]);
                    }
                }

                temp = await App.tagDAO.GetTagAsync();
                tagList.ItemsSource = temp;
                ChangeMode();
            }
        }


        /// <summary>
        /// タグが選択されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TappedTagListItem(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as DB.Tag;
            Navigation.PushModalAsync(new TagDetailPage(item));
        }
    }
}