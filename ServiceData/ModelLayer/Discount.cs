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
        //Empty Constructor
        public Discount() { }
    
        //Constructor without Id Parameter
        public Discount(double rate){

            Rate = rate;

        }

        //Reuses Constructor

        public Discount(int id, double rate) : this (rate)
        {
            Id = id;
        }


    }



}
