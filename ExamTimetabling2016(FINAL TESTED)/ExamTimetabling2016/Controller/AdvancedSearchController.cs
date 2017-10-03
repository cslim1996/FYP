using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

public class AdvancedSearchController
{
    AdvancedSearch advancedSearch;

    public AdvancedSearchController()
    {
        advancedSearch = new AdvancedSearch();
    }

    public AdvancedSearchController(String datePeriod)
    {
        advancedSearch = new AdvancedSearch(datePeriod);
    }

    public DataTable searchByDatePeriod(String DatePeriod)
    {
        return advancedSearch.searchByDatePeriod(DatePeriod);
    }

    public DataTable searchByDatePeriodWithOptionFaculty(String DatePeriod, String option)
    {
        return advancedSearch.searchByDatePeriodWithOptionFaculty(DatePeriod, option);
    }

    public DataTable searchByDatePeriodWithOptionFacultyProgramme(String DatePeriod, String option, String option2)
    {
        return advancedSearch.searchByDatePeriodWithOptionFacultyProgramme(DatePeriod, option, option2);
    }

    public DataTable searchByDatePeriodWithOptionFacultyCourse(String DatePeriod, String option, String option2)
    {
        return advancedSearch.searchByDatePeriodWithOptionFacultyCourse(DatePeriod, option, option2);
    }

    public DataTable searchByDatePeriodWithOptionDate(String DatePeriod, String option)
    {
        return advancedSearch.searchByDatePeriodWithOptionDate(DatePeriod, option);
    }

    public DataTable searchByDatePeriodWithOptionDateProgramme(String DatePeriod, String option, String option2)
    {
        return advancedSearch.searchByDatePeriodWithOptionDateProgramme(DatePeriod, option, option2);
    }

    public DataTable searchByDatePeriodWithOptionDateCourse(String DatePeriod, String option, String option2)
    {
        return advancedSearch.searchByDatePeriodWithOptionDateCourse(DatePeriod, option, option2);
    }

    public DataTable searchByDatePeriodWithProgramme(String DatePeriod, String option)
    {
        return advancedSearch.searchByDatePeriodWithProgramme(DatePeriod, option);
    }

    public DataTable searchByDatePeriodWithCourse(String DatePeriod, String option)
    {
        return advancedSearch.searchByDatePeriodWithCourse(DatePeriod, option);
    }

    public DataTable searchByDatePeriodWithProgrammeCourse(String DatePeriod, String option, String option2)
    {
        return advancedSearch.searchByDatePeriodWithProgrammeCourse(DatePeriod, option, option2);
    }

    public List<String> getFaculty(String DatePeriod)
    {
        return advancedSearch.getFaculty(DatePeriod);
    }

    public List<String> getDate(String DatePeriod)
    {
        return advancedSearch.getDate(DatePeriod);
    }

    public List<String> getDateSession(String DatePeriod, String option)
    {
        return advancedSearch.getDateSession(DatePeriod, option);
    }

    public List<String> getProgramme(String DatePeriod)
    {
        return advancedSearch.getProgramme(DatePeriod);
    }

    public List<String> getProgrammeDate(String DatePeriod, String option)
    {
        return advancedSearch.getProgrammeDate(DatePeriod, option);
    }

    public List<String> getProgrammeFAFB(String DatePeriod)
    {
        return advancedSearch.getProgrammeFAFB(DatePeriod);
    }

    public List<String> getProgrammeFASC(String DatePeriod)
    {
        return advancedSearch.getProgrammeFASC(DatePeriod);
    }

    public List<String> getProgrammeFEBE(String DatePeriod)
    {
        return advancedSearch.getProgrammeFEBE(DatePeriod);
    }

    public List<String> getProgrammeFSAH(String DatePeriod)
    {
        return advancedSearch.getProgrammeFSAH(DatePeriod);
    }

    public List<String> getProgrammeCNBL(String DatePeriod)
    {
        return advancedSearch.getProgrammeCNBL(DatePeriod);
    }

    public List<String> getCourse(String DatePeriod)
    {
        return advancedSearch.getCourse(DatePeriod);
    }

    public List<String> getCourseDate(String DatePeriod, String option)
    {
        return advancedSearch.getCourseDate(DatePeriod, option);
    }

    public List<String> getCourseDateByDate(String DatePeriod, String option, String option2, String option3)
    {
        return advancedSearch.getCourseDateByDate(DatePeriod, option, option2, option3);
    }

    public List<String> getCourseFAFB(String DatePeriod)
    {
        return advancedSearch.getCourseFAFB(DatePeriod);
    }

    public List<String> getCourseFASC(String DatePeriod)
    {
        return advancedSearch.getCourseFASC(DatePeriod);
    }

    public List<String> getCourseFEBE(String DatePeriod)
    {
        return advancedSearch.getCourseFEBE(DatePeriod);
    }

    public List<String> getCourseFSAH(String DatePeriod)
    {
        return advancedSearch.getCourseFSAH(DatePeriod);
    }

    public List<String> getCourseCNBL(String DatePeriod)
    {
        return advancedSearch.getCourseCNBL(DatePeriod);
    }

    public List<String> getCourseWithProgrammeOrderDate(String DatePeriod, String option)
    {
        return advancedSearch.getCourseWithProgrammeOrderDate(DatePeriod, option);
    }

    public List<String> getCourseWithProgramme(String DatePeriod, String option)
    {
        return advancedSearch.getCourseWithProgramme(DatePeriod, option);
    }

    public List<String> getProgrammeWithCourse(String DatePeriod, String option)
    {
        return advancedSearch.getProgrammeWithCourse(DatePeriod, option);
    }

    public List<String> getTimetableOfProgrammeCourse_Main(String DatePeriod, String option, String option2, String option3)
    {
        return advancedSearch.getTimetableOfProgrammeCourse_Main(DatePeriod, option, option2, option3);
    }

    public String[,] getTimetableOfProgrammeCourse_ResitRepeat(String DatePeriod, String option, String option2)
    {
        return advancedSearch.getTimetableOfProgrammeCourse_ResitRepeat(DatePeriod, option, option2);
    }
    public String[,] getTimetableOfProgrammeCourse_MainSearch(String DatePeriod, String option2, String option3)
    {
        return advancedSearch.getTimetableOfProgrammeCourse_MainSearch(DatePeriod, option2, option3);
    }

    public String[,] getTimetableOfProgrammeCourse_ResitRepeatSearch(String DatePeriod, String option2)
    {
        return advancedSearch.getTimetableOfProgrammeCourse_ResitRepeatSearch(DatePeriod, option2);
    }

    public List<String> getProgrammeWOYear(String DatePeriod)
    {
        return advancedSearch.getProgrammeWOYear(DatePeriod);
    }

    public List<String> getProgrammeFAFBWOYear(String DatePeriod)
    {
        return advancedSearch.getProgrammeFAFBWOYear(DatePeriod);
    }

    public List<String> getProgrammeFASCWOYear(String DatePeriod)
    {
        return advancedSearch.getProgrammeFASCWOYear(DatePeriod);
    }

    public List<String> getProgrammeFEBEWOYear(String DatePeriod)
    {
        return advancedSearch.getProgrammeFEBEWOYear(DatePeriod);
    }

    public List<String> getProgrammeFSAHWOYear(String DatePeriod)
    {
        return advancedSearch.getProgrammeFSAHWOYear(DatePeriod);
    }
}
