using System.Collections.Generic;

namespace MVC.MyClones.Models
{
    /// <summary>
    /// Таблица
    /// </summary>
    public class DEGridTable
    {
        public DEGridTableDataSource DataSource { get; set; }
        public List<DEGridTableColumn> Columns { get; set; }
        public string MasterDetailName { get; set; }
    }

    /// <summary>
    /// Ресурсы для таблицы
    /// </summary>
    public class DEGridTableDataSource
    {
        public string Type { get; set; }

        /// <summary>
        /// Ключ, по которому идентифицировать данные, чаще всего - ид
        /// </summary>
        public string Key { get; set; }
        public Dictionary<string, string> LoadParams { get; set; }

        /// <summary>
        /// Урл для загрузки данных
        /// </summary>
        public string LoadUrl { get; set; }

        /// <summary>
        /// Урл для обновления данных
        /// </summary>
        public string UpdateUrl { get; set; }

        /// <summary>
        /// Урл для создания элемента
        /// </summary>
        public string CreateUrl { get; set; }

        /// <summary>
        /// Урл для удаления элемента
        /// </summary>
        public string DeleteUrl { get; set; }
    }

    /// <summary>
    /// Колонка
    /// </summary>
    public class DEGridTableColumn
    {
        public DEGridTableColumn() { }
        public DEGridTableColumn(string dataField)
        {
            IsSimple = true;
            DataField = dataField;
        }

        public bool IsSimple { get; set; }

        /// <summary>
        /// Название поля, по которому строится колонка
        /// </summary>
        public string DataField { get; set; }

        /// <summary>
        /// Название колонки, которое будет отображено
        /// </summary>
        public string Caption { get; set; }

        public DEGridTableLookup Lookup { get; set; }
    }

    public class DEGridTableLookup
    {
        public string ValueExpr { get; set; }
        public string DisplayExpr { get; set; }
        public DEGridTableDataSource DataSource { get; set; }
    }
}
