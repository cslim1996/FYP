using System;
using System.Collections.Generic;
using System.Data;
using System.Web;


    public class ExamSesssionController
    {
        ExamSesssion examSesion;

        public ExamSesssionController()
	    {
            examSesion = new ExamSesssion();
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public ExamSesssionController(DateTime date, String session)
        {
            examSesion = new ExamSesssion(date, session);
            //
            // TODO: Add constructor logic here
            //
        }

        public void insertToDB(DateTime date, String session, String DatePeriod)
        {
            examSesion.insertToDB(date, session, DatePeriod);
        }

        public String[,] checkHoliday(String startDate, String endDate)
        {
            examSesion = new ExamSesssion();
            return examSesion.checkHoliday(startDate, endDate);
        }

        public void deletePreviousSelectedDate(String sessionID)
        {
            examSesion.deletePreviousSelectedDate(sessionID);
        }

        public Boolean checkTimeslotIdExist(String TimeslotID)
        {
            return examSesion.checkTimeslotIdExist(TimeslotID);
        }

        public DataTable searchByExamePeriod(String DatePeriod)
        {
            return examSesion.searchByExamePeriod(DatePeriod);
        }

        public List<String> searchDayByExamePeriod(String DatePeriod)
        {
            return examSesion.searchDayByExamePeriod(DatePeriod);
        }

        public List<String> searchSessionByExamePeriod(String DatePeriod)
        {
            return examSesion.searchSessionByExamePeriod(DatePeriod);
        }
    
    }
    
