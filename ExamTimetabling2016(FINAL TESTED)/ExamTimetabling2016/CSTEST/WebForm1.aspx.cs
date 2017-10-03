using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.CSTEST
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //initialization of variables;
        string[] demoString1 = {"Gender == M","isMuslim == Y"};
        string[] word;
        string y;
        //read line function


        protected void Page_Load(object sender, EventArgs e)
        {
        }
        //validity test
        public void Test1() {

            //for testing validity of the variables
            foreach (string a in demoString1)
            {
                    word = a.Split(' ');

            }
        } 
         
        //get a list of invigilators based on  
        public void Test2()
        {
            foreach (string x in demoString1)
            {
                if (x.ToLower().Equals("gender"))
                {

                }
            }
        }
    }
}