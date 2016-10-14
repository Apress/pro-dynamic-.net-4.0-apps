using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionDemo
{
    public interface IInvoice
    {
        DateTime SaleDate { get; set; }
        double GetTotal();
    }

    public interface IMoreInvoice
    {
        int SalesRepID { get; set; }
    }

    public delegate void UpdateEventHandler(object sender, EventArgs e);

    public class Invoice : IInvoice, IMoreInvoice
    {
        public int iInvoiceNumber;
        public string szCustomerCode;

        private DateTime _dSaleDate;
        private int _iSalesRepID;

        public DateTime SaleDate
        {
            get { return _dSaleDate; }
            set { _dSaleDate = value; }
        }

        public int SalesRepID
        {
            get { return _iSalesRepID; }
            set { _iSalesRepID = value; }
        }

        public Invoice()
        {
        }

        public Invoice(DateTime dSaleDate, int iSalesRepID)
        {
            dSaleDate = _dSaleDate;
            iSalesRepID = _iSalesRepID;
        }

        public event UpdateEventHandler Updated;

        protected virtual void OnUpdated(EventArgs e)
        {
            if (Updated != null)
                Updated(this, e);
        }

        public double GetTotal()
        {
            return 150;
        }

        public double GetSalesTax()
        {
            return 150 * .07;
        }

        public double ApplyDiscount(double dblDiscount)
        {
            return 150 * dblDiscount;
        }

    }

}
