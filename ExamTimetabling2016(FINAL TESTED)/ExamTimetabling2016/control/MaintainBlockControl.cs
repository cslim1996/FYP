using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    class MaintainBlockControl
    {
        private BlockDA blockDA;

        public MaintainBlockControl()
        {
            blockDA = new BlockDA();
        }

        public List<Block> searchBlocksList(DateTime date, string time)
        {
            return blockDA.searchBlocksList(date, time);
        }

        public void shutDown()
        {
            blockDA.shutDown();
        }
    }
}
