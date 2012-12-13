using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Cs.Exam3.Data;

namespace Cs.Exam3.Business
{
    public class CPayroll
    {
    // Private Fields
        private Guid _Id;
        private string _sEmployeeId;
        private int _iCheckNo;
        private DateTime _dtCheckDate;
        private decimal _decCheckAmount;
        private decimal _decHoursWorked;

    // Public Properties
        public Guid Id
        {
            get { return _Id; }
        }

        public string EmployeeId
        {
            get { return _sEmployeeId; }
            set { _sEmployeeId = value; }
        }

        public int CheckNo
        {
            get { return _iCheckNo; }
            set { _iCheckNo = value; }
        }

        public DateTime CheckDate
        {
            get { return _dtCheckDate; }
            set { _dtCheckDate = value; }
        }

        public decimal CheckAmount
        {
            get { return _decCheckAmount; }
            set { _decCheckAmount = value; }
        }

        public decimal HoursWorked
        {
            get { return _decHoursWorked; }
            set { _decHoursWorked = value; }
        }

    // Public Constructors
        public CPayroll()
        {
            _Id = Guid.NewGuid();
        }

        public CPayroll(string sEmployeeId, int iCheckNo, DateTime dtCheckDate, decimal decCheckAmount, decimal decHoursWorked)
        {
            _Id = Guid.NewGuid();
            EmployeeId = sEmployeeId;
            CheckNo = iCheckNo;
            CheckDate = dtCheckDate;
            CheckAmount = decCheckAmount;
            HoursWorked = decHoursWorked;
        }

        public CPayroll(DataRow oDR)
        {
            _Id = (Guid)oDR["Id"];
            EmployeeId = (string)oDR["EmployeeId"];
            CheckNo = (int)oDR["CheckNo"];
            CheckDate = (DateTime)oDR["CheckDate"];
            CheckAmount = (decimal)oDR["CheckAmount"];
            HoursWorked = (decimal)oDR["HoursWorked"];
        }

    // Public Methods
        public int Insert()
        {
            try
            {
                CSQLServer oCD = new CSQLServer();
                
                int iRows = oCD.Insert("INSERT INTO tblPayroll VALUES('" + Id + "', '" + EmployeeId + "', " + CheckNo + ", '" + CheckDate.ToString() + "', " + CheckAmount.ToString() + ", " + HoursWorked.ToString() + ")");

                oCD = null;
                return iRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update()
        {
            try
            {
                CSQLServer oCD = new CSQLServer();

                int iRows = oCD.Update("UPDATE tblPayroll SET EmployeeId = '" + EmployeeId + "', CheckNo = " + CheckNo.ToString() + ", CheckDate = '" + CheckDate.ToString() + "', CheckAmount = " + CheckAmount.ToString() + ", HoursWorked = " + HoursWorked.ToString() + " WHERE Id = '" + Id + "'");

                oCD = null;
                return iRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete()
        {
            try
            {
                CSQLServer oCD = new CSQLServer();

                int iRows = oCD.Delete("DELETE FROM tblPayroll WHERE Id = '" + Id + "'");

                oCD = null;
                return iRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    // Private Methods
    }
}
