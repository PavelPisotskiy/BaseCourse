using BaseCourse.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCourse.DataSource.Interfaces
{
    interface IOrderItemRecordsChangeController
    {
        IEnumerable<OrderItemRecord> GetOrderItemRecordsForAdding();
        IEnumerable<OrderItemRecord> GetOrderItemRecordsForUpdating();
        IEnumerable<OrderItemRecord> GetOrderItemRecordsForDeleting();
    }
}
