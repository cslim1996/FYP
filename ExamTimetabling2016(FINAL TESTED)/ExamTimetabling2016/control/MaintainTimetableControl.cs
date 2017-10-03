using System.Collections.Generic;

namespace ExamTimetabling2016
{
    class MaintainTimetableControl
    {
        private TimetableDA timetableDA;

        public MaintainTimetableControl()
        {
            timetableDA = new TimetableDA();
        }

        public List<Timetable> selectTimetable()
        {
            return timetableDA.selectTimetable();
        }

        public void shutDown()
        {
            timetableDA.shutDown();
        }
    }
}
