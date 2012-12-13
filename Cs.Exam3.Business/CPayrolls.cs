using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Cs.Exam3.Data;

namespace Cs.Exam3.Business
{
    public class CPayrolls
    {
    // Private Fields
        private List<CPayroll> _glItems;

    // Public Properties
        public int Count
        {
            get { return _glItems.Count; }
        }

        public List<CPayroll> Items
        {
            get { return _glItems; }
            set { _glItems = value; }
        }

    // Public Constructors
        public CPayrolls()
        {
            _glItems = new List<CPayroll>();
        }

    // Public Methods
        public void Add(CPayroll oItem)
        {
            _glItems.Add(oItem);
        }

        public void RemoveAt(int iIndex)
        {
            _glItems.RemoveAt(iIndex);
        }

        public int GetData()
        {
            try
            {
                Items = new List<CPayroll>();
                CSQLServer oSQL = new CSQLServer();

                DataTable odtPayrolls = oSQL.GetData("SELECT * FROM tblPayroll;");
                foreach (DataRow oDR in odtPayrolls.Rows)
                {
                    CPayroll oPayroll = new CPayroll(oDR);
                    this.Add(oPayroll);
                }

                return odtPayrolls.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
