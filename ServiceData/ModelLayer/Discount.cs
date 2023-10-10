using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Discount
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public int ProductGroupId { get ; set; }
        public int CustomerGroupId { get; set; }


        //Empty Constructor
        public Discount() { }
    
        //Constructor without Id Parameter
        public Discount(double rate, int productGroupId, int customerGroupId )
        {

            Rate = rate;
            ProductGroupId = productGroupId;
            CustomerGroupId = customerGroupId;

        }

        //Reuses Constructor

        public Discount(int id, double rate, int productGroupId, int customerGroupId) : this (rate, productGroupId, customerGroupId)
        {
            Id = id;
        }


    }



}
