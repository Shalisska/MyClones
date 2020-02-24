using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Common
{
    public enum ManufactoringStageState
    {
        /// <summary>
        /// В ожидании
        /// </summary>
        Pending,

        /// <summary>
        /// В процессе
        /// </summary>
        Processing,

        /// <summary>
        /// Закончена
        /// </summary>
        Ended
    }
}
