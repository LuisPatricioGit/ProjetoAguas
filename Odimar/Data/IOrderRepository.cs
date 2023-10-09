﻿using Odimar.Data.Entities;
using Odimar.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Odimar.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName);

        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        Task DeleteDetailTempAsync(int id);

        Task<bool> ConfirmOrderAsync(string userName);

        Task DeliveryOrder(DeliveryViewModel model);
        Task<Order> GetOrderAsync(int id);
    }
}
