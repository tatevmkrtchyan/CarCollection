using System;

namespace CarCollection
{
    public class SalonChangedEventArgs : EventArgs
    {
       public Brand Brand { get; set; }
       public Car Car { get; set; }  

       public SalonChangedType ChangeType { get; set; }
    }
}
