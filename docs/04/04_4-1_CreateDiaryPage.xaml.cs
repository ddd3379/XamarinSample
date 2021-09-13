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
        // 現在のタグ一覧
        List<Tag> tags = null;

        public CreateDiaryPage()
        {
            InitializeComponent();
            GetTagList();
        }

        /// <summary>
        /// 過去の日記データが渡された場合、それらのデータを作成画面に反映
        /// </summary>
        /// <param name="diary"></param>
        public CreateDiaryPage(Diary diary)
        {
            InitializeComponent();
            SetDiaryInfo(diary);
            GetTagList();
        }
        /// <summary>
        /// タグ一覧の取得
        /// </summary>
        private async void GetTagList()
        {
            tags = await App.tagDAO.GetTagAsync();
        }
        /// <summary>
        /// 日記の情報を設定
        /// </summary>
        /// <param name="diary"></param>
        private async void SetDiaryInfo(Diary diary)
        {
            _checkedDiaryID = diary.DiaryID;
            Date.Date = diary.Date;
            Title.Text = diary.Title;
            Detail.Text = diary.Detail;

            Tag temp = await App.tagDAO.GetTagAsync(diary.Tag1);
            tag1.Text = temp != null ? temp.Text : "タグ1を設定";

            temp = await App.tagDAO.GetTagAsync(diary.Tag2);
            tag2.Text = temp != null ? temp.Text : "タグ2を設定";

            temp = await App.tagDAO.GetTagAsync(diary.Tag3);
            tag3.Text = temp != null ? temp.Text : "タグ3を設定";
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
                // タグ情報の取得
                Tag saveTag1 = await App.tagDAO.GetTagAsyncText(tag1.Text);
                int saveTagID1 = tag1.Text == "タグ1を設定" ? -1 : saveTag1.TagID;

                Tag saveTag2 = await App.tagDAO.GetTagAsyncText(tag2.Text);
                int saveTagID2 = tag2.Text == "タグ2を設定" ? -1 : saveTag2.TagID;

                Tag saveTag3 = await App.tagDAO.GetTagAsyncText(tag3.Text);
                int saveTagID3 = tag3.Text == "タグ3を設定" ? -1 : saveTag3.TagID;



                // DAOを用いて入力されたデータをDBに保存する
                await App.diaryDAO.SaveDiaryAsync(
                    new DB.Diary { DiaryID = _checkedDiaryID,
                                   Date = Date.Date,
                                   Title = Title.Text,
                                   Detail = Detail.Text,
                                   Tag1 = saveTagID1,
                                   Tag2 = saveTagID2,
                                   Tag3 = saveTagID3
                    });

                // セーブが完了したら、元画面に自動的に戻る
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// タグ1が選択されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTag1Clicked(object sender, EventArgs e)
        {
            if (tags.Count != 0)
            {
                List<string> tagList = new List<string>();
                foreach (Tag tag in tags)
                {
                    tagList.Add(tag.Text);
                }

                string check = await DisplayActionSheet("タグを選択", "Cancel", "Reset", tagList.ToArray());
                if (check == "Reset")
                {
                    tag1.Text = "タグ1を設定";
                }
                else if (check == "Cancel")
                {
                    // nothing
                }
                else
                {
                    tag1.Text = check;
                }
            }
            else
            {
                string check = await DisplayActionSheet("先にタグを作成してください。", "Cancel", null);
            }
        }
        /// <summary>
        /// タグ2が選択されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTag2Clicked(object sender, EventArgs e)
        {
            if (tags.Count != 0)
            {
                List<string> tagList = new List<string>();
                foreach (Tag tag in tags)
                {
                    tagList.Add(tag.Text);
                }

                string check = await DisplayActionSheet("タグを選択", "Cancel", "Reset", tagList.ToArray());
                if (check == "Reset")
                {
                    tag2.Text = "タグ2を設定";
                }
                else if (check == "Cancel")
                {
                    // nothing
                }
                else
                {
                    tag2.Text = check;
                }
            }
            else
            {
                string check = await DisplayActionSheet("先にタグを作成してください。", "Cancel", null);
            }
        }
        /// <summary>
        /// タグ3が選択されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTag3Clicked(object sender, EventArgs e)
        {
            if (tags.Count != 0)
            {
                List<string> tagList = new List<string>();
                foreach (Tag tag in tags)
                {
                    tagList.Add(tag.Text);
                }

                string check = await DisplayActionSheet("タグを選択", "Cancel", "Reset", tagList.ToArray());
                if (check == "Reset")
                {
                    tag3.Text = "タグ3を設定";
                }
                else if (check == "Cancel")
                {
                    // nothing
                }
                else
                {
                    tag3.Text = check;
                }
            }
            else
            {
                string check = await DisplayActionSheet("先にタグを作成してください。", "Cancel", null);
            }
        }
    }
}