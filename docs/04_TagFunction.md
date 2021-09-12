# 04. タグ機能

## 1. タグのDB作成
1. [Visual C#アイテム]>[クラス]  (Tagを作成)    
[Diary.cs](./04/04_1-1_Tag.cs)  

2. データベースを操作するクラスの作成
[Visual C#アイテム]>[クラス]  (TagDAOを作成)    
[DiaryDAO.cs](./04/04_1-2_TagDAO.cs)  





## 2. DB操作を追加

1. データベースを開くコードの追加(App.xaml.csを編集)  
[App.xaml.cs](./04/04_2-1_App.xaml.cs)  



## 3. タグ一覧ページの作成

1. ページの新規作成
[Visual C#アイテム]>[Xamarin.Forms]>[コンテンツページ]  
今回はコンテンツページ名は「TagPage」とした。

2. コードの修正  
[TagPage.xaml](./04/04_3-2_TagPage.xaml)  
[TagPage.xaml.cs](./04/04_3-2_TagPage.xaml.cs)  


3. タブにタグページを追加する  
[AppShell.xaml](./04/04_3-3_AppShell.xaml)  


4. 実行結果

![実行結果](../image/04/04_3-4_xamarinTest005.gif) 



## 4. 日記作成ページにタグを追加

1. コードの修正  
[CreateDiaryPage.xaml](./04/04_4-1_CreateDiaryPage.xaml)  
[CreateDiaryPage.xaml.cs](./04/04_4-1_CreateDiaryPage.xaml.cs)

2. 実行結果

![実行結果](../image/04/04_4-2_xamarinTest006.gif) 
