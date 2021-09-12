using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XamarinTest.DB
{
    public class DiaryDAO
    {
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// コンストラクタ（DBに接続する）
        /// </summary>
        /// <param name="dbPath">DBのパス</param>
        public DiaryDAO(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Diary>().Wait();
        }

        /// <summary>
        /// 非同期のデータ取得
        /// </summary>
        /// <returns></returns>
        public Task<List<Diary>> GetDiaryAsync()
        {
            return _database.Table<Diary>().ToListAsync();
        }

        /// <summary>
        /// 非同期のデータ取得(日付の降順でソート)
        /// </summary>
        /// <returns></returns>
        public Task<List<Diary>> GetDiaryAsyncSorted()
        {
            return _database.Table<Diary>().OrderByDescending(x => x.Date).ToListAsync();
        }

        /// <summary>
        /// IDを指定したデータ取得
        /// </summary>
        /// <param name="id">CategoryIDの指定</param>
        /// <returns></returns>
        public Task<Diary> GetDiaryAsync(int id)
        {
            return _database.Table<Diary>().Where(i => i.DiaryID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 指定した日付のデータを取得
        /// </summary>
        /// <returns></returns>
        public Task<List<Diary>> GetDiaryAsyncDateTimeDAY(DateTime dateTime)
        {
            DateTime nextDay = dateTime.AddDays(1);
            return _database.Table<Diary>().Where(i => (i.Date >= dateTime && i.Date < nextDay)).OrderByDescending(x => x.Date).ToListAsync();
        }
        /// <summary>
        /// 指定した月のデータを取得
        /// </summary>
        /// <returns></returns>
        public Task<List<Diary>> GetDiaryAsyncDateTimeMonth(DateTime dateTime)
        {
            DateTime nextDay = dateTime.AddMonths(1);
            return _database.Table<Diary>().Where(i => (i.Date >= dateTime && i.Date < nextDay)).OrderByDescending(x => x.Date).ToListAsync();
        }

        /// <summary>
        /// 新規データの挿入(diaryIDを指定すれば、そのデータを更新する)
        /// </summary>
        /// <param name="catergory"></param>
        /// <returns></returns>
        public Task<int> SaveDiaryAsync(Diary diary)
        {
            if (diary.DiaryID != 0)
            {
                return _database.UpdateAsync(diary);
            }
            else
            {
                return _database.InsertAsync(diary);
            }
        }

        /// <summary>
        /// 指定したデータの削除
        /// </summary>
        /// <param name="catergory"></param>
        /// <returns></returns>
        public Task<int> DeleteDiaryAsync(Diary diary)
        {
            return _database.DeleteAsync(diary);
        }
    }
}
