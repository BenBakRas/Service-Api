using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class OrderlineGroup
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderlineId { get; set; }

        public int ComboId { get; set; }
        //Empty constructor
        public OrderlineGroup() { }

        //Constructor with parameteter xecept orderline
        public OrderlineGroup(int productId, int orderLineId, int comboId)
        {
            ProductId = productId;
            OrderlineId = orderLineId;
            ComboId = comboId;
           
        }
        public OrderlineGroup(int id, int productId, int orderLineId, int comboId) : this(productId, orderLineId, comboId) 
        {
            Id = id;

        }


    }
}
