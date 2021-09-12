# 03. 日記アプリの基本部分

## 1. 画面遷移

### 〇概要
MainPageには今まで作成した日記の一覧を表示し、日記の新規作成は別画面で行う。
そこで、MainPageから日記作成画面への遷移を行う。

### 1-1. 日記作成画面を作成

[Visual C#アイテム]>[Xamarin.Forms]>[コンテンツページ]  
今回はコンテンツページ名は「CreateDiaryPage」とした。



### 1-2. CreateDiaryPageのコードの編集

CreateDiaryPageと表示されるだけの画面。  

- [CreateDiaryPage.xaml](./03/03_1-2_CreateDiaryPage.xaml)  
- CreateDiaryPage.xaml.csは修正なし。



### 1-3. MainPageのコードの編集

ボタンを押すことで、日記作成画面へ遷移するように変更。

- [MainPage.xaml](./03/03_1-3_MainPage.xaml)  
- [MainPage.xaml.cs](./03/03_1-3_MainPage.xaml.cs)  

#### 〇補足

- MainPage.xaml
```XML
        <Button Text="新規作成"
                VerticalOptions="EndAndExpand"
                Clicked="OnNewDiaryButtonClicked" />
```
クリックされた際、"OnNewDiaryButtonClicked"が呼び出される。

- MainPage.xaml.cs
```C#
        private void OnNewDiaryButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateDiaryPage());
        }
```
Navigation.PushModalAsync()により、新規画面を呼び出すことができる。  
呼び出された画面では、「スマホ搭載の戻るボタンをクリック」または「Navigation.PopModalAsync()を実行」で元の画面に戻ることができる。

##### 参考：https://docs.microsoft.com/ja-jp/xamarin/xamarin-forms/app-fundamentals/navigation/modal






## 2. テキストの入力および入力した内容の取得

### 〇概要
ここでは、CreateDiaryPageを編集し、「テキストの入力フォームの作成」および「そのフォームに入力された内容の取得」ができるようにする。


### 2-1. 

