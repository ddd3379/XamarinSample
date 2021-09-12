using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XamarinTest.DB
{
    public class TagDAO
    {
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// コンストラクタ（DBに接続する）
        /// </summary>
        /// <param name="dbPath">DBのパス</param>
        public TagDAO(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Tag>().Wait();
        }

        /// <summary>
        /// 非同期のデータ取得
        /// </summary>
        /// <returns></returns>
        public Task<List<Tag>> GetTagAsync()
        {
            return _database.Table<Tag>().ToListAsync();
        }

        /// <summary>
        /// IDを指定したデータ取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Tag> GetTagAsync(int id)
        {
            return _database.Table<Tag>().Where(i => i.TagID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// タグ名に指定したデータ取得
        /// </summary>
        /// <returns></returns>
        public Task<Tag> GetTagAsyncText(string text)
        {
            return _database.Table<Tag>().Where(i => i.Text == text).FirstOrDefaultAsync();
        }

        /// <summary>
        /// タグ名に指定したデータ一覧取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<Tag>> GetTagListAsyncText(string text)
        {
            return _database.Table<Tag>().Where(i => i.Text == text).ToListAsync();
        }

        /// <summary>
        /// 新規データの挿入
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveTagAsync(Tag tag)
        {
            if (tag.TagID != 0)
            {
                return _database.UpdateAsync(tag);
            }
            else
            {
                return _database.InsertAsync(tag);
            }
        }

        /// <summary>
        /// 指定したデータの削除
        /// </summary>
        /// <param name="catergory"></param>
        /// <returns></returns>
        public Task<int> DeleteTagAsync(Tag tag)
        {
            return _database.DeleteAsync(tag);
        }
    }
}
